using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.OperationLibrary.Operation.IndicatorOperation
{
   public  class IndicatorOperation:Operation
    {
        public Indicator.Indicator indicator = null;
        public Run.RunEnvironment environment = null;

        public  string target = "value";
        public IndicatorOperation( Run.RunEnvironment environment)
        {
            this.environment = environment;
        }
        public override double Run(params object[] paras)
        {
            int index = 0;
            if (paras.Length > 0)
            {
                if (paras[0] is double|| paras[0] is int)
                { int.TryParse(paras[0].ToString(), out index); }
                if (paras[0] is Formula.Parameter)
                    index = (int)(paras[0] as Formula.Parameter).Value;
            } 
            
            return indicator.GetValue(target, index+baseIndex);
        }
    }
}
