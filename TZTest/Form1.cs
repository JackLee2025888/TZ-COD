using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TZ.OperationLibrary.Formula;

namespace TZ.TZTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(@"D:\StoZooData\LC5\2021\vipdoc\sh\fzline", "sh000009.lc5");
            var res = StoZoo.Dog.StockData.KLine.getLC5(path);
            if (res == null || res.Count == 0)
            {
                return;
            }
            var qujian=  res.Skip(2).Take(100).ToList ();

            string p1= Path.Combine(@"D:\StoZooData", "p1.lc5");
            string p2 = Path.Combine(@"D:\StoZooData", "p2.lc5");
            StoZoo.Dog.StockData.KLine.SaveFile (qujian, p1);
            StoZoo.Dog.StockData.KLine.SaveFile(qujian, p2);
            //bool a= false  && true || true && true ;
            // string b = "||aaa||bbb||";
            //string c= b.Replace("||", "|");

            //Regex regex = new Regex(@"^\w+$");

            //   Regex regex = new Regex(@"^-?[0-9]+(\.?[0-9]*)$"); ;
            // regex = new Regex(@"^([0-9])+(\.[0-9]+)?|\d$");
            // regex = new Regex(@"^(-?[0-9]*[.]*[0-9]{0,3})$");
            // Regex  regex = new Regex(@"^\d*$");
            //Regex regex = new Regex(@"^\d+(\.\d{1,})?$");
            //bool w = regex.IsMatch("123");
            //w = regex.IsMatch("1");
            //w = regex.IsMatch("-1");
            //w = regex.IsMatch("-123");
            //w = regex.IsMatch("123.");
            //w = regex.IsMatch("12335.22");
            //w = regex.IsMatch("0.25");
            //w = regex.IsMatch("-0.25");
            //w = regex.IsMatch("l2+3");
            //w = regex.IsMatch("l2[3aas_TG_");
            //w = regex.IsMatch("l2,s_TG_");
            //string test = "4/2*3";
            ////string test = "Pow[2,3]";
            //Formula.FormulaReader fr = new Formula.FormulaReader();
            //Formula formula = fr.Read(test);
            //double res = formula.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {

           var res= TwoDivide.Mubiao(-2, 2, -1,-5,5,0.1) ;
             int.TryParse(textBox1.Text, out  int n);
            for (int i = 0; i < n; i++)
            {
                res = TwoDivide.Mubiao(res.Item2, res.Item3, res.Item4,-5,5,0.1);
            }
            //while (res.Item1 > TwoDivide.Mubiao(res.Item2, res.Item3, res.Item4).Item1)
            //{
            //    res = TwoDivide.Mubiao(res.Item2, res.Item3, res.Item4);
            //}
            double resy = res.Item1;
            textBox2.Text = resy.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double a = double.NaN;
            double c = double.NaN;
            bool res = a >c;
             res = a < c;
             res = a == c;
            double b = 1 + a;

            TimeSpan ts = new TimeSpan(13, 0, 0);
           double buy= ts.TotalMinutes;
            double sale= (new TimeSpan(14, 30, 0)).TotalMinutes;
            //string str = "";
            //for (int i = 600000; i < 600100; i++)
            //{ str += i.ToString() + ","; }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //string str = Convert.ToString(12, 2);

            //  DateTime dt = DateTime.Now;

            //  DateTime zuotian = dt.Date.AddDays(-1);

            //double m=  (dt - zuotian).TotalDays;
            //  double n = (zuotian-dt  ).TotalDays;
            //int offset = 1 - (int)dt.DayOfWeek;

            double a = 4 * 7.29 * 546 / 100000* 3.28;
            double sh = Math.Sinh(a);
            double shpi = Math.Sinh(a / 180 * Math.PI); ;


            DateTime dt = DateTime.Now;
             int intkey = (int)Math.Ceiling(((double)dt.Minute) / 15.0);
            //string key = dt.Date.AddDays(offset).ToString("yyyyMMdd");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // int totalamount = 10000;
            // List<int> choukacishu = new List<int>();
            // Random random = new Random();
            // for (int ta = 0; ta < totalamount; ta++)
            // {
            //     double thisrate = 0.02;
            //     int cishu = 0;
            //     while (thisrate < random.NextDouble())
            //     {
            //         //if (cishu > 30 && cishu < 50)
            //         //    thisrate = 0.02;
            //         //if (cishu > 50)
            //         //{ thisrate += 0.005; }
            //         cishu++;
            //     }
            //     choukacishu.Add(cishu);
            // }
            //double res= choukacishu.Average();
            // textBox1.Text = res.ToString();


            int totalamount = 10000;
            List<int> choukacishu = new List<int>();
            int n = 14;
            for(int i=0;i<totalamount;i++)
            {
                Random random = new Random();
                List<int> cards = new List<int>();
                Dictionary<int, int> cardtimes = new Dictionary<int, int>();
                while (cardtimes.Count < n)
                {

                    int thiscard = (int)(n * random.NextDouble());
                    cards.Add(thiscard);
                    if (cardtimes.Keys.Contains(thiscard))
                    { cardtimes[thiscard]++; }
                    else
                    {
                        cardtimes.Add(thiscard, 1);
                    }
                }
          
                choukacishu.Add ( cards.Count);
            }
            double aver = choukacishu.Sum() / choukacishu.Count;
            textBox1.Text = aver.ToString();
        }
    }
}
