using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TZ.DataLibrary;

namespace TZ.OperationLibrary
{
    public static class Globe
    {
        public static string LoadPath = Environment.CurrentDirectory;
        public static WebApiConnection WebAPI = new WebApiConnection();
        public static string SystemID = "";

       // public static List<Run.RunEnvironment> RunEnvironments = new List<Run.RunEnvironment>();
        public static UserSetting UserSettings = null;
        public static SystemSetting SystemSettings = new SystemSetting();

        public static Dictionary<string, Run.RunEnvironment> testMemory = new Dictionary<string, Run.RunEnvironment>();

        public static Dictionary<string, double> DeriveValues = new Dictionary<string, double>();

        public static Dictionary <string,List< StoZoo.Dog.StockData.KLine>> IndexLine =new Dictionary<string, List<StoZoo.Dog.StockData.KLine>>();
        public static Dictionary<string, List<StoZoo.Dog.StockData.Average>> IndexMALine = new Dictionary<string, List<StoZoo.Dog.StockData.Average>>();


        #region WEBAPI
        public static T GET<T>(params string[] address)
        {
            try
            {
                string url = SystemSettings.Server + "/api/" + string.Join("/", address);
                return WebAPI.getUrl<T>(url);
            }
            catch
            {
                return default(T);
            }
        }
        public static T POST<T>(object obj, params string[] address)
        {
            try
            {
                string url = SystemSettings.Server + "/api/" + string.Join("/", address);
                return WebAPI.postUrl<T>(url, obj);
            }
            catch
            {
                return default(T);
            }
        }

        public static T GETA<T>(string server, params string[] address)
        {
            try
            {
                string url = server + "/" + string.Join("/", address);
                return WebAPI.getUrl<T>(url);
            }
            catch
            {
                return default(T);
            }
        }
        public static T POST<T>(string server, object obj, params string[] address)
        {
            try
            {
                string url = server + "/" + string.Join("/", address);
                return WebAPI.postUrl<T>(url, obj);
            }
            catch
            {
                return default(T);
            }
        }

        #endregion 


        #region 数据转换


        public static T LoadFile<T>(string path)
        {
            try
            {

                Stream sm = new FileStream(path, FileMode.Open, FileAccess.Read);
                byte[] data = new byte[sm.Length];
                sm.Read(data, 0, data.Length);
                sm.Close();
                sm.Dispose();
                return StoZoo.Dog.Global.Deserialize<T>(data);
            }
            catch
            {
                return default(T);
            }

        }
        public static bool SaveFile(string path, object obj)
        {
            try
            {
                try
                {
                    if (File.Exists(path))
                        File.Delete(path);
                }
                catch { }

                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                byte[] data = StoZoo.Dog.Global.SerializeToByte(obj);

                Stream sm = new FileStream(path, FileMode.Create, FileAccess.Write);
                sm.Write(data, 0, data.Length);
                sm.Close();
                sm.Dispose();
                return true;
            }
            catch
            {
                return false;

            }
        }

        #endregion

        public static string GetSystemID()
        {
            string ID = "";
            try
            {
                ManagementClass MC = new ManagementClass("win32_processor");
                foreach (ManagementObject mo in MC.GetInstances())
                    ID += mo.Properties["ProcessorId"].Value.ToString();
            }
            catch
            {
            }
            //try
            //{
            //    ManagementClass MC = new ManagementClass("Win32_DiskDrive");
            //    foreach (ManagementObject mo in MC.GetInstances())
            //        ID += mo.Properties["SerialNumber"].Value.ToString();
            //}
            //catch
            //{
            //}

            if (ID == "")
                return "";
            else
                return GenerateMD5(ID);
        }

        public static string GenerateMD5(string txt)
        {

            using (MD5 mi = MD5.Create())
            {

                byte[] buffer = Encoding.Default.GetBytes(txt);

                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }

                return sb.ToString().ToUpper();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns>线类型，参数名称，参数类型，参数属性名</returns>
        public static string[] IndicatorSystemParameter(string str)
        {

            string LineType = "day";
            string ParameterName = "";
            string ParameterType = "";
            string Target = "value";
            string[] linetypepool = new string[] { "day", "lc5", "lc10", "lc15", "lc20", "lc30", "lc60","week","month" };

            string[] lines = str.Split(new char[] { '_' }, StringSplitOptions.None);

            if (lines.Length < 1) return null;
            if (lines.Length == 1)
            {
                LineType = "day";
                ParameterName = lines[0];
            }
            if (lines.Length == 2)
            {
                if (linetypepool.Contains(lines[0].ToLower()))
                {
                    LineType = lines[0].ToLower();
                    ParameterName = lines[1];
                }
                else
                {
                    LineType = "day";
                    ParameterName = lines[0];
                    Target = lines[1].ToLower();
                }
            }
            if (lines.Length == 3)
            {
                LineType = lines[0].ToLower();
                ParameterName = lines[1];
                Target = lines[2].ToLower();
            }


            if (!linetypepool.Contains(LineType.ToLower()))
                return null;
           

            if (ParameterName.ToLower() == "kline")
                ParameterType = "kline";
            else if (!Globe.UserSettings.SystemParameterSettings.ContainsKey(ParameterName))
            {
                return null;
            }
            else { ParameterType = Globe.UserSettings.SystemParameterSettings[ParameterName].Type.ToLower(); }

            string[] targets = null;
            switch (ParameterType)
            {
                case ("kline"):
                    targets = new string[] { "bp","sp", "ep", "lp", "hp","vol","amt","value","turnover" };
                    if (!targets.Contains(Target))
                        return null;
                    break;
                case ("kdj"):
                    targets = new string[] { "d", "k", "j", "value" };
                    if (!targets.Contains(Target))
                        return null;
                    break;
                case ("macd"):
                    targets = new string[] { "macd", "diff", "dea", "value" };
                    if (!targets.Contains(Target))
                        return null;
                    break;
                case ("asi"):
                    targets = new string[] { "asi", "asit", "value" };
                    if (!targets.Contains(Target))
                        return null;
                    break;
                case ("ma"):
                    if (Target.ToLower() != "value")
                        return null;
                    break;
                case ("rsi"):
                    if (Target.ToLower() != "value")
                        return null;
                    break;
                default:
                    return null;
            }
            return new string[] {LineType,ParameterName,ParameterType,Target };
        }



    }
}
