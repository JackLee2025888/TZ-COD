using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoZoo.Dog.StockData
{
    public class Counter
    {
        public DateTime dateTime { set; get; }

        public List<CounterItem> Counters { set; get; }
        

        //public static List<Counter> Count1(List<KLine> kLines, Dictionary<int, List<KLine> > LC5s = null)
        //{
        //    List<Counter> rt = new List<Counter>();

        //    List<CounterItem> counters = new List<CounterItem>();

        //    //寻找价格范围
        //    float maxprice = float.MinValue, minprice = float.MaxValue;
        //    foreach (KLine kline in kLines)
        //    {
        //        maxprice = Math.Max(maxprice, kline.highPrice);
        //        minprice = Math.Min(minprice, kline.lowPrice);
        //    }

        //    //进行价格拆分
        //    float step = Math.Min((float)Math.Round(minprice * 0.005, 2), (float)Math.Round((maxprice - minprice) / 150, 2));
        //    if (step < 0.01) step = 0.01f;
        //    for (int i = 0; minprice + step * (i - 1) <= maxprice; i++)
        //        counters.Add(new CounterItem() { price = (float)Math.Round(minprice + step * i, 2), Volume = 0 });
            

        //    //开始计算

        //    foreach(KLine kline in kLines)
        //    {


        //        RemoveCounter(counters, kline);

        //        if (LC5s != null && LC5s.ContainsKey(kline.dateTime.Year) && LC5s[kline.dateTime.Year] != null) //获取5分钟线
        //        {
        //            List<KLine> thisLc5s = LC5s[kline.dateTime.Year].Where(p => p.dateTime.Date == kline.dateTime.Date).ToList();
        //            long allv = thisLc5s.Sum(p => p.dealVolume);
        //            if (thisLc5s.Count > 0)
        //                foreach (KLine lc5 in thisLc5s)
        //                    AppendCount(counters, lc5, true);
        //            else
        //                AppendCount(counters, kline);
        //        }
        //        else
        //            AppendCount(counters, kline);

        //        rt.Add(new Counter()
        //        {
        //            dateTime = kline.dateTime.Date,
        //            Counters = counters.Select(p => new CounterItem() { price = p.price, Volume = p.Volume }).ToList()
        //        }) ;
        //    }


        //    return rt;
        //}

        public static List<Counter> Count(List<KLine> kLines, Dictionary<int, List<KLine>> LC5s = null)
        {
            List<Counter> rt = new List<Counter>();

            Dictionary<float, CounterItem> counters = new Dictionary<float, CounterItem>();

            //寻找价格范围
            float maxprice = float.MinValue, minprice = float.MaxValue;
            foreach (KLine kline in kLines)
            {
                maxprice = Math.Max(maxprice, kline.highPrice);
                minprice = Math.Min(minprice, kline.lowPrice);
            }

            //进行价格拆分
            for (float p = minprice; p <= maxprice; p = (int)Math.Round(((p + 0.01) * 100), 0) / 100f)
                counters.Add(p, new CounterItem() { price = (float) p, Volume = 0 });


            //开始计算

            foreach (KLine kline in kLines)
            {
                //按比例减少其他价格量
                RemoveCounter(counters, kline);

                //按三角形增加当前价格量
                if (LC5s != null && LC5s.ContainsKey(kline.dateTime.Year) && LC5s[kline.dateTime.Year] != null) //获取5分钟线
                {
                    List<KLine> thisLc5s = LC5s[kline.dateTime.Year].Where(p => p.dateTime.Date == kline.dateTime.Date).ToList();
                    long allv = thisLc5s.Sum(p => p.dealVolume);
                    if (thisLc5s.Count > 0)
                        foreach (KLine lc5 in thisLc5s)
                            AppendCount(counters, lc5);
                    else
                        AppendCount(counters, kline);
                }
                else
                    AppendCount(counters, kline);

                //计算当前筹码分布 以收盘价为基准，每1%幅度增减
                float step =(float) Math.Round(kline.endPrice * 0.01f, 2);
                if (step < 0.01f)
                    step = 0.01f;

                List<CounterItem> thisCounter = new List<CounterItem>();
                for(float p = kline.endPrice; p <= maxprice; p = (int)Math.Round(((p + step) * 100), 0) / 100f)
                    thisCounter.Add(new CounterItem()
                    {
                        price = p,
                        Volume = GetVol(counters, p, p + step)
                    });
                for (float p = kline.endPrice - step; p >= minprice; p = (int)Math.Round(((p - step) * 100), 0) / 100f)
                    thisCounter.Add(new CounterItem()
                    {
                        price = p,
                        Volume = GetVol(counters, p, p + step)
                    });
                thisCounter.Sort((a, b) => a.price > b.price ? 1 : -1);


                rt.Add(new Counter()
                {
                    dateTime = kline.dateTime.Date,
                    Counters = thisCounter
                });
            }


            return rt;
        }
        private static void RemoveCounter(Dictionary<float, CounterItem> counter, KLine kline)
        {

            foreach (CounterItem item in counter.Values)
                item.Volume = (int)(item.Volume * (1 - kline.dealTurnover / 100));
        }

        private static void AppendCount(Dictionary<float, CounterItem> counter, KLine kline)
        {
            int count = (int)((kline.highPrice - kline.lowPrice) * 100) + 1; //总价格数

            int stepCount = 0; //量拆分数
            {
                for (int i = 1; i <= Math.Floor(count / 2.0); i++)
                    stepCount += i;
                if (count % 2 == 0)
                    stepCount = stepCount * 2;
                else
                    stepCount = stepCount * 2 + (int)Math.Ceiling(count / 2.0);
            }

            int stepVol = kline.dealVolume / stepCount; //单份量


            int allVol = 0;
            for(int i= 1; i < Math.Ceiling(count / 2.0); i++)
            {
                float p1 = kline.lowPrice + 0.01f * (i - 1);
                float p2 = kline.highPrice - 0.01f * (i - 1);

                if (counter.ContainsKey(p1))
                    counter[p1].Volume += stepVol * i;
                if (counter.ContainsKey(p2))
                    counter[p2].Volume += stepVol * i;

                allVol += stepVol * i * 2;
            }

            if (count % 2 == 0)
            {
                float p1 = kline.lowPrice + 0.01f * ((int)Math.Ceiling(count / 2.0) - 1);
                float p2 = kline.highPrice - 0.01f * ((int)Math.Ceiling(count / 2.0) - 1);
                if (counter.ContainsKey(p1))
                    counter[p1].Volume += (kline.dealVolume - allVol) / 2;
                if (counter.ContainsKey(p2))
                    counter[p2].Volume += (kline.dealVolume - allVol) / 2;
            }
            else
            {
                float p1 = kline.highPrice - 0.01f * ((int)Math.Ceiling(count / 2.0) - 1);
                if (counter.ContainsKey(p1))
                    counter[p1].Volume += kline.dealVolume - allVol;

            }

        }

        private static int GetVol(Dictionary<float, CounterItem> counter, float p1, float p2)
        {
            int vol = 0;
            for (float p = p1; p < p2; p = (int)Math.Round(((p + 0.01) * 100), 0) / 100f)
                if (counter.ContainsKey(p))
                    vol += counter[p].Volume;
            return vol;
        }
        public class CounterItem
        {
            public float price { set; get; }
            public int Volume { set; get; }
        }
    }
}
