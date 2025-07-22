using System;
using System.Collections.Generic;

namespace TZ.OperationLibrary.Indicator
{
    public class MA : Indicator
    {
        public MA(string type, string name, int longPeriod) : base(type)
        {
            this.Type = type;
            this.Name = name;
            this.Long = longPeriod;
        }

        private Dictionary<int, List<StoZoo.Dog.StockData.Average>> MAData = new Dictionary<int, List<StoZoo.Dog.StockData.Average>>();
        private int Long;



        public StoZoo.Dog.StockData.Average GetIndicator(DateTime datetime)
        {
            if (MAData.ContainsKey(datetime.Year))
            {
                if (Type == "lc15")
                {
                    return MAData[datetime.Year].FirstOrDefault(p => p.dateTime == getCurrentMinute(datetime, 15));
                }
                else
                    return MAData[datetime.Year].FirstOrDefault(p => p.dateTime == datetime);

            }
            else if (MAData.ContainsKey(0))
            {
                if (Type == "day")
                {
                    return MAData[0].FirstOrDefault(p => p.dateTime.Date == datetime.Date);
                }
                if (Type == "month")
                {
                    return MAData[0].FirstOrDefault(p => p.dateTime.Year == datetime.Year && p.dateTime.Month == datetime.Month);
                }
                if (Type == "week")
                {
                    StoZoo.Dog.StockData.Average week = null;
                    foreach (var kline in MAData[0])
                    {
                        double cha = (kline.dateTime - datetime).TotalDays;
                        if (cha >= 0 && cha < 7 && (kline.dateTime.DayOfWeek >= datetime.DayOfWeek))
                        {
                            week = kline;
                            break;
                        }
                        if (cha <= 0 && cha > -7 && (kline.dateTime.DayOfWeek <= datetime.DayOfWeek))
                        {
                            week = kline;
                            break;
                        }
                    }
                    return week;
                }
                else
                { return null; }
            }

            else
                return null;
        }
        public StoZoo.Dog.StockData.Average GetIndicator(int index)
        {
            foreach (int year in MAData.Keys)
            {
                if (index < MAData[year].Count)
                    return MAData[year][index];
                else
                    index -= MAData[year].Count;
            }
            return null;
        }

        public int IndexOf(StoZoo.Dog.StockData.Average ma)
        {
            int index = 0;
            foreach (int year in MAData.Keys)
            {
                if (ma.dateTime.Year == year || year == 0)
                {
                    int i = MAData[year].IndexOf(ma);
                    if (i >= 0)
                        return index + i;
                    else
                        return -1;
                }
                index += MAData[year].Count;
            }
            return -1;
        }

