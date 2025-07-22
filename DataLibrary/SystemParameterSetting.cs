using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.DataLibrary
{
    public class SystemParameterSetting
    {
        public string UUID { set; get; }
        public string Name { set; get; }
        public string Type { set; get; }
        public List<double> Settings { get; set; }

    }
}
