using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.OperationLibrary.Operation.StockOperation
{
    public  class FindDay:Operation
    {
        public static FindDay Create(params object[] paras)
        {
            if (paras.Length < 3) return null;
            if (!((paras[0] is Formula.Formula)|| (paras[0] is Formula.Parameter))) return null;
            if (!(paras[1] is double) || !(paras[2] is double)) return null;

            int start = (int)(double)paras[1];
            FindDay fd = new FindDay();
            if (start == 0) fd.isLC = true;
            return fd;
        }

        public FindDay()
        { }
        public override double Run(params object[] paras)
        {
            if (paras.Length < 3) return double.NaN;

            if ((paras[0] is Formula.Formula) || (paras[0] is Formula.Parameter))
            {
                if (paras[1] is double && paras[2] is double)
                {
                    int start = (int)(double)paras[1];
                    int end = (int)(double)paras[2];
                    if (end < start) return double.NaN;
                    Formula.Formula formula = null;
                    if (paras[0] is Formula.Formula)
                         formula = (Formula.Formula)paras[0];
                    else
                        formula =  ((Formula.Parameter)paras[0])._formula;

                    double res =double .NaN ;
                    for ( int i = start; i <= end; i++)
                    {
                        double tiaojian = 0;
                        if (formula != null)
                        {
                            formula.SetBaseIndex(i);
                            tiaojian = formula.Value;
                        }
                        if (tiaojian > 0)
                        {
                            res = i;
                            break;
                        }
                    }
                    return res;
                }
            }
            return double.NaN;
        }
    }
}
