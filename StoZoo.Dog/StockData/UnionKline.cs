using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoZoo.Dog.StockData
{
 public    class UnionKline
    {
        public static List<KLine> GetLCN(string path,int n=15)
        {
            List<KLine> LC5 = KLine.getLC5(path) ;
            List<KLine> LCN = new List<KLine>();
            //int m = n / 5;
            KLine firstKL=  LC5.FirstOrDefault();
            DateTime thisDate= firstKL.dateTime.Date ;

            TimeSpan thisTime= new TimeSpan(9, 30, 0);
            TimeSpan endtime = thisTime.Add(TimeSpan.FromMinutes(n));
            List<KLine> toBUnion = new List<KLine>();
            for (int i = 0; i < LC5.Count; i++)
            {
            
                if(LC5[i].dateTime.Date >thisDate || LC5[i].dateTime.TimeOfDay> endtime)
                {
                    if (toBUnion.Count > 0)
                    {
                        LCN.Add(unionKlines(toBUnion));
                        thisTime = toBUnion.Last().dateTime.TimeOfDay;
                        endtime = thisTime.Add(TimeSpan.FromMinutes(n)); 
                        toBUnion = new List<KLine>();
                    }
                }
                if (LC5[i].dateTime.Date > thisDate)
                {
                    thisTime = new TimeSpan(9, 30, 0);
                    thisDate = LC5[i].dateTime.Date;
                }
                if (LC5[i].dateTime.TimeOfDay > new TimeSpan(13, 0, 0)&& LC5[i].dateTime.TimeOfDay < new TimeSpan(13, 10, 0))
                   thisTime = new TimeSpan(13, 0, 0);
                endtime = thisTime.Add(TimeSpan.FromMinutes(n));
                toBUnion.Add(LC5[i]);
            }
            return LCN;
        }

        public static  KLine unionKlines(List<KLine> klines)
        {
             KLine kline=klines.FirstOrDefault() ;
            long dealAmount = 0;
            float  dealTurnover = 0;
            int  dealVolume = 0;
            foreach (KLine kl in klines)
            {
                if (kl.dateTime > kline.dateTime)
                    kline.dateTime = kl.dateTime;
                if (kl.highPrice > kline.highPrice)
                    kline.highPrice = kl.highPrice;
                if (kl.lowPrice  < kline.lowPrice)
                    kline.lowPrice = kl.lowPrice;
                dealAmount += kl.dealAmount;
                dealTurnover += kl.dealTurnover;
                dealVolume += kl.dealVolume;
            }
            kline.dealAmount = dealAmount;
            kline.dealTurnover = dealTurnover;
            kline.dealVolume = dealVolume;
            kline.endPrice = klines[klines.Count - 1].endPrice;
            return kline;        
        }

    }
}
