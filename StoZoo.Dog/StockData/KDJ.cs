using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoZoo.Dog.StockData
{
    public class KDJ
    {
        public DateTime dateTime { set; get; }

        public float K { set; get; }
        public float D { set; get; }
        public float J { set; get; }

        private float lastMin = 0;
        private float lastMax = 0;
        private int M1, M2;
        private float lastK, lastD;
        

        public static List<KDJ> Count(List<KLine> kLines, int LONG, int M1, int M2)
        {
            List<KDJ> rt = new List<KDJ>();

            float Kt = 50;
            float Dt = 50;
            for (int i = 0; i < kLines.Count; i++)
            {
                float lastMax = GetMaxPrice(kLines, i - 1, LONG - 1);
                float lastMin = GetMinPrice(kLines, i - 1, LONG - 1);
                float maxPrice = Math.Max(lastMax, kLines[i].highPrice);
                float minPrice = Math.Min(lastMin, kLines[i].lowPrice);
                float RSVt = (kLines[i].endPrice - minPrice) / (maxPrice - minPrice) * 100;
                if (float.IsNaN(RSVt)) RSVt = 0;

                float lastK = Kt;
                float lastD = Dt;

                Kt = RSVt / M1 + (M1 - 1) * Kt / M1;
                Dt = Kt / M2 + (M2 - 1) * Dt / M2; 
                float Jt = 3 * Kt - 2 * Dt;

                rt.Add(new KDJ
                {
                    dateTime = kLines[i].dateTime,
                    K = Kt,
                    D = Dt,
                    J = Jt,
                    lastMax = lastMax,
                    lastMin = lastMin,
                    M1 = M1,
                    M2 = M2,
                    lastK = lastK,
                    lastD = lastD
                });
            }


            return rt;
        }

        private static float GetMaxPrice(List<KLine> klines, int index, int LONG)
        {
            float max = float.MinValue;
            for (int i = index; i >= 0 && i > index - LONG; i--)
                if (klines[i].highPrice > max)
                    max = klines[i].highPrice;

            return max;
        }

        private static float GetMinPrice(List<KLine> klines, int index, int LONG)
        {
            float min = float.MaxValue;
            for (int i = index; i >= 0 && i > index - LONG; i--)
                if (klines[i].lowPrice < min)
                    min = klines[i].lowPrice;

            return min;
        }


        public static KDJ Count(KDJ lastValue, float realPrice, float realMin, float realMax)
        {
            float maxPrice = Math.Max(lastValue.lastMax, realMax);
            float minPrice = Math.Min(lastValue.lastMin, realMin);

            float RSVt = (realPrice - minPrice) / (maxPrice - minPrice) * 100;
            float Kt = RSVt / lastValue.M1 + (lastValue.M1 - 1) * lastValue.lastK / lastValue.M1;
            float Dt = Kt / lastValue.M2 + (lastValue.M2 - 1) * lastValue.lastD / lastValue.M2;
            float Jt = 3 * Kt - 2 * Dt;
            return new KDJ()
            {
                dateTime = lastValue.dateTime,
                K = Kt,
                D = Dt,
                J = Jt,
                lastMax = lastValue.lastMax,
                lastMin = lastValue.lastMin,
                M1 = lastValue.M1,
                M2 = lastValue.M2,
                lastK = lastValue.lastK,
                lastD = lastValue.lastD
            };
        }
    }
}
