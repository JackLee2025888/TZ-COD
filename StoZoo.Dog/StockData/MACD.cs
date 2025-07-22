using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoZoo.Dog.StockData
{
    public class MACD
    {
        public DateTime dateTime { set; get; }

        public float Macd { set; get; }
        public float Diff { set; get; }
        public float Dea { set; get; }

        public float LastEMAShort { set; get; }
        public float LastEMALong { set; get; }
        public float LastDea { set; get; }

        public static MACD Count(MACD lastvalue, float thisPrice, int SHORT, int LONG, int MID)
        {
            float emashort = calcEMA(thisPrice, lastvalue.LastEMAShort, SHORT);
            float emalong = calcEMA(thisPrice, lastvalue.LastEMALong, LONG);
            float dif = emashort - emalong;
            float dea = calcDEA(dif, lastvalue.LastDea, MID);

            return new MACD()
            {
                dateTime = lastvalue.dateTime,
                Diff = dif,
                Dea = dea,
                Macd = (dif - dea) * 2,
                LastEMAShort = lastvalue.LastEMAShort,
                LastEMALong = lastvalue.LastEMALong,
                LastDea = lastvalue.LastDea
            };
        }



        public static List<MACD> Count(List<KLine> kLines, int SHORT, int LONG, int MID)
        {
            List<float> data = kLines.Select(p => p.endPrice).ToList();

            List<MACD> macd = new List<MACD>();

            List<float> emaShort = calcEMA(data, SHORT);
            List<float> emaLong = calcEMA(data, LONG);

            Global.SaveFile(System.IO.Path.Combine("D:\\stockresult", data.Count.ToString() + ".json"), emaShort);



            List<float> dif = calcDIF(data, emaShort, emaLong);
            List<float> dea = calcDEA(dif, MID);
            if (data.Count > 1)
            {
                macd.Add(new MACD()
                {
                    dateTime = kLines[0].dateTime,
                    Diff = dif[0],
                    Dea = dea[0],
                    Macd = (dif[0] - dea[0]) * 2,
                    LastEMAShort = 0,
                    LastEMALong = 0,
                    LastDea = 0
                });
            }
            for (int i = 1; i < data.Count; i++)
                macd.Add(new MACD()
                {
                    dateTime = kLines[i].dateTime,
                    Diff =dif[i],
                    Dea = dea[i],
                    Macd = (dif[i] - dea[i]) * 2,
                    LastEMAShort=emaShort [i-1],
                    LastEMALong = emaLong[i - 1],
                    LastDea = dea[i-1],
                });





            return macd;
        }

        public static List<float> calcDIF(List<float> data, List<float> emaShort, List<float> emaLong)
        {
            List<float> dif = new List<float>();
            //List<float> emaShort = calcEMA(data, SHORT);
            //List<float> emaLong = calcEMA(data, LONG);
            for (int i = 0; i < data.Count; i++)
                dif.Add(emaShort[i] - emaLong[i]);

            return dif;
        }

        public static List<float> calcDEA(List<float> dif, int MID)
        {
            return calcEMA(dif, MID);
        }
        public static float calcDEA(float  dif,float lastDea, int MID)
        {
            return calcEMA(dif,lastDea , MID);
        }


        public static List<float> calcEMA(List<float> data, int n)
        {
            float a = 2.0f / (n + 1);

            List<float> ema = new List<float>();
            ema.Add(data[0]);
            for (int i = 1; i < data.Count; i++)
                ema.Add(a * data[i] + (1 - a) * ema[i - 1]);
            return ema;
        }
        public static float calcEMA(float endprice, float lastEMA, int n)
        {
            float a = 2.0f / (n + 1);
            float ema = a * endprice + (1 - a) * lastEMA;
            return ema;
        }

    }
}
