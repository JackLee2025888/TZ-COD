using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.DataLibrary
{
    public class RunSetting
    {
        public DateTime BeginDate { set; get; }

        public DateTime EndDate { set; get; }

        public bool is00 { set; get; }

        public bool is60 { set; get; }

        public bool is30 { set; get; }

        public bool is68 { set; get; }

        public bool isST { set; get; }

        public bool isCST { set; get; }

        public List<string> CST { set; get; }

        public bool isProcess { set; get; }

        public int Process { set; get; }

        public List<string> Indicator1 { set; get; }
        public List<string> Indicator2 { set; get; }

        public string outPath { set; get; }

        public string DeriveAddress { set; get; }

        public string DeriveKey { set; get; }
        public bool DeriveYear { set; get; }
        public override bool Equals(object obj)
        {
            RunSetting other = obj as RunSetting;
            if (other == null)
                return false;

            if (BeginDate.Date != other.BeginDate.Date) return false;
            if (EndDate.Date != other.EndDate.Date) return false;
            if (is00 != other.is00) return false;
            if (is30 != other.is30) return false;
            if (is60 != other.is60) return false;
            if (is68 != other.is68) return false;
            if (isST != other.isST) return false;
            if (isCST != other.isCST) return false;
            if (outPath != other.outPath) return false;
            if (isProcess != other.isProcess) return false;

            if (isCST && string.Join(", ", CST) != string.Join(", ", other.CST)) return false;
            if (isProcess && Process != other.Process) return false;
            if (string.Join(", ", Indicator1) != string.Join(", ", other.Indicator1)) return false;
            if (string.Join(", ", Indicator2) != string.Join(", ", other.Indicator2)) return false;

            return true;
        }


    }
}