        public override bool LoadData(StoZoo.Dog.StockData.Stock stock, Run.RunEnvironment environment = null, bool need = true)
        {
            this.Environment = environment;

            if (environment.baseKlines.TryGetValue(Type, out var baseline))
            {
                int l = Long;
                if (Globe.DeriveValues.TryGetValue("_Y_" + Name, out var derivedValue))
                {
                    l = (int)derivedValue;
                }

                if (!Globe.DeriveValues.TryGetValue("_Y_" + Name) || need)
                {
                    MAData.Clear();
                    List<int> years = baseline.Years;
                    for (int i = 0; i < years.Count; i++)
                    {
                        List<StoZoo.Dog.StockData.KLine> klines = new List<StoZoo.Dog.StockData.KLine>();
                        klines.AddRange(baseline.GetKlineYear(years[i]));

                        if (i > 0)
                            klines.InsertRange(0, baseline.GetKlineYear(years[i - 1]).TakeLast(l - 1));

                        MAData.Add(years[i], StoZoo.Dog.StockData.Average.Count(klines, l));
                    }
                    OffsetIndex = -1;
                }
                if (MAData.Count < 1)
                    return false;
                return true;
            }
            return false;
        }
        public override double GetValue(params object[] param)
        {
            string target = (string)param[0];
            int index = 0;
            if (param.Length > 1)
            {
                int.TryParse(param[1].ToString(), out index);
            }

						 



            if (OffsetIndex < 0)
            {
                if (Type == "day" || Type == "week" || Type == "month")
                {
                    var thisma = GetIndicator(Environment.thisDate.Date);
                    if (thisma == null) return double.NaN;
                    int arrayindex = IndexOf(thisma);
                    if (arrayindex < 0) return double.NaN;
                    OffsetIndex = baseline.thisIndex - arrayindex;
                }
                else if (Type == "lc5") {
                    if (baseline.thisIndex == -1)
                    {
                        baseline.thisIndex = (Environment.baseKlines[Type] as KLine).IndexOf(Environment.thisLC5Kline);
                    }
                    if (baseline.thisIndex < 0) return double.NaN;

                    var thisma = GetIndicator(Environment.thisDate.Date + Environment.thisTime);
                    if (thisma == null) return double.NaN;
                    int arrayindex = IndexOf(thisma);
                    if (arrayindex < 0) return double.NaN;
                    OffsetIndex = baseline.thisIndex - arrayindex;
                }
                else if (Type == "lc15")
                {
                    if (baseline.thisIndex == -1)
                    {
                        var kline = Environment.otherKlines[Type] as KLine;
                        var thiskline = kline.GetKline(getCurrentMinute(Environment.thisLC5Kline.dateTime, 15));
                        if (thiskline == null)
                            return double.NaN; 
                        baseline.thisIndex = kline.IndexOf(thiskline);
                        // baseline.thisIndex = (Environment.baseKlines[Type] as KLine).IndexOf(Environment.thisLC5Kline);
                    }
                    if (baseline.thisIndex < 0) return double.NaN;

                    var thisma = GetIndicator(Environment.thisDate.Date + Environment.thisTime);
                    if (thisma == null) return double.NaN;
                    int arrayindex = IndexOf(thisma);
                    if (arrayindex < 0) return double.NaN;
                    OffsetIndex = baseline.thisIndex - arrayindex;
                }
            }

            StoZoo.Dog.StockData.Average ma = null;
            if (Type == "day" || Type == "week" || Type == "month")
            {
                if (index == 0)
                {
                    if (baseline.thisIndex - OffsetIndex < 0) return double.NaN;
                    ma = StoZoo.Dog.StockData.Average.Count(GetIndicator(baseline.thisIndex - OffsetIndex), Environment.realDayKline.endPrice);
                }
                else
                {
                    if (baseline.thisIndex - OffsetIndex - index < 0) return double.NaN;
                    ma = GetIndicator(baseline.thisIndex - OffsetIndex - index);
                }
            }
            else if (Type == "lc5")
            {
                if (baseline.thisIndex == -1)
                {
                    baseline.thisIndex = (Environment.baseKlines[Type] as KLine).IndexOf(Environment.thisLC5Kline);
                }
                if (baseline.thisIndex < 0) return double.NaN;
                if (index == 0)
                {
                    if (baseline.thisIndex - OffsetIndex < 0) return double.NaN;
                    var lastvalue = GetIndicator(baseline.thisIndex - OffsetIndex);
                    if (lastvalue == null) return double.NaN;

                    ma = StoZoo.Dog.StockData.Average.Count(lastvalue, Environment.thisLC5Kline.endPrice);
                }
                else
                {
                    if (baseline.thisIndex - OffsetIndex - index < 0) return double.NaN;
                    ma = GetIndicator(baseline.thisIndex - OffsetIndex - index);
                    if (ma == null) return double.NaN;
                }
            }
            else if (Type == "lc15")
            {
                if (baseline.thisIndex == -1)
                {
                    var kline = Environment.otherKlines[Type] as KLine;
                    var thiskline= kline.GetKline(Environment.thisLC5Kline.dateTime);
                    if (thiskline == null)
                        return double.NaN;
                    baseline.thisIndex = kline.IndexOf(thiskline);
                }
                if (baseline.thisIndex < 0) return double.NaN;
                if (index == 0)
                {
                    if (baseline.thisIndex - OffsetIndex < 0) return double.NaN;
                    var lastvalue = GetIndicator(baseline.thisIndex - OffsetIndex);
                    if (lastvalue == null) return double.NaN;
                    ma = StoZoo.Dog.StockData.Average.Count(lastvalue, Environment.thisLC5Kline.endPrice);
                }
                else
                {
                    if (baseline.thisIndex - OffsetIndex - index < 0) return double.NaN;
                    ma = GetIndicator(baseline.thisIndex - OffsetIndex - index);
                    if (ma == null) return double.NaN;
                }


            }

            switch (target.ToLower())
            {
                case "value":
                    //Gemini 修改添加的部分,在`GetValue`方法中，`ma`变量可能为`null`。如果`ma`变量为`null`，那么`switch (target.ToLower())`语句将引发`NullReferenceException`异常。为了避免这种情况，您应该在`switch`语句之前检查`ma`变量是否为`null`。
                    if (ma == null)
                    {
                        throw new NullReferenceException("The 'ma' variable is null.");
                    }
                    //Google Gemini修改添加的部分end
                    return ma.Value;
            }
            return double.NaN;
        }

    }
}
