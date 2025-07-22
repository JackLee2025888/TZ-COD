using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZ.OperationLibrary.Indicator;

namespace TZ.OperationLibrary.Operation.StockOperation
{
    class SZIndex : Operation
    {

        public Run.RunEnvironment environment = null;
        public SZIndex(Run.RunEnvironment environment)
        {
            this.environment = environment;
            this.isLC = false ;
        }
        public override double Run(params object[] paras)
        {
            List<StoZoo.Dog.StockData. KLine> res = new List<StoZoo.Dog.StockData.KLine>(); ;
            int offset = 0;
            //string target = (string)paras[0];
            if (paras.Length >0)
            {
                if (paras[0] is double || paras[0] is int)
                    int.TryParse(paras[0].ToString(), out offset);
            }

            if (!Globe.IndexLine.ContainsKey("szindex"))
            {
                 string path = Path.Combine(Globe.SystemSettings.DataPath, "Day","SH",  "000001.day");
                 res = StoZoo.Dog.StockData.KLine.ReadFile(path); 
            }
            else
            {
                res = Globe.IndexLine["szindex"] ;
            }

            if (res == null || res.Count == 0)
            {
                return double.NaN;
            }
            var targetkline= res.Where(p => p.dateTime.Date == environment.thisDate).FirstOrDefault();
            if (targetkline == null) return double.NaN;        

            var reskline = targetkline;

            if (offset != 0)
            {
                int dateindex = res.IndexOf(targetkline);
                reskline = res[dateindex - offset];

            }
            if (reskline != null) return reskline.endPrice;

          
            return double.NaN ;
        }

    }
}
