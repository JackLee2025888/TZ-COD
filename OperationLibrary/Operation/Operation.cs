using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZ.OperationLibrary.Operation.StockOperation;

namespace TZ.OperationLibrary.Operation
{
    public class Operation
    {

        public int baseIndex = 0;
        public bool isLC = false;
        public virtual double Run(params object[] paras)
        {
            //double[] values = new double[paras.Length];
            //for (int i = 0; i < paras.Length; i++)
            //    if (paras[i] is double || paras[i] is float || paras[i] is int)
            //        values[i] = (double)paras[i];
            //    else if (paras[i] is Formula.Parameter)
            //        values[i] = ((Formula.Parameter)paras[i]).Value;
            //    else if (paras[i] is Formula.Formula)
            //        values[i] = ((Formula.Formula)paras[i]).Value;
            //return RunValue(values);
            return 0;
        }
     


        //public virtual double RunValue(params double[] paras)
        //{
        //    return 0;
        //}

        public static Operation GetOperation(string str, Run.RunEnvironment environment, List<object> parameters)
        {
            Operation op = SymbolOperation(str, environment, parameters);
            if (op != null) return op;
            op = CalculateOperation(str, environment, parameters);
            if (op != null) return op;
            return  ParameterOperation(str, environment, parameters);
        }
        public static Operation SymbolOperation(string str, Run.RunEnvironment environment,List<object> parameters)
        {
            if (parameters.Count < 2) return null;
            switch (str.ToLower())
            {
                case "+":
                    return new Plus();
                case "-":
                    return new Minus();
                case "*":
                    return new Mutiply();
                case "/":
                    return new Divide();
                case ">":
                    return new Greater();
                case ">=":
                    return new GreaterEqual();
                case "<":
                    return new Less();
                case "<=":
                    return new LessEqual();
                case "=":
                    return new Equal();
                case "&":
                    return new Add();
                case "|":
                    return new Or();
                case "pow":
                    return new Pow();
            }return null;               
        }
        public static Operation CalculateOperation(string str, Run.RunEnvironment environment, List<object> parameters)
        {
            //if (parameters.Count < 3) return null;
            switch (str.ToLower())
            {               
                case "sum":
                    return SUMOperation.Create(parameters.ToArray());
                case "max":
                    return MAXOperation.Create(parameters.ToArray());
                case "min":
                    return MINOperation.Create(parameters.ToArray());
                case "average":
                    return AverageOperation.Create(parameters.ToArray());
                case "holdinghp":
                    return new HoldingHP(environment);
                case "buyprice":
                    return new BuyPriceOperation(environment);
                case "findday":
                    return FindDay.Create(parameters.ToArray());
                case "szindex":
                    return new SZIndex(environment);;                   
                case "szindexma":
                    return new SZIndexMA(environment); ;
                case "hsindex":
                    return new HSIndex(environment); ;
                case "hsindexma":
                    return new HSIndexMA(environment); ;
            }
            return null;
        }


        public static Operation ParameterOperation(string str, Run.RunEnvironment environment, List<object> parameters)
        {
            string[] names = Globe.IndicatorSystemParameter(str);
            if (names == null) return null;
            DataLibrary.SystemParameterSetting  settings=null;
            if (names[1].ToLower() == "kline")
            { }
            else if (Globe.UserSettings.SystemParameterSettings.ContainsKey(names[1]))
            {               
                settings = Globe.UserSettings.SystemParameterSettings[names[1]];
            }
            else { return null; }         

            switch (names [2].ToLower())
            {
                case ("kline"):              
                    return  KLineOperation.Create(environment, names [0], names[3], parameters.ToArray ()); ;
                case ("kdj"):                   
                        return  KDJOperation.Create(environment, names[0],names[1],names[3],settings. Settings.ToArray(), parameters.ToArray());
                case ("macd"):
                        return  MACDOperation.Create(environment, names[0],names[1], names[3], settings.Settings.ToArray(), parameters.ToArray()); 
                
                case ("ma"):                   
                        return  MAOperation.Create(environment, names[0],names[1], names[3], settings.Settings.ToArray(), parameters.ToArray());
            
                case ("rsi"):                   
                        return  RSIOperation.Create(environment, names[0],names [1], names[3], settings.Settings.ToArray(), parameters.ToArray());
                case ("asi"):
                    return ASIOperation.Create(environment, names[0], names[1], names[3], settings.Settings.ToArray(), parameters.ToArray());

                default:
                    return null;
            }
            return null;
        }
    }
}
