using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.OperationLibrary.Indicator
{
    public class KDJ :Indicator
    {

        public KDJ(string type,string name,  int LONG, int M1, int M2) :base(type)
        {
            this.Type = type;
            this.Long = LONG;
            this.M1 = M1;
            this.M2 = M2;
            this.Name = name;
        }
        Dictionary<int, List<StoZoo.Dog.StockData.KDJ>> KDJData = new Dictionary<int, List<StoZoo.Dog.StockData.KDJ>>();
        int Long;
        int M1;
        int M2;


        public StoZoo.Dog.StockData.KDJ GetIndicator(DateTime datetime)
        {
            if (KDJData.ContainsKey(datetime.Year))
            {
                if (Type == "lc15")
                {
                    return KDJData[datetime.Year].FirstOrDefault(p => p.dateTime == getCurrentMinute(datetime, 15));
                }
                else
                    return KDJData[datetime.Year].FirstOrDefault(p => p.dateTime == datetime);

            }
            else if (KDJData.ContainsKey(0))
                return KDJData[0].FirstOrDefault(p => p.dateTime.Date == datetime.Date);
            else
                return null;
        }
        public StoZoo.Dog.StockData.KDJ GetIndicator(int index)
        {
            foreach (int y in KDJData.Keys)
                if (index < KDJData[y].Count)
                    return KDJData[y][index];
                else
                    index -= KDJData[y].Count;
            return null;
        }
        public int IndexOf(StoZoo.Dog.StockData.KDJ kdj)
        {
            int index = 0;
            foreach (int y in KDJData.Keys)
            {
                if (kdj.dateTime.Year == y || y == 0)
                {
                    int i = KDJData[y].IndexOf(kdj);
                    if (i >= 0)
                        return index + i;
                    else
                        return -1;
                }
                index += KDJData[y].Count;
            }
            return -1;
        }

        KLine  baseline = null;
        public override bool LoadData(StoZoo.Dog.StockData.Stock stock, Run.RunEnvironment environment = null, bool need = true)
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

            if (baseline != null)
            {
              
                int l = Long;
                int m1 = M1;
                int m2 = M2;

                if (Globe.DeriveValues.ContainsKey("_Y_" + Name + "_LONG"))
                    l = (int)Globe.DeriveValues["_Y_" + Name + "_LONG"];
                if (Globe.DeriveValues.ContainsKey("_Y_" + Name + "_M1"))
                    m1 = (int)Globe.DeriveValues["_Y_" + Name + "_M1"];
                if (Globe.DeriveValues.ContainsKey("_Y_" + Name + "_M2"))
                    m2 = (int)Globe.DeriveValues["_Y_" + Name + "_M2"];
                if (Globe.DeriveValues.ContainsKey("_Y_" + Name + "_LONG") || Globe.DeriveValues.ContainsKey("_Y_" + Name + "_M1")|| Globe.DeriveValues.ContainsKey("_Y_" + Name + "_M2")||need )
                {
                    KDJData.Clear();
                    List<int> years = baseline.Years;
                    for (int i = 0; i < years.Count; i++)
                    {
                        List<StoZoo.Dog.StockData.KLine> klines = new List<StoZoo.Dog.StockData.KLine>();
                        klines.AddRange(baseline.GetKlineYear(years[i]));
                        //List<StoZoo.Dog.StockData.KLine> klines = baseline.GetKlineYear(years[i]);
                        if (i > 0)
                            klines.InsertRange(0, baseline.GetKlineYear(years[i - 1]).TakeLast(l));

                        KDJData.Add(years[i], StoZoo.Dog.StockData.KDJ.Count(klines, l, m1, m2));

                    }

                    OffsetIndex = -1;
                } 
                
                if (KDJData.Count  < 1) return false;
                return true;
            }
            else if (environment.otherKlines.ContainsKey(Type))
            { return true; }
            else { return false; }
        }


        public override double GetValue(params object[] param)
        {
            string target = (string)param[0];
            int index = 0;
            if (param.Length > 1)
                int.TryParse(param[1].ToString(), out index); 

            if (OffsetIndex <0)
            {
                if (Type == "day" || Type == "week" || Type == "month")
                {
                    var thiskdj = GetIndicator(Environment.thisDate.Date);
                    if (thiskdj == null) return double.NaN;
                    int arrayindex = IndexOf(thiskdj);
                    if (arrayindex < 0) return double.NaN;
                    OffsetIndex = baseline.thisIndex - arrayindex;
                }
                else if (Type == "lc5")
                {
                    if (baseline.thisIndex == -1)
                    {
                        baseline.thisIndex = (Environment.baseKlines[Type] as KLine).IndexOf(Environment.thisLC5Kline);
                    }
                    if (baseline.thisIndex == -1) return double.NaN;

                    var thiskdj = GetIndicator(Environment.thisDate.Date + Environment.thisTime);
                    if (thiskdj == null) return double.NaN;
                    int arrayindex = IndexOf(thiskdj);
                    if (arrayindex < 0) return double.NaN;
                    OffsetIndex = baseline.thisIndex - arrayindex;
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

                    var thisma = GetIndicator(Environment.thisDate.Date + Environment.thisTime);
                    if (thisma == null) return double.NaN;
                    int arrayindex = IndexOf(thisma);
                    if (arrayindex < 0) return double.NaN;
                    OffsetIndex = baseline.thisIndex - arrayindex;
                }
            }
            StoZoo.Dog.StockData.KDJ reskdj = null;
            if (Type == "day")
            {
                if (index == 0)
                {
                    if (baseline.thisIndex - OffsetIndex < 0) return double.NaN;
                    reskdj = StoZoo.Dog.StockData.KDJ.Count(GetIndicator(baseline.thisIndex - OffsetIndex), Environment.realDayKline.endPrice, Environment.realDayKline.lowPrice, Environment.realDayKline.highPrice);
                }
                else
                {
                    if (baseline.thisIndex - OffsetIndex - index < 0) return double.NaN;
                    reskdj = GetIndicator(baseline.thisIndex - OffsetIndex - index);
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
                    reskdj = StoZoo.Dog.StockData.KDJ.Count(lastvalue , Environment.thisLC5Kline.endPrice,Environment.thisLC5Kline.lowPrice,Environment.thisLC5Kline.highPrice);
                }
                else
                {
                    if (baseline.thisIndex - OffsetIndex - index < 0) return double.NaN;
                    reskdj = GetIndicator(baseline.thisIndex - OffsetIndex - index);
                    if (reskdj == null) return double.NaN;
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
                    if (baseline.thisIndex - OffsetIndex < 0) return double.NaN;
                    var lastvalue = GetIndicator(baseline.thisIndex - OffsetIndex);
                    if (lastvalue == null) return double.NaN;
                    reskdj = StoZoo.Dog.StockData.KDJ.Count(lastvalue, Environment.thisLC5Kline.endPrice, Environment.thisLC5Kline.lowPrice, Environment.thisLC5Kline.highPrice);
                }
                else
                {
                    if (baseline.thisIndex - OffsetIndex - index < 0) return double.NaN;
                    reskdj = GetIndicator(baseline.thisIndex - OffsetIndex - index);
                    if (reskdj == null) return double.NaN;
                }


            }

            switch (target.ToLower())
            {
                case "k":
                    return reskdj.K;
                case "d":
                    return reskdj.D;
                case "j":
                    return reskdj.J;
                case "value":
                    return reskdj.J;
            }

            return double.NaN;
        }

    }
}
