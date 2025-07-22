using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoZoo.Dog
{
    public  class TaskExchange
    {
        public string uuid { set; get; }

        public string batteryId { set; get; }
        public string userId { set; get; }

        public string   beginTime { set; get; }
        public string endTime { set; get; }
        
        public List<TaskResult> Results { set; get; }


        public class TaskResult
        {
            public Dictionary<string, double> Parameters { set; get; }

            public Dictionary<string, double> Results { set; get; }

        }
    }
}
