using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoZoo.Dog.StockData
{
    public class RealHead
    {
        public string name { set; get; }
        public string updateTime { get; set; }

        // 基本实时信息
        public string code { set; get; }

        public float lastPrice { set; get; }

        public float beginPrice { set; get; }

        public float highPrice { set; get; }

        public float lowPrice { set; get; }

        public float endPrice { set; get; }


        //基础交易数据
        public int allCount { set; get; }//总手

        public int outCount { set; get; }//外盘

        public int inCountA { set; get; }//内盘A 网页

        public int inCountB { set; get; }//内盘B 

        public int allVol { set; get; }//总额

        public int VolumeRatio { set; get; }//量比

        public float Change { set; get; }//换手 





        //股票信息

        public float PE_D { set; get; }//市盈率 动

        public float PE_T { set; get; }//市盈率 TTM 

        public float PE_J { set; get; }//市盈率 静


        public int MarketCountA { set; get; }//总股本

        public int MarketCountB { set; get; }//流通股本 

        public double MarketVolA { set; get; }//总市值

        public double MarketVolB { set; get; }//流通市值



        //资金量
        public int T1In { set; get; }

        public int T1Out { set; get; }

        public int T2In { set; get; }

        public int T2Out { set; get; }

        public int T3In { set; get; }

        public int T3Out { set; get; }

        public int T4In { set; get; }

        public int T4Out { set; get; }



        public double SJL { set; get; }
        public double JLR { set; get; }
        public double FZL { set; get; }
    }
}
