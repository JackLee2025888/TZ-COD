using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TZ.OperationLibrary.Indicator
{
    public class KLine : Indicator
    {
        public KLine(string type) : base(type)
        {
            this.Type = type;
        }

        public int thisIndex { get; set; }

        public StockData.KLine GetKline(DateTime datetime)
        {
            if (KLineData.ContainsKey(datetime.Year))
            {
                if (Type == "lc15")
                {
                    DateTime dt = getCurrentMinute(datetime, 15);
                    return KLineData[datetime.Year].FirstOrDefault(p => p.dateTime == getCurrentMinute(datetime, 15));
                }
                else
                {
                    return KLineData[datetime.Year].FirstOrDefault(p => p.dateTime == datetime);
                }
            }
            else if (KLineData.ContainsKey(0))
            {
                if (Type == "day")
                {
                    return KLineData[0].FirstOrDefault(p => p.dateTime.Date == datetime.Date);
                }
                if (Type == "month")
                {
                    return KLineData[0].FirstOrDefault(p => p.dateTime.Year == datetime.Year && p.dateTime.Month == datetime.Month);
                }
                if (Type == "week")
                {
                    StoZoo.Dog.StockData.KLine week = null;
                    foreach (var kline in KLineData[0])
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
                else { return null; }
            }
            else
                return null;
        }

        // More methods implementations...

        public override double GetValue(params object[] param)
        {
            string target = (string)param[0];
            int index = 0;
            if (param.Length > 1)
            {
                if (param[1] is double || param[1] is int)
                    int.TryParse(param[1].ToString(), out index);
                if (param[1] is Formula.Parameter fp)
                    int.TryParse(fp.Value.ToString(), out index);
            }
            StoZoo.Dog.StockData.KLine tKline = null;

            if (index == 0)
            {
                var todayline = Environment.realDayKline;

                switch (Type)
                {
                    case "day":
                        tKline = Environment.realDayKline;
                        break;
                    case "week":
                        if (Environment.baseKlines.ContainsKey("day"))
                        {
                            Indicator indicator = Environment.baseKlines["day"];
                            tKline = StoZoo.Dog.StockData.KLine.CountWeek(todayline, (indicator as KLine).AllKLineData);
                        }
                        break;
                    case "month":
                        if (Environment.baseKlines.ContainsKey("day"))
                        {
                            Indicator indicator = Environment.baseKlines["day"];
                            tKline = StoZoo.Dog.StockData.KLine.CountMonth(todayline, (indicator as KLine).AllKLineData);
                        }
                        break;
                    case "lc15":
                        if (Environment.otherKlines.ContainsKey("lc15"))
                        {
                            Indicator indicator = Environment.otherKlines["lc15"];
                            if (Environment.thisLC5Kline.dateTime.Minute % 15 != 0)
                                tKline = StoZoo.Dog.StockData.KLine.CountMinutes(Environment.thisLC5Kline, (indicator as KLine).KLineData, 15);
                            else
                            {
                                if (thisIndex == -1)
                                {
                                    var thiskline = GetKline(Environment.thisLC5Kline.dateTime);
                                    thisIndex = IndexOf(thiskline);
                                }
                                if (thisIndex < 0) return double.NaN;
                                index = thisIndex - index;
                                if (index < 0) return double.NaN;
                                tKline = GetKline(index);
                            }
                        }
                        break;
                    case "lc5":
                        tKline = Environment.thisLC5Kline;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (Type == "lc5")
                {
                    if (thisIndex == -1)
                    {
                        thisIndex = IndexOf(Environment.thisLC5Kline);
                    }
                    if (thisIndex < 0) return double.NaN;
                    index = thisIndex - index;
                    if (index < 0) return double.NaN;
                    tKline = GetKline(index);
                }
                else if (Type == "lc15")
                {
                    if (thisIndex == -1)
                    {
                        var thiskline = GetKline(Environment.thisLC5Kline.dateTime);
                        if (thiskline == null) return double.NaN;
                        thisIndex = IndexOf(thiskline);
                    }
                    if (thisIndex < 0) return double.NaN;
                    index = thisIndex - index;
                    if (index < 0) return double.NaN;
                    tKline = GetKline(index);
                }
                else
                {
                    index = thisIndex - index;
                    if (index < 0) return double.NaN;
                    tKline = KLineData[0][index];
                }
            }

            switch (target.ToLower())
            {
                case "lp":
                    return tKline.lowPrice;
                case "hp":
                    return tKline.highPrice;
                case "bp":
                case "sp":
                    return tKline.beginPrice;
                case "ep":
                    return tKline.endPrice;
                case "amt":
                    return tKline.dealAmount;
                case "vol":
                    return tKline.dealVolume;
                case "turnover":
                    return tKline.dealTurnover;
                case "value":
                    return tKline.endPrice;
            }
            return 0;
        }

        public override bool LoadData(Stock stock, Run.RunEnvironment environment = null, bool need = true)
        {
            this.Environment = environment;
            List<ExRight> exRights = ExRight.ReadFile(Path.Combine(Globe.SystemSettings.DataPath, "ExRight", stock.MarketCode, stock.Code + ".ert"));

            if (Type == "day")
            {
                string path = Path.Combine(Globe.SystemSettings.DataPath, Type, stock.MarketCode, stock.Code + ".day");
                var res = StoZoo.Dog.StockData.KLine.ReadFile(path);
                if (res == null || res.Count == 0)
                {
                    return false;
                }
                // KLineData = res.Where(p => p.dateTime.Date >= environment.BeginDate.Date && p.dateTime.Date <= environment.EndDate.Date).ToList();
                AllKLineData = res;
                //var allkline = new Dictionary<int, List<StoZoo.Dog.StockData.KLine>>();
                //allkline.Add(0, res);
                
                //Environment.otherKlines.Add("alldaykline", new KLine("alldaykline") { KLineData = allkline });

                var firstday = res.FirstOrDefault(p => p.dateTime.Date >= environment.BeginDate);
                int fdindex = res.IndexOf(firstday);
                var lastday = res.LastOrDefault(p => p.dateTime.Date <= environment.EndDate);
                int ldindex = res.IndexOf(lastday);
                fdindex = fdindex - 200;
                if (fdindex < 0) fdindex = 0;
                ldindex += 100;
                if (ldindex > res.Count - 1) ldindex = res.Count - 1;
                KLineData.Clear();
                KLineData.Add(0, res.Skip(fdindex).Take(ldindex - fdindex + 1).ToList());

                if (exRights != null) ExRight.countExRight(KLineData[0], exRights);
                if (KLineData.Count < 1) return false;
                return true;
            }
            else if (Type == "week")
            {
                if (environment.baseKlines .ContainsKey("day"))
                {
                    Indicator indicator = environment.baseKlines["day"];
                    KLineData.Clear();
                    var week = StoZoo.Dog.StockData.KLine.CountWeek(((indicator as KLine).AllKLineData));
                   // AllKLineData = (indicator as KLine).AllKLineData;
                    KLineData.Add(0, week);
                    OffsetIndex = -1;
                    return true;
                }
                return false;
            }
            else if (Type == "month")
            {
                if (environment.baseKlines.ContainsKey("day"))
                {
                    Indicator indicator = environment.baseKlines["day"];
                    KLineData.Clear();
                    var month = StoZoo.Dog.StockData.KLine.CountMonth((indicator as KLine).AllKLineData);
                   // AllKLineData = (indicator as KLine).AllKLineData;
                    KLineData.Add(0, month);
                    OffsetIndex = -1;
                    return true;
                }
                return false;
            }
            else if (Type == "lc15")
            {
                if (environment.baseKlines.ContainsKey("lc5"))
                {
                    Indicator indicator = environment.baseKlines["lc5"];

                    var lc5klinedata = (indicator as KLine).KLineData;
                    KLineData.Clear();
                    foreach (int key in lc5klinedata.Keys)
                    {
                        var lc15 = StoZoo.Dog.StockData.KLine.CountMinutes(lc5klinedata[key], 15);
                        KLineData.Add(key, lc15);
                    }
                    if (KLineData.Count == 0)
                        return false;
                    if (exRights != null)
                        foreach (int year in KLineData.Keys)
                            ExRight.countExRight(KLineData[year], exRights);
                    // AllKLineData = (indicator as KLine).AllKLineData;
                    OffsetIndex = -1;
                    return true;
                }
                return false;
            }
            else
            {
                int beginyear = (environment.baseKlines["day"] as KLine).KLineData[0].FirstOrDefault().dateTime.Year;
                int endyear = (environment.baseKlines["day"] as KLine).KLineData[0].Last().dateTime.Year;
                KLineData.Clear();
                for (int i = beginyear; i <= endyear; i++)
                {
                    string path = Path.Combine(Globe.SystemSettings.DataPath, Type, i.ToString(), "vipdoc", stock.MarketCode.ToLower(), "fzline", stock.MarketCode.ToLower() + stock.Code + ".lc5");
                    var lc5 = StoZoo.Dog.StockData.KLine.getLC5(path);
                    if (lc5 == null || lc5.Count == 0)
                        continue;
                    if (!KLineData.ContainsKey(i))
                        KLineData.Add(i, lc5);
                }
                if (KLineData.Count == 0)
                    return false;
                if (exRights != null)
                    foreach (int year in KLineData.Keys)
                        ExRight.countExRight(KLineData[year], exRights);
                return true;
            }

        }


     
    }
}
