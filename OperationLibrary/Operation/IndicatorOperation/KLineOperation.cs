using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.OperationLibrary.Operation
{
   public  class KLineOperation:IndicatorOperation.IndicatorOperation
    {    
        public KLineOperation(Run.RunEnvironment environment,string type="day",string target="ep" ):base (environment )
        {
            this.environment = environment;
            if (environment != null)
            {              
                if (this.environment.baseKlines.ContainsKey(type))
                { indicator = this.environment.baseKlines[type]; }
                else
                {
                    if (this.environment.otherKlines.ContainsKey(type))
                        indicator = this.environment.otherKlines[type];
                    else
                    {
                        indicator = new Indicator.KLine(type);
                        this.environment.otherKlines.Add(type, indicator);
                    }
                }
                this.target = target;
            }
       
        }

        public static KLineOperation  Create(Run.RunEnvironment environment, string type, string target,  params object[] parameters)
        {
            if (parameters.Length > 0)
            {
                if (!((parameters[0] is double)||(parameters[0] is Formula.Parameter))) return null;
            }
            KLineOperation  kline = new KLineOperation(environment, type, target);
            if (parameters.Length == 0)
                kline.isLC = true;
            else
            {
                if (parameters[0] is double && (double)parameters[0] == 0.0)
                { kline.isLC = true; }
                //if (parameters[0] is Formula.Parameter)
                //{
                //    double test = ((Formula.Parameter)parameters[0]).Value; 
                //}
                if (parameters[0] is Formula.Parameter)
                    kline.isLC = true;
            }
            if (type.ToLower() != "day")
                kline.isLC = true;
            return kline;
        }

    }
}
