using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZ.OperationLibrary.Indicator;

namespace TZ.OperationLibrary.Operation.StockOperation
{
    class HoldingHP: Operation
    {
        public Run.RunEnvironment environment = null;
        public HoldingHP(Run.RunEnvironment environment)
        {
            this.environment = environment;
            this.isLC = true;
        }
        public override double Run(params object[] paras)
        {
            double res = double.NegativeInfinity;
            int offset = 0;
            if (paras.Length > 0)
            {
                if (paras[0] is double || paras[0] is int)
                    int.TryParse(paras[0].ToString(), out offset);
            }
            var baseline=  environment.baseKlines["day"] as KLine;
            if (offset == 0)
            {
                for (int i = environment.BuyIndex; i < baseline.thisIndex; i++)
                {
                    double hp = baseline.GetKline(i).highPrice;
                    if (hp > res) res = hp;
                }
                if (offset == 0)
                {
                    if (environment.realDayKline.highPrice > res)
                        res = environment.realDayKline.highPrice;
                }
            }
            else {
                for (int i = environment.BuyIndex; i <= baseline.thisIndex - offset ; i++)
                {
                    double hp = baseline.GetKline(i).highPrice;
                    if (hp > res) res = hp;
                }
            }
            return res;
        }

    }
}
