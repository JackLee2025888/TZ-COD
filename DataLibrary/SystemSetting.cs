using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.DataLibrary
{
    public class SystemSetting
    {

        public string Server { set; get; } = "http://localhost:8888";

        public string DataPath { set; get; } = @"E:\StoZooData";

        public string SecretKey { set; get; }

        public List<RunSetting> RunSettings { set; get; }
    }
}
