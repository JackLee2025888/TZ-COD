using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoZoo.Dog.StockData
{
    public class RSI
    {
        public DateTime dateTime { set; get; }
        public float Value { set; get; }

        private float lastPrice = 0;
        private float lastUp = 0;
        private float lastDown = 0;
        private int N = 0;
        public static List<RSI> Count(List<KLine> kLines, int N)
        {
            List<RSI> result = new List<RSI>();

            if (kLines.Count <= N)
                return result;


            float allUp = 0;
            float allDown = 0;
            for (int i = 1; i <= N; i++)
            {
                allUp += Math.Max(0, kLines[i].endPrice - kLines[i - 1].endPrice);
                allDown += Math.Abs(Math.Min(0, kLines[i].endPrice - kLines[i - 1].endPrice));
                
            }
            result.Add(new RSI()
            {
                dateTime = kLines[N].dateTime,
                Value = allUp / (allUp + allDown) * 100,

                lastPrice = kLines[N - 1].endPrice,
                lastUp = (allUp - Math.Max(0, kLines[N].endPrice - kLines[N - 1].endPrice)) / (N - 1),
                lastDown = (allDown - Math.Abs(Math.Min(0, kLines[N].endPrice - kLines[N - 1].endPrice))) / (N - 1),
                N = N
            });

            allUp = allUp / N;
            allDown = allDown / N;


            for (int i = N + 1; i < kLines.Count; i++)
            {
                allUp = allUp * (N - 1) + Math.Max(0, kLines[i].endPrice - kLines[i - 1].endPrice);
                allDown = allDown * (N - 1) + Math.Abs(Math.Min(0, kLines[i].endPrice - kLines[i - 1].endPrice));

                result.Add(new RSI()
                {
                    dateTime = kLines[i].dateTime,
                    Value =allUp / (allUp + allDown) * 100,

                    lastPrice = kLines[i - 1].endPrice,
                    lastUp = (allUp - Math.Max(0, kLines[i].endPrice - kLines[i - 1].endPrice)) / (N - 1),
                    lastDown = (allDown - Math.Abs(Math.Min(0, kLines[i].endPrice - kLines[i - 1].endPrice))) / (N - 1),
                    N = N
                });


                allUp = allUp / N;
                allDown = allDown / N;
            }
            
            return result;
        }
        //public static List<RSI> Count(List<KLine> kLines, int N)
        //{
        //    List<RSI> result = new List<RSI>();

        //    if (kLines.Count < N)
        //        return result;


        //    float allUp = 0;
        //    float allDown = 0;
        //    for (int i = 1; i < kLines.Count; i++)
        //    {
        //        allUp += Math.Max(0, kLines[i].endPrice - kLines[i - 1].endPrice);
        //        allDown += Math.Abs(kLines[i].endPrice - kLines[i - 1].endPrice);

        //        if (i >= N - 1)
        //        {
        //            result.Add(new RSI()
        //            {
        //                dateTime = kLines[i].dateTime,
        //                Value = allUp / allDown * 100,

        //                lastPrice = kLines[i - 1].endPrice,
        //                lastDown = allDown - Math.Abs(kLines[i].endPrice - kLines[i - 1].endPrice),
        //                lastUp = allUp - Math.Max(0, kLines[i].endPrice - kLines[i - 1].endPrice),
        //            });

        //            allUp -= Math.Max(0, kLines[i - N + 2].endPrice - kLines[i - N + 1].endPrice);
        //            allDown -= Math.Abs(kLines[i - N + 2].endPrice - kLines[i - N + 1].endPrice);

        //        }
        //    }

        //    return result;
        //}

        public static RSI Count(RSI lastValue, float realPrice)
        {
            float allUp = lastValue.lastUp * (lastValue.N - 1) + Math.Max(0, realPrice - lastValue.lastPrice);
            float allDown = lastValue.lastDown * (lastValue.N - 1) + Math.Abs(Math.Min(0, realPrice - lastValue.lastPrice));


            return new RSI()
            {
                dateTime = lastValue.dateTime,
                Value = allUp / (allUp + allDown) * 100,

                lastPrice = lastValue.lastPrice,
                lastDown = lastValue.lastDown,
                lastUp = lastValue.lastUp,
                N = lastValue.N
            };
        }

    }
}
