using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoZoo.Dog.StockData
{
    public class ExRight
    {
        public DateTime dateTime { set; get; }
        public float pxbl { set; get; } = 0;  //派息比例
        public float sgbl { set; get; } = 0;  //送股比例
        public float cxbl { set; get; } = 0;  //除息比例
        public float pgbl { set; get; } = 0;  //配股比例
        public float pgjg { set; get; } = 0;  //配股价格
        public float pghg { set; get; } = 0;



        public byte[] GetByte()
        {
            byte[] result = new byte[32];
            int dateInt = dateTime.Year * 10000 + dateTime.Month * 100 + dateTime.Day;

            BitConverter.GetBytes(dateInt).CopyTo(result, 0);
            BitConverter.GetBytes(pxbl).CopyTo(result, 4);
            BitConverter.GetBytes(sgbl).CopyTo(result, 8);
            BitConverter.GetBytes(cxbl).CopyTo(result, 12);
            BitConverter.GetBytes(pgbl).CopyTo(result, 16);
            BitConverter.GetBytes(pgjg).CopyTo(result, 20);
            BitConverter.GetBytes(pghg).CopyTo(result, 24);

            return result;
        }

        public static ExRight ReadByte(byte[] data)
        {
            if (data.Length != 32)
                return null;

            try
            {
                ExRight result = new ExRight();
                int dateInt = BitConverter.ToInt32(data, 0);

                result.dateTime = new DateTime(dateInt / 10000, dateInt % 10000 / 100, dateInt % 10000 % 100, 0 , 0 , 0);
                result.pxbl = BitConverter.ToSingle(data, 4);
                result.sgbl = BitConverter.ToSingle(data, 8);
                result.cxbl = BitConverter.ToSingle(data, 12);
                result.pgbl = BitConverter.ToSingle(data, 16);
                result.pgjg = BitConverter.ToSingle(data, 20);
                result.pghg = BitConverter.ToSingle(data, 24);

                return result;
            }
            catch
            {
                return null;
            }


        }

        public static ExRight GetLast(string path)
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
            ExRight line = ReadByte(data);
            df.Close();
            return line;
        }
        public static List<ExRight> ReadFile(string path, int offset = 0, int count = 0)
        {
            DataFile df = new DataFile();
            if (!df.Open(path, offset))
                return null;

            List<ExRight> result = new List<ExRight>();
            while (true)
            {
                byte[] data = df.Read(32);
                if (data == null)
                {
                    df.Close();
                    return result;
                }
                ExRight line = ReadByte(data);
                if (line != null)
                    result.Add(line);
                if (count > 0 && result.Count >= count)
                {
                    df.Close();
                    return result;
                }
            }
        }
        public static bool SaveFile(List<ExRight> list, string path, int offset = 0, bool append = false)
        {
            DataFile df = new DataFile();
            if (!df.Open(path, offset, append))
                return false;

            foreach (ExRight item in list)
                df.Write(item.GetByte());

            return df.Cut();
        }


        public static void countExRight(List<KLine> klines, List<ExRight> exRights)
        {
            if (exRights == null) return;
            DateTime lastDate = DateTime.Now.Date; // klines[klines.Count - 1].dateTime.Date;

            foreach (KLine line in klines)
            {
                List<ExRight> rights = new List<ExRight>();
                foreach (ExRight right in exRights)
                    if (right.dateTime.Date <= lastDate && right.dateTime.Date > line.dateTime.Date)
                        rights.Add(right);
                countExRight(line, rights);
            }
        }

        private static void countExRight(KLine line, List<ExRight> exRights)
        {
            exRights.Sort((a, b) => DateTime.Compare(a.dateTime, b.dateTime));

            foreach (ExRight right in exRights)
            {
                line.beginPrice = countExRight(line.beginPrice, right);
                line.endPrice = countExRight(line.endPrice, right);
                line.highPrice = countExRight(line.highPrice, right);
                line.lowPrice = countExRight(line.lowPrice, right);
            }
        }

        private static float countExRight(float value, ExRight exright)
        {
            return (float)Math.Round((value - exright.pxbl + exright.pgbl * exright.pgjg) / (1 + exright.pgbl + exright.sgbl), 2);
        }

    }
}
