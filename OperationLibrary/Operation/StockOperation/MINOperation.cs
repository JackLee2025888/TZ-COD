using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.OperationLibrary.Operation.StockOperation
{
    public class MINOperation : Operation
    {
        public static MINOperation Create(params object[] paras)
        {
            if (paras.Length < 3) return null;
            if (!(paras[0] is Formula.Formula)) return null;
            if (!((paras[1] is double) || (paras[1] is Formula.Parameter))) return null;
            if (!((paras[2] is double) || (paras[2] is Formula.Parameter))) return null;

            int start =(int) (double ) paras[1];
            MINOperation min = new MINOperation();
            if (start == 0) min.isLC = true;
            return min;
        }

        public MINOperation()
        { }


        public override double Run(params object[] paras)
        {
            if (paras.Length < 3) return double.NaN;

            if (paras[0] is Formula.Formula)
            {
                int start = -1, end = -1;
                if ((paras[1] is double || paras[1] is int || paras[1] is Formula.Parameter) && (paras[2] is double || paras[2] is int || paras[2] is Formula.Parameter))
                {
                    if (paras[1] is double || paras[1] is int)
                        start = (int)(double)paras[1];
                    if (paras[1] is Formula.Parameter)
                        start = (int)(paras[1] as Formula.Parameter).Value;
                    if (paras[2] is double || paras[2] is int)
                        end = (int)(double)paras[2];
                    if (paras[2] is Formula.Parameter)
                        end = (int)(paras[2] as Formula.Parameter).Value;
                }
                if (start < 0 || end < 0) return double.NaN;
                if (end < start) return double.NaN;

                Formula.Formula formula = (Formula.Formula)paras[0];
                Formula.Formula condition = null;
                if (paras.Length > 3 && paras[3] is Formula.Formula)
                    condition = ((Formula.Parameter)paras[3])._formula;

                double res = double.PositiveInfinity;
                for (int i = start; i <= end; i++)
                {
                    double tiaojian = 1;
                    if (condition != null)
                    {
                        condition.SetBaseIndex(i);
                        tiaojian = condition.Value;
                    }
                    if (tiaojian > 0)
                    {
                        double val = formula._operation.Run(i);
                        if (val < res) res = val;
                    }

                }
                return res;
            }
            return double.NaN;
        }
    }
}
