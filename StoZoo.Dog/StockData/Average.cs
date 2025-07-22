using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoZoo.Dog.StockData
{
    public class Average
    {
        public DateTime dateTime { set; get; }

        public float Value { set; get; }

        private float lastValue = 0;

        private int count = 0;

        public static List<Average> Count(List<KLine> kLines, int count)
        {
            kLines.Sort((a, b) => DateTime.Compare(a.dateTime, b.dateTime));

            List<Average> result = new List<Average>();
            if (kLines.Count < count) return result;

            float allValue = kLines.Where(p => p.dateTime < kLines[count - 1].dateTime).Sum(p => p.endPrice);

            for (int i = count - 1, j = 0; i < kLines.Count; i++,j++)
            {
                allValue = allValue + kLines[i].endPrice;
                result.Add(
                    new Average()
                    {
                        dateTime = kLines[i].dateTime,
                        Value =allValue / count,
                        lastValue = allValue - kLines[i].endPrice,
                        count = count
                    });
                allValue = allValue - kLines[j].endPrice;
            }

            return result;
        }

        public static Average Count(Average lastValue, float realPrice)
        {
            return new Average()
            {
                dateTime = lastValue.dateTime,
                Value =(lastValue.lastValue + realPrice)/lastValue.count,
                lastValue = lastValue.lastValue,
                count = lastValue.count
            };
        }

        public static Average CountLast(List<KLine> kLines, int count, int index = 0)
        {
            kLines.Sort((a, b) => DateTime.Compare(a.dateTime.Date, b.dateTime.Date));

            float allValue = 0;
            for (int i = 0; i < count; i++)
                allValue += kLines[kLines.Count - index - 1 - i].endPrice;

            return new Average()
            {
                dateTime = kLines[kLines.Count - index - 1].dateTime,
                Value = allValue / count,
                lastValue = allValue - kLines[kLines.Count - index - 1].endPrice,
                count = count
            };
        }

        public static (float, float) Count(Average lastValue, float min, float max)
        {
            return (min * lastValue.count - lastValue.lastValue, max * lastValue.count - lastValue.lastValue);
        }
    }
}
