using StoZoo.Dog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TZ.OperationLibrary.Run
{
    public class RUNDevire
    {
        DataLibrary.RunSetting setting = null;
        DataLibrary.Policy policy = null;

        Dictionary<string, RunEnvironment> Environments = null;

        List<EnvironmentClass> runnings = null;

        public RUNDevire(DataLibrary.RunSetting setting, DataLibrary.Policy policy)
        {
            this.setting = setting;
            this.policy = policy;

            int n = setting.Process;
            if (!setting.isProcess)
                n = 1;

            Environments = new Dictionary<string, RunEnvironment>();

            runnings = new List<EnvironmentClass>();
            for (int i = 0; i < n; i++)
                runnings.Add(new EnvironmentClass()
                {
                    running = false,
                    environment = null
                });
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
            callback?.Invoke(0, "正在获取首次计算数据...");
            callback?.Invoke(-2, "获取首次计算数据");

            OperationExchange oe = getOperation();
            if (oe == null)
                return;

            Globe.DeriveValues.Clear();
            foreach (var v in oe.Parameters)
            {
                Globe.DeriveValues.Add(v.key, StoZoo.Dog.Global.ReDeserialize<double>(v.value));
            }

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
                    if (setting.is00) stocks.AddRange(m.Stocks.Where(p => p.Code.StartsWith("00")));
                    if (setting.is60) stocks.AddRange(m.Stocks.Where(p => p.Code.StartsWith("60")));
                    if (setting.is30) stocks.AddRange(m.Stocks.Where(p => p.Code.StartsWith("30")));
                    if (setting.is68) stocks.AddRange(m.Stocks.Where(p => p.Code.StartsWith("68")));
                }
            }

            if (!setting.isCST && !setting.isST)
                stocks = stocks.Where(p => !p.Name.Contains("ST")).ToList();

            //加载股票数据
            
            callback?.Invoke(-2, "开始加载股票数据");
            for (int i = 0; i < stocks.Count; i++)
            {
                if (Environments.ContainsKey(stocks[i].Code)) continue;
                RunEnvironment re = new RunEnvironment();
                if (!re.LoadBaseInfo(setting, policy))
                {
                    Environments = null;
                    callback?.Invoke(-1, "无法创建运行环境");
                    return;
                }

                callback?.Invoke((i + 1) * 100 / stocks.Count, "正在加载数据 " + stocks[i].Name + "[" + stocks[i].Code + "]...");
                if (!re.LoadStock(stocks[i]))
                    callback?.Invoke(-2, "无法读取 " + stocks[i].Name + "[" + stocks[i].Code + "] 数据...");
                else
                    Environments.Add(stocks[i].Code, re);
            }

          
            //开始模拟
            int doCount = 0;
         
            while (true)
            {
                rts.Clear();
               
                callback?.Invoke(-2, "开始第 " + (++doCount).ToString() + " 次模拟");
                callback?.Invoke(-2, "参数: " + string.Join(", ", oe.Parameters.Select(p=> p.key + " = " + p.value.ToString())));

                bool isdone = false;
                for (int i = 0; i < stocks.Count; i++)
                {
                    if (!Environments.ContainsKey(stocks[i].Code)) continue;
                    Environments[stocks[i].Code].LoadIndicator(stocks[i], false);

                    isdone = false;
                    while (!isdone)
                    {
                        foreach (EnvironmentClass e in runnings)
                        {
                            if (!e.running)
                            {
                                isdone = true;
                                callback?.Invoke((i + 1) * 100 / stocks.Count, "正在模拟 " + stocks[i].Name + "[" + stocks[i].Code + "]...");

                                e.running = true;
                                e.environment = Environments[stocks[i].Code];
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
                    foreach (EnvironmentClass e in runnings)
                        if (e.running)
                        {
                            isdone = false;
                            break;
                        }
                    if (!isdone)
                        Thread.Sleep(50);
                }

                rts = rts.Where(p => !string.IsNullOrEmpty(p)).ToList(); 
                //写入模拟结果
                string swName = DateTime.Now.ToString("OUTMMddHHmmss");
                StreamWriter sw = null;

                try
                {
                    sw = new StreamWriter(Path.Combine(setting.outPath, swName + ".csv"), true, Encoding.GetEncoding(0));
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

                callback?.Invoke(-2, "第 " + doCount.ToString() + " 次模拟完成");

                //计算结果数据

                (double P, double V, double T) = GetResult(rts);

                oe.Results = new List<KeyValue>()
                { 
                    new KeyValue("Key", swName),
                    new KeyValue("P", P),
                    new KeyValue("V", V),
                    new KeyValue("T", T)
                };

                double M = double.NaN;
                if (setting.DeriveYear)
                {
                    if (rts.Count > 0)
                        M = MoNi(rts);
                    oe.Results.Add(new KeyValue("M", M));
                }

                callback?.Invoke(-2, "提交第 " + (doCount).ToString() + " 次计算结果");
                callback?.Invoke(-2, "P: "+ P.ToString ()+ ", V: " + V.ToString()+ ", T: " + T.ToString() + ", M: " + M.ToString());
                string rr = PostOperation(oe);
                if (rr == null)
                    return;

                if (rr == "end")
                {
                    callback?.Invoke(-2, "模拟完成, 获取最终结果");

                    List<string> endresult = GetEndResult();
                    foreach(string str in endresult)
                        callback?.Invoke(-2, str);

                    callback?.Invoke(-1, "完成");
                    return;
                }


                callback?.Invoke(-2, "获取第 " + (doCount + 1).ToString() + " 次计算数据");

                oe = getOperation();
                if (oe == null)
                    return;

                Globe.DeriveValues.Clear();
                foreach (var v in oe.Parameters)
                {
                    Globe.DeriveValues.Add(v.key, StoZoo.Dog.Global.ReDeserialize<double>(v.value));
                }
            }
        }


        private OperationExchange getOperation()
        {
            KeyValue result = null;

            while (true)
            {
                result = Globe.GETA<KeyValue>(setting.DeriveAddress, "api", "task", "getoperation", setting.DeriveKey);
                if (result == null)
                {
                    callback?.Invoke(-1, "无法连接衍生服务");
                    return null;
                }
                if (result.key != "000" && result.key != "001")
                {
                    callback?.Invoke(-1, result.value.ToString());
                    return null;
                }

                if (result.key == "000")
                {
                    OperationExchange rt = StoZoo.Dog.Global.ReDeserialize<OperationExchange>(result.value);
                    if (rt == null)
                    {
                        callback?.Invoke(-1, "获取计算数据错误");
                        return null;
                    }
                    return rt;
                }

                callback?.Invoke(-2, result.value.ToString());

                if (result.value.ToString() == "task is locked")
                    Thread.Sleep(1000);
                else
                    Thread.Sleep(5000);
            }
        }


        private string PostOperation(OperationExchange oe)
        {
            KeyValue result = null;

            while (true)
            {
                result = Globe.POST<KeyValue>(setting.DeriveAddress, oe, "api", "task", "postoperation");
                if (result == null)
                {
                    callback?.Invoke(-1, "无法连接衍生服务");
                    return null;
                }
                if (result.key != "000" && result.key != "001")
                {
                    callback?.Invoke(-1, result.value.ToString());
                    return null;
                }

                if (result.key == "000")
                {
                    if (result.value.ToString() == "task ended")
                        return "end";
                    else
                        return "";
                }

                if (result.value.ToString() == "task is locked")
                    Thread.Sleep(1000);
                else
                    Thread.Sleep(5000);
            }
        }

        private void Doing(object obj)
        {
            (StoZoo.Dog.StockData.Stock stock, EnvironmentClass e) = ((StoZoo.Dog.StockData.Stock, EnvironmentClass))obj;

            List<string> result = e.environment.RUN(stock);
            if (result != null)
                rts.AddRange(result);

            e.running = false;
        }

        private double MoNi(List<string> rt)
        {

            //模拟操作
            float Amount = 1000000;
            float yongjinRange = 0.00025f;
            float yongjinMin = 5;
            float yinhuaRange = 0.001f;

            float oldAmount = Amount;

            //整理数据
            List<(string, string, DateTime, float, DateTime, float)> lists = new List<(string, string, DateTime, float, DateTime, float)>();
            foreach (string line in rt)
            {
                string[] items = line.Split(",");
                double buy, sale;
                DateTime buyTime, saleTime;
                if (double.TryParse(items[3], out buy) && double.TryParse(items[5], out sale) && DateTime.TryParse(items[2], out buyTime) && DateTime.TryParse(items[4], out saleTime))
                    lists.Add((items[0], items[1], buyTime, (float)buy, saleTime, (float)sale));
            }

            List<(string, string, DateTime, float, DateTime, float, int)> holdings = new List<(string, string, DateTime, float, DateTime, float, int)>();

            lists.Sort((a, b) => DateTime.Compare(a.Item3, b.Item3));

            DateTime thisDate = lists[0].Item3.Date;

            List<double> AllAmounts = new List<double>();

            List<TimeSpan> buytimes = new List<TimeSpan>();

            for (TimeSpan t = new TimeSpan(9, 35, 0); t < new TimeSpan(15, 0, 0); t += new TimeSpan(0, 5, 0))
                if (t <= new TimeSpan(11, 30, 0) || t >= new TimeSpan(13, 0, 0))
                    buytimes.Add(t);

            while (lists.Count > 0 || holdings.Count > 0)
            {
                //List<string> thisdaysaled = new List<string>();
                foreach (TimeSpan buytime in buytimes)
                {
                    //先执行卖出
                    List<(string, string, DateTime, float, DateTime, float, int)> saleList = holdings.Where(p => p.Item5.Date == thisDate && p.Item5.TimeOfDay < buytime).ToList();
                    holdings.RemoveAll(p => saleList.Contains(p));
                    foreach ((string, string, DateTime, float, DateTime, float, int) item in saleList)
                    {
                        float saleAmount = item.Item6 * item.Item7;
                        float yongjin = saleAmount * yongjinRange;
                        if (yongjin < yongjinMin) yongjin = yongjinMin;
                        Amount = Amount + saleAmount - saleAmount * yinhuaRange - yongjin;

                        float ba = item.Item4 * item.Item7;
                        float yj = ba * yongjinRange;
                        if (yj < yongjinMin) yj = yongjinMin;
                    }
                    //再执行买入

                    List<(string, string, DateTime, float, DateTime, float)> buyList = lists.Where(p => p.Item3.Date == thisDate && p.Item3.TimeOfDay == buytime).ToList();
                    lists.RemoveAll(p => buyList.Contains(p));
                    (int count, float buyAmount) = BuyPolicy(Amount * (1 - yongjinRange), buyList.Count, Amount + holdings.Sum(p => p.Item7 * p.Item4));

                    for (int i = 0; i < count; i++)
                    {
                        int index = new Random().Next(0, buyList.Count - 1);
                        int buycount = (int)Math.Floor(buyAmount / buyList[index].Item4 / 100) * 100;
                        float buyamount = buyList[index].Item4 * buycount;
                        float yongjin = buyamount * yongjinRange;
                        if (yongjin < yongjinMin) yongjin = yongjinMin;
                        Amount = Amount - buyamount - yongjin;
                        holdings.Add((buyList[index].Item1, buyList[index].Item2, buyList[index].Item3, buyList[index].Item4, buyList[index].Item5, buyList[index].Item6, buycount));
                        buyList.RemoveAt(index);
                    }
                }


                //2:55卖
                List<(string, string, DateTime, float, DateTime, float, int)> lasttimesaleList = holdings.Where(p => p.Item5.Date == thisDate).ToList();
                holdings.RemoveAll(p => lasttimesaleList.Contains(p));
                foreach ((string, string, DateTime, float, DateTime, float, int) item in lasttimesaleList)
                {
                    float saleAmount = item.Item6 * item.Item7;
                    float yongjin = saleAmount * yongjinRange;
                    if (yongjin < yongjinMin) yongjin = yongjinMin;
                    Amount = Amount + saleAmount - saleAmount * yinhuaRange - yongjin;

                    float ba = item.Item4 * item.Item7;
                    float yj = ba * yongjinRange;
                    if (yj < yongjinMin) yj = yongjinMin;
                }

                //删除所有未使用买入
                lists.RemoveAll(p => p.Item3.Date == thisDate.Date);

                float AA = holdings.Sum(p => p.Item7 * p.Item4);

                //AllAmounts.Add(Amount + AA);

                thisDate = thisDate.AddDays(1);
            }


            return (Amount - oldAmount) / oldAmount;
        }


        private (int, float) BuyPolicy(float amount, int count, float allamount = 0)
        {
            int maxBuyCount = 30;
            float maxBuyRange = 1f;
            float minBuyAmount = 30000;
            float maxBuyAmount = Math.Min(allamount * 1f, 2500000);



            //确定可买金额
            float buyAmount = allamount * maxBuyRange;
            if (buyAmount > amount) buyAmount = amount;
            if (buyAmount < minBuyAmount) buyAmount = amount;
            if (buyAmount < minBuyAmount)
                return (0, 0);

            //确定可买数量和单手金额
            if (count > maxBuyCount) count = maxBuyCount;
            float oneAmount = buyAmount / count;
            if (oneAmount < minBuyAmount)
            {
                oneAmount = minBuyAmount;
                count = (int)Math.Floor(buyAmount / oneAmount);
                oneAmount = buyAmount / count;
            }
            if (oneAmount > maxBuyAmount)
                oneAmount = maxBuyAmount;
            return (count, oneAmount);

        }

        private (double, double, double) GetResult(List<string> rt)
        {
            int allCount = 0;
            int upCount = 0;
            double win = 0;
            int holdday = 0;

            foreach(string line in rt)
            {
                string[] items = line.Split(",");
                double buy, sale;
                int day;
                if (double.TryParse(items[3],out buy) && double.TryParse(items[5], out sale) && int.TryParse(items[7], out day) )
                {
                    allCount++;
                    if (sale > buy) upCount++;
                    win += (sale - buy) / buy;
                    holdday += day;
                }
            }
            if (allCount < 1) return (double.NaN, double.NaN, double.NaN);
            return (upCount * 1.0 / allCount, win / allCount, holdday * 1.0 / allCount);

        }

        private List<string> GetEndResult()
        {
            List<string> rt = new List<string>();
            KeyValue result = Globe.GETA<KeyValue>(setting.DeriveAddress, "api", "task", "gettask", setting.DeriveKey);
            if (result == null)
            {
                rt.Add("无法连接衍生服务");
                return rt;
            }

            if (result.key != "000")
            {
                rt.Add(result.value.ToString());
                return rt;
            }

            TaskExchange task = StoZoo.Dog.Global.ReDeserialize<TaskExchange>(result.value);
            if (task == null)
            {
                rt.Add("获取结果数据错误");
                return rt;
            }

            if (task.Results.Count <= 0 )
            {
                rt.Add("无计算结果数据");
                return rt;
            }

            TaskExchange.TaskResult maxP = null, maxV = null, minT = null, maxM = null ;
            foreach (TaskExchange.TaskResult r in task.Results)
            {
                if (maxP == null || (r.Results.ContainsKey("P") && r.Results["P"] > maxP.Results["P"]))
                    maxP = r;
                if (maxV == null || (r.Results.ContainsKey("V") && r.Results["V"] > maxV.Results["V"]))
                    maxV = r;
                if (minT == null || (r.Results.ContainsKey("T") && r.Results["T"] < minT.Results["T"]))
                    minT = r;
                if (maxM == null || (r.Results.ContainsKey("M") && r.Results["M"] > maxM.Results["M"]))
                    maxM = r;
            }

            if (maxP != null && maxP.Results.ContainsKey("P"))
            {
                rt.Add("最优 P:");
                rt.Add("参数: " + string.Join(", ", maxP.Parameters.Select(p=>p.Key + ": " + p.Value.ToString())));
                rt.Add("结果: " + string.Join(", ", maxP.Results.Select(p => p.Key + ": " + p.Value.ToString())));
            }
            if (maxV != null && maxV.Results.ContainsKey("V"))
            {
                rt.Add("最优 V:");
                rt.Add("参数: " + string.Join(", ", maxV.Parameters.Select(p => p.Key + ": " + p.Value.ToString())));
                rt.Add("结果: " + string.Join(", ", maxV.Results.Select(p => p.Key + ": " + p.Value.ToString())));
            }
            if (minT != null && minT.Results.ContainsKey("T"))
            {
                rt.Add("最优 T:");
                rt.Add("参数: " + string.Join(", ", minT.Parameters.Select(p => p.Key + ": " + p.Value.ToString())));
                rt.Add("结果: " + string.Join(", ", minT.Results.Select(p => p.Key + ": " + p.Value.ToString())));
            }
            if (maxM != null && maxM.Results.ContainsKey("M"))
            {
                rt.Add("最优 M:");
                rt.Add("参数: " + string.Join(", ", maxM.Parameters.Select(p => p.Key + ": " + p.Value.ToString())));
                rt.Add("结果: " + string.Join(", ", maxM.Results.Select(p => p.Key + ": " + p.Value.ToString())));
            }

            //foreach (TaskExchange.TaskResult r in task.Results)
            //{
            //    rt.Add("参数: " + string.Join(", ", r.Parameters.Select(p => p.Key + ": " + p.Value.ToString())));
            //    rt.Add("结果: " + string.Join(", ", r.Results.Select(p => p.Key + ": " + p.Value.ToString())));
            //}

            return rt;

        }

        class EnvironmentClass
        {
            public bool running = false;
            public RunEnvironment environment = null;
        }
    }
}
