using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.DataLibrary
{
    public class Policy
    {
        public string UUID { set; get; }
        public string Name { set; get; }
        public List<ConditionGroup> BuyGroup { set; get; }
        public List<ConditionGroup> SaleGroup { set; get; }


        public class ConditionGroup
        {

            public string Name { set; get; }
            public string DoCondition { set; get; }
            public int GroupType { set; get; }
            public List<string> Conditions { set; get; }
        }
    }
}
