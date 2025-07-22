using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoZoo.Dog.StockData
{
    public class Stock
    {
        public string Code { set; get; }
        public string Name { set; get; }
        public string MarketCode { set; get; }
        public string MarketMember { set; get; }

        // 0 = 股票 1 = 板块 2 = 指数
        public int Type { set; get; } = 0;

        public List<string> SubItems { set; get; } = new List<string>();
    }

    public class Market
    {
        public string Name { set; get; }
        public string Code { set; get; }
        public string Member { set; get; }
        public List<Stock> Stocks { set; get; } = new List<Stock>();
        public List<Stock> ShareIndexs { set; get; } = new List<Stock>();
    }

    public class StockData
    {
        public string upDateTime { set; get; } = "1990-01-01";
        public List<Market> Markets { set; get; } = new List<Market>();

        public List<Stock> Blocks { set; get; } = new List<Stock>();
    }
}
