using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StoZoo.Dog
{
    public static class Global
    {
        public static string LocalPath = "e:\\StoZooData";
         
        public static string GetMarketCode(string code)
        {
            switch (code.Substring(0, 1))
            {
                case "0":
                case "3":
                    return "SZ";
                case "6":
                    return "SH";
                default:
                    return "SZ";
            }

        }

        public delegate void SetLogDelegate(string str);


        #region 数据转换

        private static JsonSerializerOptions GetOption()
        {
            JsonSerializerOptions js = new JsonSerializerOptions();
            js.Converters.Add(new DoubleConverter());
            js.Converters.Add(new DateTimeConverter());

            return js;
        }

        public static string Serialize(object obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj, GetOption());
            }
            catch
            {
                return "";
            }
        }

        public static T Deserialize<T>(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(json, GetOption());
            }
            catch
            {
                return default(T);
            }
        }
        public static T ReDeserialize<T>(object obj)
        {
            try
            {
                return Deserialize<T>(Serialize(obj));
            }
            catch
            {
                return default(T);
            }
        }
        public static byte[] SerializeToByte(object obj)
        {
            try
            {
                return SerializeString(Serialize(obj));
            }
            catch
            {
                return new byte[0];
            }
        }

        public static T Deserialize<T>(byte[] bytes)
        {
            try
            {
                return Deserialize<T>(DeserializeToString(bytes));
            }
            catch
            {
                return default(T);
            }
        }

        public static byte[] SerializeString(string str)
        {
            try
            {
                return Encoding.UTF8.GetBytes(str);
            }
            catch
            {
                return new byte[0];
            }
        }

        public static string DeserializeToString(byte[] b)
        {
            try
            {
                return Encoding.UTF8.GetString(b);
            }
            catch
            {
                return "";
            }
        }

        public static T LoadFile<T>(string path)
        {
            try
            {

                Stream sm = new FileStream(path, FileMode.Open, FileAccess.Read);
                byte[] data = new byte[sm.Length];
                sm.Read(data, 0, data.Length);
                sm.Close();
                sm.Dispose();
                return Deserialize<T>(data);
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

                byte[] data = SerializeToByte(obj);

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
    }
}
