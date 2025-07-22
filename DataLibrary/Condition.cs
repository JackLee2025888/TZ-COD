using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.DataLibrary
{
    public class Condition
    {
        public string UUID { set; get; }
        public string Name { set; get; }
        public string Formula { set; get; }

        public bool isLC { set; get; }
    }
}
