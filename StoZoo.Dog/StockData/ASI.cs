using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoZoo.Dog.StockData
{
    public class ASI
    {
        public DateTime dateTime { set; get; }
        public float Value { set; get; }
 
        public float ASIT { set; get; }
        public float LastASIT { get; set; }

        public float LastSI { get; set; }
        public KLine LastKline { get; set; }
        private int M, N;


        public static List<ASI> Count(List<KLine> kLines, int M, int N)
        {
            List<ASI> rt = new List<ASI>();
            if (kLines.Count < 1) return rt;      
            List<float> SIs = new List<float>();
            float asit = float.NaN ; 
            for (int i = 1; i < kLines.Count; i++)
            {
                float A = Math.Abs(kLines[i].highPrice - kLines[i - 1].endPrice);
                float B = Math.Abs(kLines[i].lowPrice - kLines[i - 1].endPrice);
                float C = Math.Abs(kLines[i].highPrice - kLines[i - 1].lowPrice);
                float D = Math.Abs(kLines[i - 1].endPrice - kLines[i - 1].beginPrice);
                float E = kLines[i].endPrice - kLines[i - 1].endPrice;
                float F = kLines[i].endPrice - kLines[i].beginPrice;
                float G = kLines[i - 1].endPrice - kLines[i - 1].beginPrice;
                float X = E + (float)0.5 * F + G;
                float K, R;
                if (A > B)
                {
                    K = A;
                    R = (float)(A + 0.5 * B + 0.25 * D);
                    if (C >= A)
                    {
                        R = (float)(C + 0.25 * D);
                    }
                }
                else
                {
                    K = B;
                    R = (float)(B + 0.5 * A + 0.25 * D);
                    if (C >= B)
                    {
                        R = (float)(C + 0.25 * D);
                    }
                }
                float L = 3;
                float SI = 48* X / R * K / L;

                SIs.Add(SI);
                float ASI = 0;
                for (int m = 0; m < M; m++)
                {
                    if (SIs.Count - 1 >= m)
                        ASI += SIs[SIs.Count - m - 1];
                    else break;
                }
               

                float lastasit = asit;
                asit = 0;
                if (rt.Count<N)
                {
                    asit = 0;                   
                }
                else 
                {
                    for (int j = 0; j < N-1; j++)
                        asit += rt[rt.Count - 1 - j].Value;
                    asit += ASI;
                    asit = asit / N;                
                }

                rt.Add(new Dog.StockData.ASI()
                {
                    dateTime = kLines[i].dateTime,
                    Value  =ASI,
                    ASIT  = asit,
                    LastASIT  = lastasit ,
                    LastSI =SI,
                    LastKline = kLines[i - 1],
                    M =M,
                    N=N
                });;
            }

            return rt;
        }

        public static ASI Count(ASI lastValue,KLine realkline)
        {
            List<ASI> rt = new List<ASI>();

            float A = Math.Abs(realkline.highPrice - lastValue.LastKline.endPrice);
            float B = Math.Abs(realkline.lowPrice - lastValue.LastKline.endPrice);
            float C = Math.Abs(realkline.highPrice - lastValue.LastKline.lowPrice);
            float D = Math.Abs(lastValue.LastKline.endPrice - lastValue.LastKline.beginPrice);
            float E = realkline.endPrice - lastValue.LastKline.endPrice;
            float F = realkline.endPrice - realkline.beginPrice;
            float G = lastValue.LastKline.endPrice - lastValue.LastKline.beginPrice;
            float X = E + (float)0.5 * F + G;
            float K, R;
            if (A > B)
            {
                K = A;
                R = (float)(A + 0.5 * B + 0.25 * D);
                if (C > A)
                {
                    R = (float)(C + 0.25 * D);
                }
            }
            else
            {
                K = B;
                R = (float)(B + 0.5 * A + 0.25 * D);
                if (C > B)
                {
                    R = (float)(C + 0.25 * D);
                }
            }
            float L = 3;
            float SI = 48 * X / R * K / L;
            float ASI = lastValue.Value - lastValue.LastSI + SI;

            float asit = (lastValue.ASIT * lastValue.N -lastValue .Value  + ASI) / lastValue.N;
            return new Dog.StockData.ASI()
            {
                dateTime = lastValue.dateTime,
                Value = ASI,
                ASIT = asit,
                LastASIT = lastValue .LastASIT ,
                LastSI = SI,
                LastKline = lastValue.LastKline,
                M = lastValue.M,
                N = lastValue.N
            };
        }


    }
}
