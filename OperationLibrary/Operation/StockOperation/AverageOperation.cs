using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.OperationLibrary.Operation.StockOperation
{
    public class AverageOperation : Operation
    {

        public static AverageOperation Create(params object[] paras)
        {
            if (paras.Length < 3) return null;
            if (!(paras[0] is Formula.Formula)) return null;
            if (!(paras[1] is double) || !(paras[2] is double)) return null;

            int start = (int)(double)paras[1];
            AverageOperation ave = new AverageOperation();
            if (start == 0) ave.isLC = true;
            return ave;
        }

        public AverageOperation()
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
                Formula.Formula formula = (Formula.Formula)paras[0];

                Formula.Formula condition = null;
                if (paras.Length > 3 && paras[3] is Formula.Parameter)
                    condition = ((Formula.Parameter)paras[3])._formula;

                double res = 0;
                for (int i = start; i <= end; i++)
                {
                    double tiaojian = 1;
                    if (condition != null)
                    {
                        condition.SetBaseIndex(i);
                        tiaojian = condition.Value;
                    }
                    if (tiaojian > 0)
                        res += formula._operation.Run(i);
                }
                return res / (end - start + 1);

            }
            return double.NaN;
        }
    }
}
