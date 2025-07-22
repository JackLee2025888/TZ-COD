using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoZoo.Dog
{
    public class OperationExchange
    {
        public string TaskKey { set; get; }

        public string OperationKey { set; get; }

        public List<KeyValue> Parameters { set; get; }

        public List<KeyValue> Results { set; get; }
    }
}
