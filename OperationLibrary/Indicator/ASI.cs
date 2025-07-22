using System;
using System.Collections.Generic;
using System.Linq;

namespace TZ.OperationLibrary.Indicator
{
    public class ASI : Indicator
    {
        public ASI(string type, string name, int M, int N) : base(type)
        {
            this.Type = type;
            this.M = M;
            this.N = N;
            this.Name = name;
        }

        Dictionary<int, List<StoZoo.Dog.StockData.ASI>> ASIData = new Dictionary<int, List<StoZoo.Dog.StockData.ASI>>();

        int M;
        int N;
        int OffsetIndex = -1;

        public StoZoo.Dog.StockData.ASI GetIndicator(DateTime datetime)
        {
            if (ASIData.ContainsKey(datetime.Year))
            {
                if (Type == "lc15")
                {
                    return ASIData[datetime.Year].FirstOrDefault(p => p.dateTime == getCurrentMinute(datetime, 15));
                }
                else
                {
                    return ASIData[datetime.Year].FirstOrDefault(p => p.dateTime == datetime);
                }
            }
            else if (ASIData.ContainsKey(0))
            {
                return ASIData[0].FirstOrDefault(p => p.dateTime.Date == datetime.Date);
            }
            else
            {
                return null;
            }
        }

        public StoZoo.Dog.StockData.ASI GetIndicator(int index)
        {
            foreach (int y in ASIData.Keys)
            {
                if (index < ASIData[y].Count)
                {
                    return ASIData[y][index];
                }
                else
                {
                    index -= ASIData[y].Count;
                }
            }
            return null;
        }

        public int IndexOf(StoZoo.Dog.StockData.ASI asi)
        {
            int index = 0;
            foreach (int y in ASIData.Keys)
            {
                if (asi.dateTime.Year == y || y == 0)
                {
                    int i = ASIData[y].IndexOf(asi);
                    if (i >= 0)
                    {
                        return index + i;
                    }
                    else
                    {
                        return -1;
                    }
                }
                index += ASIData[y].Count;
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
                int m = M;
                int n = N;

                if (Globe.DeriveValues.ContainsKey("_Y_" + Name + "_M"))
                    m = (int)Globe.DeriveValues["_Y_" + Name + "_M"];
                if (Globe.DeriveValues.ContainsKey("_Y_" + Name + "_N"))
                    n = (int)Globe.DeriveValues["_Y_" + Name + "_N"];
                if (Globe.DeriveValues.ContainsKey("_Y_" + Name + "_M") || Globe.DeriveValues.ContainsKey("_Y_" + Name + "_N") || need)
                {
                    ASIData.Clear();
                    List<int> years = baseline.Years;
                    for (int i = 0; i < years.Count; i++)
                    {
                        List<StoZoo.Dog.StockData.KLine> klines = new List<StoZoo.Dog.StockData.KLine>();
                        klines.AddRange(baseline.GetKlineYear(years[i]));

                        if (i > 0)
                            klines.InsertRange(0, baseline.GetKlineYear(years[i - 1]).TakeLast(m));

                        ASIData.Add(years[i], StoZoo.Dog.StockData.ASI.Count(klines, m, n));
                    }

                    OffsetIndex = -1;
                }

                if (ASIData.Count < 1) return false;

                var test = ASIData[0].ToList();
                return true;
            }
            else if (environment.otherKlines.ContainsKey(Type))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override double GetValue(params object[] param)
        {
            string target = (string)param[0];
            int index = 0;
            if (param.Length > 1)
                int.TryParse(param[1].ToString(), out index);

            if (OffsetIndex < 0)
            {
                if (Type == "day")
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
                    }
                    if (baseline.thisIndex < 0) return double.NaN;

                    var thisma = GetIndicator(Environment.thisDate.Date + Environment.thisTime);
                    if (thisma == null) return double.NaN;
                    int arrayindex = IndexOf(thisma);
                    if (arrayindex < 0) return double.NaN;
                    OffsetIndex = baseline.thisIndex - arrayindex;
                }
            }

            StoZoo.Dog.StockData.ASI resasi = null;
            if (Type == "day")
            {
                if (index == 0)
                {
                    if (baseline.thisIndex - OffsetIndex < 0) return double.NaN;
                    resasi = StoZoo.Dog.StockData.ASI.Count(GetIndicator(baseline.thisIndex - OffsetIndex), Environment.realDayKline);
                }
                else
                {
                    if (baseline.thisIndex - OffsetIndex - index < 0) return double.NaN;
                    resasi = GetIndicator(baseline.thisIndex - OffsetIndex - index);
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
                    resasi = StoZoo.Dog.StockData.ASI.Count(lastvalue, Environment.thisLC5Kline);
                }
                else
                {
                    if (baseline.thisIndex - OffsetIndex - index < 0) return double.NaN;
                    resasi = GetIndicator(baseline.thisIndex - OffsetIndex - index);
                    if (resasi == null) return double.NaN;
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
                    resasi = StoZoo.Dog.StockData.ASI.Count(lastvalue, Environment.thisLC5Kline);
                }
                else
                {
                    if (baseline.thisIndex - OffsetIndex - index < 0) return double.NaN;
                    resasi = GetIndicator(baseline.thisIndex - OffsetIndex - index);
                    if (resasi == null) return double.NaN;
                }
            }

            switch (target.ToLower())
            {
                case "asi":
                    return resasi.Value;
                case "asit":
                    return resasi.ASIT;
                case "value":
                    return resasi.Value;
            }

            return double.NaN;
        }
    }
}
