using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZ.OperationLibrary.Indicator;

namespace TZ.OperationLibrary.Operation.StockOperation
{
    class HSIndexMA : Operation
    {

        public Run.RunEnvironment environment = null;
        public HSIndexMA(Run.RunEnvironment environment)
        {
            this.environment = environment;
            this.isLC = false ;
        }
        public override double Run(params object[] paras)
        {
            List<StoZoo.Dog.StockData.Average> res = new List<StoZoo.Dog.StockData.Average>(); ;
            int count= 1;
            int offset = 0;
            //string target = (string)paras[0];
            if (paras.Length < 2) return double.NaN;
            if (paras.Length >1)
            {
                if (paras[0] is double || paras[0] is int)
                    int.TryParse(paras[0].ToString(), out count);
                if (paras[1] is double || paras[1] is int)
                    int.TryParse(paras[1].ToString(), out offset);
            }
            if (Globe.IndexMALine.ContainsKey("hsindexma"+count.ToString ()))
            {
                res = Globe.IndexMALine["hsindexma" + count.ToString()];
            }
            else if(Globe.IndexLine.ContainsKey("hsindex"))
            {
                var klines = Globe.IndexLine["hsindex"];
                res = StoZoo.Dog.StockData.Average.Count(klines, count);
                Globe.IndexMALine.Add("hsindexma" + count.ToString(), res);              
            }
            else
            {
                string path = Path.Combine(Globe.SystemSettings.DataPath, "Day", "SH", "000300.day");
                var klines = StoZoo.Dog.StockData.KLine.ReadFile(path);
                Globe.IndexLine.Add("hsindex", klines);
                res = StoZoo.Dog.StockData.Average.Count(klines, count);
                Globe.IndexMALine.Add("hsindexma" + count.ToString(), res);
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
            if (reskline != null) return reskline.Value;

          
            return double.NaN ;
        }

    }
}
