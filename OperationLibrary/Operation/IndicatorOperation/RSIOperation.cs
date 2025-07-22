using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.OperationLibrary.Operation
{
    public class RSIOperation : IndicatorOperation.IndicatorOperation
    {
        public RSIOperation(Run.RunEnvironment environment, string type, string parametername, string target, params double[] paras) : base(environment)
        {
            this.environment = environment;
            string key = type + "_" + parametername;
            if (environment != null)
            {
                if (this.environment.Indicators.ContainsKey(key))
                    indicator = this.environment.Indicators[key];
                else
                {
                    indicator = new Indicator.RSI(type,parametername , Convert.ToInt32(paras[0]));
                    if (environment.baseKlines.ContainsKey(type))
                    {
                        indicator.baseline = environment.baseKlines[type] as Indicator.KLine;
                    }
                    else if (environment.otherKlines.ContainsKey(type))
                    {
                        indicator.baseline = environment.otherKlines[type] as Indicator.KLine;
                    }
                    else
                    {
                        this.environment.otherKlines.Add(type, new Indicator.KLine(type));
                        indicator.baseline = environment.otherKlines[type] as Indicator.KLine;
                    }
                    this.environment.Indicators.Add(key, indicator);
                }

                this.target = target;
                if (type.ToLower() != "day")
                    isLC = true;
            }
        }

        public static RSIOperation Create(Run.RunEnvironment environment, string type, string parametername, string target, double[] paras, params object[] parameters)
        {
            if (parameters.Length > 0)
            {
                if (!(parameters[0] is double)) return null;
            }
            RSIOperation rsi = new RSIOperation(environment, type, parametername, target, paras);
            if (parameters.Length == 0 || (double)parameters[0] == 0.0)
                rsi.isLC = true;
            if (type.ToLower() != "day") rsi.isLC = true;
            return rsi;
        }
    }
}
