using StoZoo.Dog.StockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.OperationLibrary.Indicator
{
    public class Indicator
    {
        public string Type = "";
        public Run.RunEnvironment Environment = null;
        public int OffsetIndex = -1;
        public string Name = "";
        public KLine baseline = null;


        public Indicator(string type)
        {
            Type = type;
        }

        public virtual double GetValue(params object[] param)
        {
            return 0;
        }

        public virtual bool LoadData(Stock stock, Run.RunEnvironment environment = null, bool need = true)
        {
            return false;
        }
        public  DateTime getCurrentMinute(DateTime dt, int interval)
        {
            if (interval <= 5 || (interval % 5) != 0)
                return dt;
            DateTime newdt;
            int intkey = (int)Math.Ceiling(((double)dt.Minute) / ((double)interval));
            if (interval * intkey >= 60)
            {
                newdt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour+(int)Math.Floor ((double)interval *intkey )/60, (interval * intkey)%60, 0);
            }
            else 
              newdt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, interval * intkey, 0);
            return newdt;
        }
    }
}
