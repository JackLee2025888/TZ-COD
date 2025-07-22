using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoZoo.Dog.StockData
{
    public class CashFlow
    {
        public DateTime dateTime { set; get; }

        public int T1 { set; get; }

        public int T2 { set; get; }

        public int T3 { set; get; }

        public int T4 { set; get; }



        public byte[] GetByte()
        {
            byte[] result = new byte[32];
            int dateInt = dateTime.Year * 10000 + dateTime.Month * 100 + dateTime.Day;

            BitConverter.GetBytes(dateInt).CopyTo(result, 0);
            BitConverter.GetBytes(T1).CopyTo(result, 4);
            BitConverter.GetBytes(T2).CopyTo(result, 8);
            BitConverter.GetBytes(T3).CopyTo(result, 12);
            BitConverter.GetBytes(T4).CopyTo(result, 16);

            return result;
        }

        public static CashFlow ReadByte(byte[] data)
        {
            if (data.Length != 32)
                return null;

            try
            {
                CashFlow result = new CashFlow();
                int dateInt = BitConverter.ToInt32(data, 0);

                result.dateTime = new DateTime(dateInt / 10000, dateInt % 10000 / 100, dateInt % 10000 % 100, 0, 0, 0);
                result.T1 = BitConverter.ToInt32(data, 4);
                result.T2 = BitConverter.ToInt32(data, 8);
                result.T3 = BitConverter.ToInt32(data, 12);
                result.T4 = BitConverter.ToInt32(data, 16);
                return result;
            }
            catch
            {
                return null;
            }


        }

        public static CashFlow GetLast(string path)
        {
            DataFile df = new DataFile();
            if (!df.Open(path, -32))
                return null;

            byte[] data = df.Read(32);
            if (data == null)
            {
                df.Close();
                return null;
            }
            CashFlow line = ReadByte(data);
            df.Close();
            return line;
        }

        public static List<CashFlow> ReadFile(string path, int offset = 0, int count = 0)
        {
            DataFile df = new DataFile();
            if (!df.Open(path, offset))
                return null;

            List<CashFlow> result = new List<CashFlow>();
            while (true)
            {
                byte[] data = df.Read(32);
                if (data == null)
                {
                    df.Close();
                    return result;
                }
                CashFlow line = ReadByte(data);
                if (line != null)
                    result.Add(line);
                if (count > 0 && result.Count >= count)
                {
                    df.Close();
                    return result;
                }
            }
        }
        public static bool SaveFile(List<CashFlow> list, string path, int offset = 0, bool append = false)
        {
            DataFile df = new DataFile();
            if (!df.Open(path, offset, append))
                return false;

            foreach (CashFlow item in list)
                df.Write(item.GetByte());

            return df.Cut();
        }
    }
}
