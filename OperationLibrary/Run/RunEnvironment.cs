using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TZ.OperationLibrary.Run
{
    public class RunEnvironment
    {
        public RunEnvironment()
        {
            baseKlines.Add("day", new Indicator.KLine("day"));
            baseKlines.Add("lc5", new Indicator.KLine("lc5"));
        }

        public bool LoadStock(StoZoo.Dog.StockData.Stock stock)
        {
            this.stock = stock;
            if (!baseKlines["day"].LoadData(stock, this)) return false;
            if (!baseKlines["lc5"].LoadData(stock, this)) return false; 

            foreach (Indicator.Indicator indicator in otherKlines.Values)
                if (!indicator.LoadData(stock, this)) return false;
            if (!LoadIndicator(stock)) return false;


            DoDates = ((Indicator.KLine)baseKlines["day"]).GetKlineRange(BeginDate.Date, EndDate.Date).Select(p => p.dateTime.Date).ToList();
            if (DoDates.Count <= 0) return false;

            StoZoo.Dog.StockData.KLine firstKline = ((Indicator.KLine)baseKlines["day"]).GetKline(DoDates[0]);
            if (firstKline == null) return false;

            this.thisDate = DoDates[0];
            //this.thisDateIndex = ((Indicator.KLine)baseKlines["day"]).IndexOf(firstKline);

            return true;
        }

        public bool LoadIndicator(StoZoo.Dog.StockData.Stock stock, bool need = true)
        {
            foreach (Indicator.Indicator indicator in Indicators.Values)
                if (!indicator.LoadData(stock, this)) return false;

            return true;
        }

        public List<string> RUN(StoZoo.Dog.StockData.Stock stock)
        {
            Indicator.KLine DayLines = (Indicator.KLine)baseKlines["day"];
            // Dictionary <int, List<StoZoo.Dog.StockData.KLine>> LC5Lines = ((Indicator.KLine)baseKlines["lc5"]).LC5Klines;
            Indicator.KLine LC5Lines = (Indicator.KLine)baseKlines["lc5"];
            Indicator.KLine WeekLines = null, MonthLines = null;
            if(otherKlines.ContainsKey("week"))
                WeekLines=(Indicator.KLine)otherKlines["week"];
            if(otherKlines.ContainsKey("month"))
                MonthLines = (Indicator.KLine)otherKlines["month"];

            Indicator.KLine LC15Lines = null;
            if (otherKlines.ContainsKey("lc15"))
                LC15Lines = (Indicator.KLine)otherKlines["lc15"];

            BuyPrice = 0;
            List<string> result = new List<string>();
            List<string> BuyResult = new List<string>();

            foreach (DateTime tDate in DoDates)
            {
                thisDate = tDate;
                thisDayKline = DayLines.GetKline(tDate.Date);
                if (thisDayKline == null) continue;
                DayLines.thisIndex = DayLines.IndexOf(thisDayKline);
                if (DayLines.thisIndex < 150) continue;
                realDayKline = new StoZoo.Dog.StockData.KLine()
                {
                    dateTime = tDate,
                    beginPrice = thisDayKline.beginPrice,
                    lowPrice = float.MaxValue,
                    highPrice = float.MinValue
                };
                //if (!LC5Lines.ContainsKey(tDate.Year)) continue;
                //List<StoZoo.Dog.StockData.KLine> thisyearLc5 = LC5Lines[tDate.Year];

                if (MonthLines != null)
                {
                  var thismonthline=  MonthLines.GetKline(tDate.Date);
                    if (thismonthline != null)
                        MonthLines.thisIndex = MonthLines.IndexOf(thismonthline);
                }
                if (WeekLines != null)
                {
                    var thisweekline = WeekLines.GetKline(tDate.Date);
                    if (thisweekline != null)
                        WeekLines.thisIndex = WeekLines.IndexOf(thisweekline);
                }


                List<StoZoo.Dog.StockData.KLine> todayLc5 = LC5Lines.GetKlineDay(thisDate.Date).ToList();


                if (thisDate.Date == new DateTime(2022,11,1))
                {
                    int i = 0;
                }


                if (!CheckFormula(BuyFormulaDays)) continue;

                //for (TimeSpan tTime = new TimeSpan(9, 35, 0); tTime <= new TimeSpan(15, 0, 0); tTime = tTime + new TimeSpan(0, 5, 0))
                foreach (var tLC5 in todayLc5)
                {
                    thisTime = tLC5.dateTime.TimeOfDay;
                    thisLC5Kline = tLC5;
                    LC5Lines.thisIndex = -1;
                    if (LC15Lines != null)
                    {
                        LC15Lines.thisIndex = -1;
                        
                    }
                    realDayKline.highPrice = Math.Max(realDayKline.highPrice, tLC5.highPrice);
                    realDayKline.lowPrice = Math.Min(realDayKline.lowPrice, tLC5.lowPrice);
                    realDayKline.endPrice = tLC5.endPrice;
                    realDayKline.dealAmount += tLC5.dealAmount;
                    realDayKline.dealVolume += tLC5.dealVolume;

                    //if (LC15Lines != null)
                    //{
                    //    var thislc15line = LC15Lines.GetKline(tLC5.dateTime);
                    //    if (thislc15line != null)
                    //        LC15Lines.thisIndex = LC15Lines.IndexOf(thislc15line);
                    //}
                
                    if (tLC5.dateTime.TimeOfDay == new TimeSpan(13, 10, 0))
                    {
                        int i = 0;
                    }

                    //5分钟线判断
                    if (!CheckFormula(BuyFormulaLC5s)) continue;
                    //确定买入
                    BuyPrice = tLC5.endPrice;
                    BuyIndex = DayLines.thisIndex;
                    BuyDate = tLC5.dateTime;

                    BuyResult = new List<string>();
                    foreach (string indicator in setting.Indicator1)
                    {
                        double v = GetIndicatorValue(indicator);
                        BuyResult.Add(double.IsNaN(v) ? "" : v.ToString("0.00"));
                    }

                    #region 卖出
                    {

                        for (int saleindex = BuyIndex + 1; saleindex < DayLines.Count; saleindex++)
                        {

                            thisDayKline = DayLines.GetKline(saleindex);
                            thisDate = thisDayKline.dateTime;
                            if (thisDayKline == null) continue;
                            DayLines.thisIndex = saleindex;
                            if (DayLines.thisIndex < 150) continue;
                            realDayKline = new StoZoo.Dog.StockData.KLine()
                            {
                                dateTime = thisDate,
                                beginPrice = thisDayKline.beginPrice,
                                lowPrice = float.MaxValue,
                                highPrice = float.MinValue
                            };
                            todayLc5 = LC5Lines.GetKlineDay(thisDate.Date).ToList();

                            if (thisDate.Date == new DateTime(2022,11, 3))
                            {
                                int i = 0;
                            }



                            //日线判断
                            if (!CheckFormula(SaleFormulaDays)) continue;

                            //for (TimeSpan tTime = new TimeSpan(9, 35, 0); tTime < new TimeSpan(15, 0, 0); tTime = tTime + new TimeSpan(0, 5, 0))

                            foreach (var sLC5 in todayLc5)
                            {
                                var time = new TimeSpan(11, 20, 0);
                                if (sLC5.dateTime.TimeOfDay == time)
                                {
                                    int i = 0;
                                }
                                if (sLC5.dateTime.TimeOfDay > new TimeSpan(14, 55, 0))
                                    continue;

                                thisTime = sLC5.dateTime.TimeOfDay;
                                thisLC5Kline = sLC5;
                                LC5Lines.thisIndex = -1;
                                if (LC15Lines != null)
                                {
                                    LC15Lines.thisIndex = -1;
                                }
                                realDayKline.highPrice = Math.Max(realDayKline.highPrice, sLC5.highPrice);
                                realDayKline.lowPrice = Math.Min(realDayKline.lowPrice, sLC5.lowPrice);
                                realDayKline.endPrice = sLC5.endPrice;
                                realDayKline.dealAmount += sLC5.dealAmount;
                                realDayKline.dealVolume += sLC5.dealVolume;

                                //5分钟线判断
                                if (!CheckFormula(SaleFormulaLC5s)) continue;


                                //确定买卖出
                                SalePrice = sLC5.endPrice;
                                SaleIndex = DayLines.thisIndex;
                                SaleDate = sLC5.dateTime;


                                List<string> thisResult = new List<string>();

                                thisResult.Add(stock.Name);
                                thisResult.Add(stock.Code);
                                thisResult.Add(BuyDate.ToString("yyyy-MM-dd HH:mm"));
                                thisResult.Add(BuyPrice.ToString("0.00"));
                                thisResult.Add(SaleDate.ToString("yyyy-MM-dd HH:mm"));
                                thisResult.Add(SalePrice.ToString("0.00"));
                                thisResult.Add(((SalePrice - BuyPrice) / BuyPrice).ToString("0.00%"));
                                thisResult.Add((SaleIndex - BuyIndex).ToString());


                                thisResult.AddRange(BuyResult);
                                foreach (string indicator in setting.Indicator2)
                                {
                                    double v = GetIndicatorValue(indicator);
                                    thisResult.Add(double.IsNaN(v) ? "" : v.ToString("0.00"));
                                }


                                result.Add(string.Join(',', thisResult));

                                BuyPrice = 0;
                                break;
                            }

                            if (BuyPrice == 0)
                                break;
                        }
                    }


                    #endregion 
                    break;
                }
                
            }

            return result;
        }

        private bool CheckFormula(List<(Formula.Formula, Formula.Formula)> Formulas)
        {
            foreach ((Formula.Formula tFormula, Formula.Formula tDoFormula) in Formulas)
                if ((tDoFormula == null || tDoFormula.Value > 0) && tFormula.Value <= 0)
                    return false;

            return true;
        }
        private double GetIndicatorValue(string str)
        {
            string[] names = Globe.IndicatorSystemParameter(str);
            if (names == null)
                return double.NaN;


            if (names[1].ToLower() == "kline")
            {
                if (names[0] == "day" || names[0] == "lc5")
                    return baseKlines[names[0]].GetValue(names[3], 0);
                else
                {
                    if (otherKlines.ContainsKey(names[0]))
                        return otherKlines[names[0]].GetValue(names[3], 0);
                    else
                        return double.NaN;
                }
            }
            else
            {
                if (Indicators.ContainsKey(names[0] + "_" + names[1]))
                    return Indicators[names[0] + "_" + names[1]].GetValue(names[3], 0);
                else
                    return double.NaN;
            }
        }

        DataLibrary.RunSetting setting = null;
        DataLibrary.Policy policy = null;


        public bool LoadBaseInfo(DataLibrary.RunSetting setting, DataLibrary.Policy policy)
        {
            this.setting = setting;
            this.policy = policy;


            this.BeginDate = setting.BeginDate.Date;
            this.EndDate = setting.EndDate.Date;

            foreach (DataLibrary.Policy.ConditionGroup group in policy.BuyGroup)
            {
                (bool isLC, Formula.Formula tFormula, Formula.Formula tDoFormula) = GetFormula(group);
                if (tFormula == null)
                    return false;

                if (isLC)
                    BuyFormulaLC5s.Add((tFormula, tDoFormula));
                else
                    BuyFormulaDays.Add((tFormula, tDoFormula));

            }

            foreach (DataLibrary.Policy.ConditionGroup group in policy.SaleGroup)
            {
                (bool isLC, Formula.Formula tFormula, Formula.Formula tDoFormula) = GetFormula(group);
                if (tFormula == null)
                    return false;

                if (isLC)
                    SaleFormulaLC5s.Add((tFormula, tDoFormula));
                else
                    SaleFormulaDays.Add((tFormula, tDoFormula));

            }

            return true;
        }

        private (bool, Formula.Formula, Formula.Formula) GetFormula(DataLibrary.Policy.ConditionGroup group)
        {
            bool isLC = false;
            int pSize = 0;
            List<DataLibrary.Condition> conditions = Globe.UserSettings.Conditions.Values.Where(p => group.Conditions.Contains(p.UUID)).ToList();

            List<Formula.Formula> Formulas = new List<Formula.Formula>();
            foreach (DataLibrary.Condition condistion in conditions)
            {
                Formula.Formula thisFormula = new Formula.Formula.FormulaReader().Read(condistion.Formula, this);
                if (thisFormula == null)
                    return (false, null, null);
                Formulas.Add(thisFormula);
                if (thisFormula.isLC) isLC = true;
            }

            Formula.Formula tFormula = Formulas[0];
            for (int i = 1; i < Formulas.Count; i++)
                tFormula = new Formula.Formula()
                {
                    _operation = Operation.Operation.GetOperation(group.GroupType == 1 ? "|" : "&", this, new List<object>() { tFormula, Formulas[i] }),
                    isLC = false,
                    Parameters = new List<object>() { tFormula, Formulas[i] }
                };

            Formula.Formula tDoFormula = null;
            if (group.DoCondition != "0")
            {
                tDoFormula = new Formula.Formula.FormulaReader().Read(group.DoCondition, this);
                if (tDoFormula == null)
                    return (false, null, null);
            }

            return (isLC, tFormula, tDoFormula);

        }


        public List<(Formula.Formula, Formula.Formula)> BuyFormulaDays = new List<(Formula.Formula, Formula.Formula)>();
        public List<(Formula.Formula, Formula.Formula)> BuyFormulaLC5s = new List<(Formula.Formula, Formula.Formula)>();

        public List<(Formula.Formula, Formula.Formula)> SaleFormulaDays = new List<(Formula.Formula, Formula.Formula)>();
        public List<(Formula.Formula, Formula.Formula)> SaleFormulaLC5s = new List<(Formula.Formula, Formula.Formula)>();


        public Dictionary<string, Indicator.Indicator> baseKlines = new Dictionary<string, Indicator.Indicator>();

        public Dictionary<string, Indicator.Indicator> otherKlines = new Dictionary<string, Indicator.Indicator>();

        public Dictionary<string, Indicator.Indicator> Indicators = new Dictionary<string, Indicator.Indicator>();

        public DateTime BeginDate;

        public DateTime EndDate;


        #region 当前模拟状态数据
        public StoZoo.Dog.StockData.Stock stock;

        public List<DateTime> DoDates;

        public DateTime thisDate;

        //public int thisDateIndex;
        //public int thisLC5Index;

        //public int thisWeekIndex;
        //public int thisMonthIndex;


        public TimeSpan thisTime;

        public StoZoo.Dog.StockData.KLine thisDayKline = null;

        public StoZoo.Dog.StockData.KLine thisLC5Kline = null;

        public StoZoo.Dog.StockData.KLine realDayKline = null;
        #endregion 


        #region 当前买卖数据
        public float BuyPrice;

        public int BuyIndex;

        public DateTime BuyDate;

        public float SalePrice;

        public int SaleIndex;

        public DateTime SaleDate;
        #endregion 


    }
}
