using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.OperationLibrary.Operation
{
   public  class Pow:Operation 
    {
        public override double Run(params object[] paras)
        {
            double[] values = new double[paras.Length];
            for (int i = 0; i < paras.Length; i++)
                if (paras[i] is double || paras[i] is float || paras[i] is int)
                    values[i] = (double)paras[i];
                else if (paras[i] is Formula.Parameter)
                    values[i] = ((Formula.Parameter)paras[i]).Value;
                else if (paras[i] is Formula.Formula)
                    values[i] = ((Formula.Formula)paras[i]).Value;
            if (values.Length < 1) return 0;
            if (values.Length < 2) return values[0];

           
            return Math.Pow(values[0], values[1]);          
        }



    }
}
