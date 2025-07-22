using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.OperationLibrary.Indicator
{
   public  class RSI:Indicator
    {
        public RSI(string type,string name, int LONG) : base(type)
        {
            this.Type = type;
            this.Long = LONG;
            this.Name = name;
        }
        Dictionary<int, List<StoZoo.Dog.StockData.RSI>> RSIData = new Dictionary<int, List<StoZoo.Dog.StockData.RSI>>();
        int Long;

        public StoZoo.Dog.StockData.RSI GetIndicator(DateTime datetime)
        {
            if (RSIData.ContainsKey(datetime.Year))
            {
                if (Type == "lc15")
                {
                    return RSIData[datetime.Year].FirstOrDefault(p => p.dateTime == getCurrentMinute(datetime, 15));
                }
                else
                    return RSIData[datetime.Year].FirstOrDefault(p => p.dateTime == datetime);

            }
            else if (RSIData.ContainsKey(0))
                return RSIData[0].FirstOrDefault(p => p.dateTime.Date == datetime.Date);
            else
                return null;
        }
        public StoZoo.Dog.StockData.RSI GetIndicator(int index)
        {
            foreach (int y in RSIData.Keys)
                if (index < RSIData[y].Count)
                    return RSIData[y][index];
                else
                    index -= RSIData[y].Count;
            return null;
        }
        public int IndexOf(StoZoo.Dog.StockData.RSI ma)
        {
            int index = 0;
            foreach (int y in RSIData.Keys)
            {
                if (ma.dateTime.Year == y || y == 0)
                {
                    int i = RSIData[y].IndexOf(ma);
                    if (i >= 0)
                        return index + i;
                    else
                        return -1;
                }
                index += RSIData[y].Count;
            }
            return -1;
        }

        KLine baseline = null;
        public override bool LoadData(StoZoo.Dog.StockData.Stock stock,Run.RunEnvironment environment = null, bool need = true)
        {
            this.Environment = environment;
            if (environment.baseKlines.ContainsKey(Type))
            {
                baseline = environment.baseKlines[Type] as KLine;
            }
            if (environment.otherKlines.ContainsKey(Type))
            {
                baseline = environment.otherKlines[Type] as KLine;
            }
            if (baseline != null )
            {
              
                int l = Long;

                if (Globe.DeriveValues.ContainsKey("_Y_" + Name))
                    l = (int)Globe.DeriveValues["_Y_" + Name];

                if (Globe.DeriveValues.ContainsKey("_Y_" + Name) || need)
                {
                    RSIData.Clear();
                    List<int> years = baseline.Years;
                    for (int i = 0; i < years.Count; i++)
                    {
                        List<StoZoo.Dog.StockData.KLine> klines = new List<StoZoo.Dog.StockData.KLine>();
                        klines.AddRange(baseline.GetKlineYear(years[i]));
                        //List<StoZoo.Dog.StockData.KLine> klines = baseline.GetKlineYear(years[i]);
                        if (i > 0)
                            klines.InsertRange(0, baseline.GetKlineYear(years[i - 1]).TakeLast(l));

                        RSIData.Add(years[i], StoZoo.Dog.StockData.RSI.Count(klines, l));

                    }
                    OffsetIndex = -1;
                }
                if (RSIData.Count < 1) return false;
                return true;
            }
            return false;         
        }
        public override double GetValue(params object[] param)
        {
            string target = (string)param[0];
            int index = 0;
            if (param.Length > 1)
                int.TryParse(param[1].ToString (), out index);

            //if (OffsetIndex < 0)
            if(true)
            {
                if (Type == "day" || Type == "week" || Type == "month")
                {
                    var thisrsi = GetIndicator(Environment.thisDate.Date);
                    if (thisrsi == null) return double.NaN;
                    int arrayindex = IndexOf(thisrsi);
                    if (arrayindex < 0) return double.NaN;
                    baseline.OffsetIndex = baseline. thisIndex - arrayindex;
                }
                else if (Type == "lc5")
                {
                    if (baseline.thisIndex == -1)
                    {
                        baseline.thisIndex = (Environment.baseKlines[Type] as KLine).IndexOf(Environment.thisLC5Kline);
                    }
                    if (baseline.thisIndex < 0) return double.NaN;
                    var thisrsi = GetIndicator(Environment.thisDate.Date + Environment.thisTime);
                    if (thisrsi == null) return double.NaN;
                    int arrayindex = IndexOf(thisrsi);
                    if (arrayindex < 0) return double.NaN;
                    baseline.OffsetIndex = baseline.thisIndex - arrayindex;
                }
                else if (Type == "lc15")
                {
                    if (baseline.thisIndex == -1)
                    {
                        var kline = Environment.otherKlines[Type] as KLine;
                        var thiskline = kline.GetKline(getCurrentMinute(Environment.thisLC5Kline.dateTime, 15));
                        if (thiskline == null) return double.NaN;
                        baseline.thisIndex = kline.IndexOf(thiskline);
                        //  baseline.thisIndex = (Environment.baseKlines[Type] as KLine).IndexOf(Environment.thisLC5Kline);
                    }
                    if (baseline.thisIndex < 0) return double.NaN;

                    var thisrsi = GetIndicator(Environment.thisDate.Date + Environment.thisTime);
                    if (thisrsi == null) return double.NaN;
                    int arrayindex = IndexOf(thisrsi);
                    if (arrayindex < 0) return double.NaN;
                    baseline.OffsetIndex = baseline.thisIndex - arrayindex;
                }

            }


            StoZoo.Dog.StockData.RSI rsi = null;
            if (Type == "day" || Type == "week" || Type == "month")
            {
                if (index == 0)
                {if (baseline.thisIndex - baseline.OffsetIndex < 0) return double.NaN;
                    rsi = StoZoo.Dog.StockData.RSI.Count(GetIndicator(baseline.thisIndex - baseline.OffsetIndex), Environment.realDayKline.endPrice);
                }
                else
                {
                    if (baseline.thisIndex - baseline.OffsetIndex - index < 0) return double.NaN;
                    rsi = GetIndicator(baseline.thisIndex - baseline.OffsetIndex - index);
                }
            }
            else if (Type == "lc15")
            {
                if (baseline.thisIndex == -1)
                {
                    var kline = Environment.otherKlines[Type] as KLine;
                    var thiskline = kline.GetKline(Environment.thisLC5Kline.dateTime);
                    if (thiskline == null) return double.NaN;
                    baseline.thisIndex = kline.IndexOf(thiskline);
                }
                if (baseline.thisIndex < 0) return double.NaN;
                if (index == 0)
                {
                    if (baseline.thisIndex - baseline.OffsetIndex < 0) return double.NaN;
                    var lastvalue = GetIndicator(baseline.thisIndex - baseline.OffsetIndex);
                    if (lastvalue == null) return double.NaN;
                    rsi = StoZoo.Dog.StockData.RSI.Count(lastvalue, Environment.realDayKline.endPrice);
                }
                else
                {
                    if (baseline.thisIndex - baseline.OffsetIndex - index < 0) return double.NaN;
                    rsi = GetIndicator(baseline.thisIndex - baseline.OffsetIndex - index);
                    if (rsi == null) return double.NaN;
                }
            }

            else {
                if (baseline.thisIndex == -1)
                {
                    baseline.thisIndex = (Environment.baseKlines[Type] as KLine).IndexOf(Environment.thisLC5Kline);
                }
                if (baseline.thisIndex < 0) return double.NaN;
                if (index == 0)
                {
                    if (baseline.thisIndex - baseline.OffsetIndex < 0) return double.NaN;
                    var lastvalue = GetIndicator(baseline.thisIndex - baseline.OffsetIndex);
                    if (lastvalue == null) return double.NaN;
                    rsi = StoZoo.Dog.StockData.RSI.Count(lastvalue, Environment.realDayKline.endPrice);
                }
                else
                {
                    if (baseline.thisIndex - baseline.OffsetIndex - index < 0) return double.NaN;
                    rsi = GetIndicator(baseline.thisIndex - baseline.OffsetIndex - index);
                    if (rsi == null) return double.NaN;
                }
            }

            switch (target.ToLower())
            {
                case "value":
                    return rsi.Value;
            }
            return double.NaN;
        }

    }
}
