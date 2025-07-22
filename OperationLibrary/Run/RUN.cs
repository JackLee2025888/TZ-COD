using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TZ.OperationLibrary.Run
{
    public class RUN
    {
        DataLibrary.RunSetting setting = null;
        DataLibrary.Policy policy = null;

        List<EnvironmentClass> Environments = null;

        public RUN(DataLibrary.RunSetting setting, DataLibrary.Policy policy)
        {
            this.setting = setting;
            this.policy = policy;

            int n = setting.Process;
            if (!setting.isProcess)
                n = 1;

            Environments = new List<EnvironmentClass>();
            for(int i = 0;i < n;i++)
            {
                RunEnvironment e = new RunEnvironment();
                if (!e.LoadBaseInfo(setting, policy))
                {
                    Environments = null;
                    return;
                }
                Environments.Add(new EnvironmentClass()
                {
                    running = false,
                    environment = e
                }) ;
            }

        }

        public void DO(CallbackDelegate callback)
        {
            this.callback = callback;

            new Thread(new ThreadStart(Start)).Start();
        }

        public delegate void CallbackDelegate(int value, string err);

        CallbackDelegate callback = null;


        List<string> rts = new List<string>();
        private void Start()
        {

            callback?.Invoke(0, "正在获取基础数据...");

            StoZoo.Dog.StockData.StockData datas = Globe.LoadFile<StoZoo.Dog.StockData.StockData>(Path.Combine(Globe.SystemSettings.DataPath, "stock.dat"));
            if (datas == null)
            {
                callback?.Invoke(-1, "无法读取基础数据文件");
                return;
            }
            //获取所有股票信息
            List<StoZoo.Dog.StockData.Stock> stocks = new List<StoZoo.Dog.StockData.Stock>();

            foreach(StoZoo.Dog.StockData.Market m in datas.Markets)
            {
                if (setting.isCST)
                    stocks.AddRange(m.Stocks.Where(p => setting.CST.Contains(p.Code)));
                else
                {
                    //foreach (StoZoo.Dog.StockData.Stock stock in m.Stocks)
                    //{
                    //    if (!setting.is00) if (stock.Code.StartsWith("00")) continue;
                    //    if (!setting.is60) if (stock.Code.StartsWith("60")) continue;
                    //    if (!setting.is30) if (stock.Code.StartsWith("30")) continue;
                    //    if (!setting.is68) if (stock.Code.StartsWith("68")) continue;
                    //    if (!setting.isST) if (stock.Name .Contains ("ST")) continue;
                    //    stocks.Add(stock);
                    //}
                    if (setting.is00) stocks.AddRange(m.Stocks.Where(p => p.Code.StartsWith("00")));
                    if (setting.is60) stocks.AddRange(m.Stocks.Where(p => p.Code.StartsWith("60")));
                    if (setting.is30) stocks.AddRange(m.Stocks.Where(p => p.Code.StartsWith("30")));
                    if (setting.is68) stocks.AddRange(m.Stocks.Where(p => p.Code.StartsWith("68")));
                }
            }

            if (!setting.isCST && !setting.isST)
                stocks = stocks.Where(p => !p.Name.Contains("ST")).ToList();

            //循环模拟
            bool isdone = false;
            for (int i = 0;i <stocks.Count;i++)
            {
                isdone = false;
                while (!isdone)
                {
                    foreach (EnvironmentClass e in Environments)
                    {
                        if (!e.running)
                        {
                            isdone = true;
                            callback?.Invoke((i + 1) * 100 / stocks.Count, "正在模拟 " + stocks[i].Name + "[" + stocks[i].Code + "]...");

                            e.running = true;
                            List<string> result = new List<string>();
                            new Thread(new ParameterizedThreadStart(Doing)).Start((stocks[i], e));

                            break;
                        }
                    }
                    if (!isdone)
                        Thread.Sleep(50);
                }
            }


            //等待模拟完成
            isdone = false;
            while (!isdone)
            {
                isdone = true;
                foreach (EnvironmentClass e in Environments)
                    if (e.running)
                    {
                        isdone = false;
                        break;
                    }
                if (!isdone)
                    Thread.Sleep(50);
            }



            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(Path.Combine ( setting.outPath,DateTime.Now.ToString ("OUTMMddHHmmss")+".csv"), true, Encoding.GetEncoding(0));
                foreach (string line in rts)
                    sw.WriteLine(line);
            }
            catch
            {

            }
            finally
            {
                if (sw != null)
                    try
                    {
                        sw.Close();
                        sw.Dispose();
                    }
                    catch { }
            }


            callback?.Invoke(-1, "模拟完成");
        }

        private void Doing(object obj)
        {
            (StoZoo.Dog.StockData.Stock stock, EnvironmentClass e) = ((StoZoo.Dog.StockData.Stock, EnvironmentClass))obj;

            if (!e.environment.LoadStock(stock))
            { 
                callback?.Invoke(-2, "无法读取 " + stock.Name + "[" + stock.Code + "] 数据...");
            }
            else
            {
                List<string> result = e.environment.RUN(stock);
                if (result != null)
                    rts.AddRange(result);
            }

            e.running = false;
        }


        class EnvironmentClass
        {
            public bool running = false;
            public RunEnvironment environment = null;
        }
    }
}
