using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoZoo.Dog.StockData
{
    public class KLine
    {
        public DateTime dateTime { set; get; }

        public float beginPrice { set; get; }
        public float endPrice { set; get; }
        public float highPrice { set; get; }
        public float lowPrice { set; get; }

        public int dealVolume { set; get; }
        public long dealAmount { set; get; }
        public float dealTurnover { set; get; }


        public byte[] GetByte()
        {
            byte[] result = new byte[48];
            int dateInt = dateTime.Year * 10000 + dateTime.Month * 100 + dateTime.Day;
            int timeInt = dateTime.Hour * 10000 + dateTime.Minute * 100 + dateTime.Second;

            BitConverter.GetBytes(dateInt).CopyTo(result, 0);
            BitConverter.GetBytes(timeInt).CopyTo(result, 4);
            BitConverter.GetBytes(beginPrice).CopyTo(result, 8);
            BitConverter.GetBytes(endPrice).CopyTo(result, 12);
            BitConverter.GetBytes(highPrice).CopyTo(result, 16);
            BitConverter.GetBytes(lowPrice).CopyTo(result, 20);
            BitConverter.GetBytes(dealTurnover).CopyTo(result, 24);
            BitConverter.GetBytes(dealVolume).CopyTo(result, 28);
            BitConverter.GetBytes(dealAmount).CopyTo(result, 32);

            return result;
        }

        public static KLine ReadByte(byte[] data)
        {
            if (data.Length != 48)
                return null;

            try
            {
                KLine result = new KLine();
                int dateInt = BitConverter.ToInt32(data, 0);
                int timeInt = BitConverter.ToInt32(data, 4);

                result.dateTime = new DateTime(dateInt / 10000, dateInt % 10000 / 100, dateInt % 10000 % 100,
                    timeInt / 10000, timeInt % 10000 / 100, timeInt % 10000 % 100);
                result.beginPrice = BitConverter.ToSingle(data, 8);
                result.endPrice = BitConverter.ToSingle(data, 12);
                result.highPrice = BitConverter.ToSingle(data, 16);
                result.lowPrice = BitConverter.ToSingle(data, 20);
                result.dealTurnover = BitConverter.ToSingle(data, 24);
                result.dealVolume = BitConverter.ToInt32(data, 28);
                result.dealAmount = BitConverter.ToInt64(data, 32);

                return result;
            }
            catch
            {
                return null;
            }


        }

        public static KLine GetLast(string path)
        {
            DataFile df = new DataFile();
            if (!df.Open(path, -48))
                return null;

            byte[] data = df.Read(48);
            if (data == null)
            {
                df.Close();
                return null;
            }
            KLine line = ReadByte(data);
            df.Close();
            return line;
        }

        public static List<KLine> ReadFile(string path, int offset = 0, int count = 0)
        {
            DataFile df = new DataFile();
            if (!df.Open(path, offset))
                return null;

            List<KLine> result = new List<KLine>();
            while(true)
            {
                byte[] data = df.Read(48);
                if (data == null)
                {
                    df.Close();
                    return result;
                }
                KLine line = ReadByte(data);
                if (line != null)
                    result.Add(line);
                if (count > 0 && result.Count >= count)
                {
                    df.Close();
                    return result;
                }
            }
        }
        public static bool SaveFile(List<KLine> list, string path, int offset = 0, bool append = false)
        {
            DataFile df = new DataFile();
            if (!df.Open(path, offset, append))
                return false;

            foreach (KLine item in list)
                df.Write(item.GetByte());

            return df.Cut();
        }


        public static List<KLine> getLC5(string path)
        {
            if (!File.Exists(path))
                return null;

            FileStream fs = null;
            List<KLine> klines = new List<KLine>();
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                fs.Seek(0, SeekOrigin.Begin);

                for (int offset = 0; offset < fs.Length; offset += 32)
                {
                    byte[] bytes = new byte[32];
                    if (fs.Read(bytes, 0, 32) < 32)
                        break;

                    try
                    {
                        int date = BitConverter.ToUInt16(bytes, 0);
                        int time = BitConverter.ToInt16(bytes, 2);
                        float begin = BitConverter.ToSingle(bytes, 4);
                        float high = BitConverter.ToSingle(bytes, 8);
                        float low = BitConverter.ToSingle(bytes, 12);
                        float end = BitConverter.ToSingle(bytes, 16);

                        float amount = BitConverter.ToSingle(bytes, 20);
                        int volume = BitConverter.ToInt32(bytes, 24);

                        int year = (int)Math.Floor(date / 2048.0) + 2004;
                        int month = (int)Math.Floor(date % 2048 / 100.0);
                        int day = date % 2048 % 100;
                        int hour = (int)Math.Floor(time / 60.0);
                        int minute = time % 60;

                        klines.Add(new KLine()
                        {
                            dateTime = new DateTime(year, month, day, hour, minute, 0),
                            beginPrice = begin,
                            endPrice = end,
                            highPrice = high,
                            lowPrice = low,
                            dealAmount = (long)amount,
                            dealVolume = volume / 100
                        });


                    }
                    catch { continue; }
                }


                fs.Close();
            }
            catch
            {
                return null;
            }
            finally
            {
                if (fs != null)
                    try
                    {
                        fs.Close();
                        fs.Dispose();
                    }
                    catch { }
            }

            return klines;
        }

        public static List<KLine> CountMinutes(List<KLine> lc5KLines,int interval = 15)
        {
            List<KLine> res = new List<KLine>();
            Dictionary<string, List<KLine>> MinutesKline = new Dictionary<string, List<KLine>>();
            foreach (KLine lc5line in lc5KLines)
            {
                var dt = lc5line.dateTime;
                DateTime newdt;
                int intkey = (int)Math.Ceiling(((double)dt.Minute) / ((double)interval));
               
                if (interval * intkey >= 60)
                {
                    newdt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour + (int)Math.Floor((double)interval * intkey) / 60, (interval * intkey) % 60, 0);
                }
                else
                    newdt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, interval * intkey, 0);

                string key =newdt.ToString ();
                if (MinutesKline.ContainsKey(key))
                {
                    MinutesKline[key].Add(lc5line);
                }
                else
                {
                    MinutesKline.Add(key, new List<KLine>() { lc5line });
                }
            }
            foreach (string key in MinutesKline.Keys)
            {
                if (key == "2022/10/21 15:00:00")
                {

                    string tt = "test";
                }
                List<KLine> lines = MinutesKline[key];
                if (lines.Count < 1) continue;
                KLine result = new KLine();
                result.dateTime = lines[lines.Count - 1].dateTime;
                result.beginPrice = lines[0].beginPrice;
                result.endPrice = lines[lines.Count - 1].endPrice;
                result.highPrice = lines.Max(p => p.highPrice);
                result.lowPrice = lines.Min(p => p.lowPrice);
                result.dealTurnover = lines.Sum(p => p.dealTurnover);
                result.dealVolume = lines.Sum(p => p.dealVolume);
                result.dealAmount = lines.Sum(p => p.dealAmount);
                res.Add(result);
            }
            return res;
        }
        public static KLine CountMinutes(KLine thiskline,Dictionary <int, List<KLine>> lc5KLinesDic,int time=15)
        {
            DateTime thisdate = thiskline.dateTime;
            int year = thisdate.Year;
            if (!lc5KLinesDic.ContainsKey(year))
                return thiskline;
            List<KLine> lc5KLines = lc5KLinesDic[year];

            List<KLine> thisdaylines = lc5KLines.Where(p => p.dateTime.Month == thisdate.Month && p.dateTime.Date == thiskline.dateTime.Date).ToList();
            int intkey = (int)Math.Floor(((double)thiskline.dateTime.Minute) / ((double)time));
            List<KLine> lines = thisdaylines.Where(p => p.dateTime.Hour == thiskline.dateTime.Hour && p.dateTime.Minute > intkey * time && p.dateTime.Minute < thiskline.dateTime.Minute).ToList ();
     
            lines.Add(thiskline);
            if (lines.Count < 1) return null;
            KLine result = new KLine();
            result.dateTime = lines[lines.Count - 1].dateTime;
            result.beginPrice = lines[0].beginPrice;
            result.endPrice = lines[lines.Count - 1].endPrice;
            result.highPrice = lines.Max(p => p.highPrice);
            result.lowPrice = lines.Min(p => p.lowPrice);
            result.dealTurnover = lines.Sum(p => p.dealTurnover);
            result.dealVolume = lines.Sum(p => p.dealVolume);
            result.dealAmount = lines.Sum(p => p.dealAmount);
            return result;
        }


        public static List<KLine> CountWeek(List<KLine> dayKLines)
        {
            List<KLine> res = new List<KLine>();
            Dictionary<string, List<KLine>> WeekKline = new Dictionary<string, List<KLine>>();
            foreach (KLine dayline in dayKLines)
            {
                int offset = 1 -(int)( dayline.dateTime .DayOfWeek);
                string key = dayline.dateTime.Date.AddDays(offset).ToString("yyyyMMdd");
                if (WeekKline.ContainsKey(key))
                {
                    WeekKline[key].Add(dayline);
                }
                else
                {
                    WeekKline.Add(key, new List<KLine>() { dayline });
                }
            }
            foreach (string key in WeekKline.Keys)
            {
                List<KLine> lines = WeekKline[key];
                if (lines.Count < 1) continue;
                KLine result = new KLine();
                result.dateTime = lines[lines.Count - 1].dateTime;
                result.beginPrice = lines[0].beginPrice;
                result.endPrice = lines[lines.Count - 1].endPrice;
                result.highPrice = lines.Max(p => p.highPrice);
                result.lowPrice = lines.Min(p => p.lowPrice);
                result.dealTurnover = lines.Sum(p => p.dealTurnover);
                result.dealVolume = lines.Sum(p => p.dealVolume);
                result.dealAmount = lines.Sum(p => p.dealAmount);
                res.Add(result);
            }
            return res;
        }

        public static KLine CountWeek(KLine thiskline, List<KLine> dayKLines)
        {
            int offset = 1 - (int)(thiskline.dateTime.DayOfWeek);
            DateTime monday = thiskline.dateTime.Date.AddDays(offset);
            List<KLine> lines = dayKLines.Where(p => p.dateTime.Date >= monday.Date && p.dateTime.Date < thiskline.dateTime.Date).ToList();
            lines.Add(thiskline);
            if (lines.Count < 1) return null ;
            KLine result = new KLine();
            result.dateTime = lines[lines.Count - 1].dateTime;
            result.beginPrice = lines[0].beginPrice;
            result.endPrice = lines[lines.Count - 1].endPrice;
            result.highPrice = lines.Max(p => p.highPrice);
            result.lowPrice = lines.Min(p => p.lowPrice);
            result.dealTurnover = lines.Sum(p => p.dealTurnover);
            result.dealVolume = lines.Sum(p => p.dealVolume);
            result.dealAmount = lines.Sum(p => p.dealAmount);
            return result;
        }
        public static KLine CountMonth(KLine thiskline, List<KLine> dayKLines)
        {
            DateTime thisdate = thiskline.dateTime;
            List<KLine> lines = dayKLines.Where(p => p.dateTime.Year == thisdate.Year && p.dateTime.Month == thisdate.Month && p.dateTime.Date < thiskline.dateTime.Date).ToList();
            lines.Add(thiskline);
            if (lines.Count < 1) return null;
            KLine result = new KLine();
            result.dateTime = lines[lines.Count - 1].dateTime;
            result.beginPrice = lines[0].beginPrice;
            result.endPrice = lines[lines.Count - 1].endPrice;
            result.highPrice = lines.Max(p => p.highPrice);
            result.lowPrice = lines.Min(p => p.lowPrice);
            result.dealTurnover = lines.Sum(p => p.dealTurnover);
            result.dealVolume = lines.Sum(p => p.dealVolume);
            result.dealAmount = lines.Sum(p => p.dealAmount);
            return result;
        }

        public static List<KLine> CountMonth(List<KLine> dayKLines)
        {
            List<KLine> res = new List<KLine>();
            Dictionary<string, List<KLine>> MonthKline = new Dictionary<string, List<KLine>>();
            foreach (KLine dayline in dayKLines)
            {                
                string key = dayline.dateTime.Year.ToString() + dayline.dateTime.Month.ToString();
                if (MonthKline.ContainsKey(key))
                {
                    MonthKline[key].Add(dayline);
                }
                else {
                    MonthKline.Add(key, new List<KLine>() { dayline });                
                }
            }
            foreach (string key in MonthKline.Keys)
            {
                List<KLine> lines = MonthKline[key];
                if (lines.Count < 1) continue;
                KLine result = new KLine();
                result.dateTime = lines[lines.Count - 1].dateTime; 
                result.beginPrice = lines[0].beginPrice;
                result.endPrice = lines[lines.Count - 1].endPrice;
                result.highPrice =lines.Max (p=>p.highPrice);
                result.lowPrice = lines.Min(p => p.lowPrice );
                result.dealTurnover = lines.Sum (p=>p.dealTurnover);
                result.dealVolume = lines.Sum(p => p.dealVolume);
                result.dealAmount = lines.Sum(p => p.dealAmount);
                res.Add(result);
            }
            return res;
        }




    }
}
