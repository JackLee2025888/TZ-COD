using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.OperationLibrary.Operation.StockOperation
{
    class BuyPriceOperation : Operation
    {

        public Run.RunEnvironment environment = null;
        public BuyPriceOperation(Run.RunEnvironment environment)
        {
            this.environment = environment;            
        }
        public override double Run(params object[] paras)
        {
            return environment.BuyPrice;
        }


    }
}
