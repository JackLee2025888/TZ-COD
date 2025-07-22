using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.OperationLibrary.Indicator
{
    public class MACD : Indicator
    {
        public MACD(string type,string name, int SHORT, int LONG, int MID) : base(type)
        {
            this.Type = type;
            this.Long = LONG;
            this.Mid  = MID;
            this.Short = SHORT;
            this.Name = name;
        }
      
        Dictionary<int, List<StoZoo.Dog.StockData.MACD>> MACDData = new Dictionary<int, List<StoZoo.Dog.StockData.MACD>>();
        int Short;
        int Long;
        int Mid;



        public StoZoo.Dog.StockData.MACD GetIndicator(DateTime datetime)
        {
            if (MACDData.ContainsKey(datetime.Year))
            {
                if (Type == "lc15")
                {
                    return MACDData[datetime.Year].FirstOrDefault(p => p.dateTime == getCurrentMinute(datetime, 15));
                }
                else
                    return MACDData[datetime.Year].FirstOrDefault(p => p.dateTime == datetime);

            }
            else if (MACDData.ContainsKey(0))
                return MACDData[0].FirstOrDefault(p => p.dateTime.Date == datetime.Date);
            else
                return null;
        }
        public StoZoo.Dog.StockData.MACD GetIndicator(int index)
        {
            foreach (int y in MACDData.Keys)
                if (index < MACDData[y].Count)
                    return MACDData[y][index];
                else
                    index -= MACDData[y].Count;
            return null;
        }
        public int IndexOf(StoZoo.Dog.StockData.MACD ma)
        {
            int index = 0;
            foreach (int y in MACDData.Keys)
            {
                if (ma.dateTime.Year == y || y == 0)
                {
                    int i = MACDData[y].IndexOf(ma);
                    if (i >= 0)
                        return index + i;
                    else
                        return -1;
                }
                index += MACDData[y].Count;
            }
            return -1;
        }
        KLine baseline = null;
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
                int s = Short;
                int m = Mid ;

                if (Globe.DeriveValues.ContainsKey("_Y_" + Name + "_LONG"))
                    l = (int)Globe.DeriveValues["_Y_" + Name + "_LONG"];
                if (Globe.DeriveValues.ContainsKey("_Y_" + Name + "_Short"))
                    s = (int)Globe.DeriveValues["_Y_" + Name + "_Short"];
                if (Globe.DeriveValues.ContainsKey("_Y_" + Name + "_Mid"))
                    m = (int)Globe.DeriveValues["_Y_" + Name + "_Mid"];

                if (Globe.DeriveValues.ContainsKey("_Y_" + Name + "_LONG") || Globe.DeriveValues.ContainsKey("_Y_" + Name + "_Short") || Globe.DeriveValues.ContainsKey("_Y_" + Name + "_Mid") || need)
                {
                    MACDData.Clear();
                    List<int> years = baseline.Years;
                    for (int i = 0; i < years.Count; i++)
                    {
                        List<StoZoo.Dog.StockData.KLine> klines = new List<StoZoo.Dog.StockData.KLine>();
                        klines.AddRange(baseline.GetKlineYear(years[i]));
                       // List<StoZoo.Dog.StockData.KLine> klines = baseline.GetKlineYear(years[i]);
                        if (i > 0)
                            klines.InsertRange(0, baseline.GetKlineYear(years[i - 1]).TakeLast(l));

                        MACDData.Add(years[i], StoZoo.Dog.StockData.MACD.Count(klines, s, l, m));

                    }

                    OffsetIndex = -1;
                }
                if (MACDData.Count < 1) return false;
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

            if (OffsetIndex < 0)
            {if (Type == "day")
                {
                    var thismacd = GetIndicator(Environment.thisDate.Date);
                    if (thismacd == null) return double.NaN;
                    int arrayindex = IndexOf(thismacd);
                    if (arrayindex < 0) return double.NaN;
                    OffsetIndex = baseline.thisIndex - arrayindex;
                }
                else if (Type == "lc5")
                {
                    if (baseline.thisIndex == -1)
                    {
                        baseline.thisIndex = (Environment.baseKlines[Type] as KLine).IndexOf(Environment.thisLC5Kline);
                    }
                    if (baseline.thisIndex < 0) return double.NaN;
                    var thisma = GetIndicator(Environment.thisDate.Date+Environment.thisTime);
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
            StoZoo.Dog.StockData.MACD resmacd = null;
            if (Type == "day" || Type == "week" || Type == "month")
            {
                if (index == 0)
                {
                    if (baseline.thisIndex - OffsetIndex < 0) return double.NaN;
                    resmacd = StoZoo.Dog.StockData.MACD.Count(GetIndicator(baseline.thisIndex - OffsetIndex), Environment.realDayKline.endPrice, Short, Long, Mid);
                }
                else
                {
                    if (baseline.thisIndex - OffsetIndex - index < 0) return double.NaN;
                    resmacd = GetIndicator(baseline.thisIndex - OffsetIndex - index);
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
                    resmacd = StoZoo.Dog.StockData.MACD.Count(lastvalue, Environment.realDayKline.endPrice, Short, Long, Mid);

                }
                else
                {
                    if (baseline.thisIndex - OffsetIndex - index < 0) return double.NaN;
                    resmacd = GetIndicator(baseline.thisIndex - OffsetIndex - index);
                    if (resmacd == null) return double.NaN;
                }
            }
            else
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

                    resmacd = StoZoo.Dog.StockData.MACD.Count(lastvalue, Environment.realDayKline.endPrice, Short, Long, Mid);
                }
                else
                {
                    if (baseline.thisIndex - OffsetIndex - index < 0) return double.NaN;
                    resmacd = GetIndicator(baseline.thisIndex - OffsetIndex - index);
                    if (resmacd == null) return double.NaN;
                }
            }

            switch (target.ToLower())
            {
                case "macd":
                    return resmacd.Macd;
                case "dea":
                    return resmacd.Dea;
                case "diff":
                    return resmacd.Diff;
                case "value":
                    return resmacd.Macd;
            }
            return 0;
        }

    }
}
