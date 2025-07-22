using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TZ.OperationLibrary.Formula
{
    public class Formula
    {

        public Formula()
        {

        }


        public  Operation.Operation _operation = null;

        public  List<object> Parameters = new List<object>();

        public bool isLC = false;

        public double Value
        {
            get
            {
                if (_operation != null)
                    return _operation.Run(Parameters.ToArray ());
                else
                    return double.NaN;
            }
        }



        public void SetBaseIndex(int index)
        {
            _operation.baseIndex = index;
            foreach (object  obj in Parameters)
            {
                if(obj is Formula )
               ( (Formula) obj).SetBaseIndex(index);
            }
        }

      public  class FormulaReader
        {

            Dictionary<string, object> parentheses = new Dictionary<string, object>();
            public FormulaReader(Dictionary<string, object> par)
            {
                this.parentheses = par;            
            }
            public FormulaReader()
            {          
            }
            public Formula Read(string str, Run.RunEnvironment environment = null)
            {
                str = str.Trim();

                //匹配参数 直接返回空
                if (NameRegex.IsMatch(str))
                    return null;

                Formula result = null;


                //匹配方法
                if (FunctionRegex.IsMatch(str)) //匹配方法
                    result = FindFunction(str, environment);

                if (result != null)
                    return result;



                //解析公式


                //规范符号
                str = str.Replace("||", "|").Replace("&&", "&").Replace("==", "=").Replace(">=", "}").Replace("<=", "{");

                //判断 括号 逻辑
                str = MissParentheses(str,environment);
                if (str == null) return null; 

                //判断 || 逻辑
                string[] Splits = str.Split("|");
                if(Splits.Length > 1)
                {
                    Formula thisFormula = null;
                    object p0 = FindObject(Splits[0].Trim(), environment);
                    if (p0 == null)
                        return null;
                    for(int i = 1; i < Splits.Length; i++)
                    {
                        object p1 = FindObject(Splits[i].Trim(), environment);
                        if (p1 == null)
                            return null;

                        if (i == 1)
                            thisFormula = new Formula()
                            {
                                _operation = Operation.Operation.GetOperation("|", environment, new List<object>() { p0, p1 }),
                                Parameters = new List<object>() { p0, p1 },
                                isLC = countLC(p0, p1)
                            };
                        else
                            thisFormula = new Formula()
                            {
                                _operation = Operation.Operation.GetOperation("|", environment, new List<object>() { thisFormula, p1 }),
                                Parameters = new List<object>() { thisFormula, p1 },
                                isLC = countLC(thisFormula, p1)
                            };
                    }
                    return thisFormula;
                }

                //判断 && 逻辑
                Splits = str.Split("&");
                if (Splits.Length > 1)
                {
                    Formula thisFormula = null;
                    object p0 = FindObject(Splits[0].Trim(), environment);
                    if (p0 == null)
                        return null;
                    for (int i = 1; i < Splits.Length; i++)
                    {
                        object p1 = FindObject(Splits[i].Trim(), environment);
                        if (p1 == null)
                            return null;

                        if (i == 1)
                            thisFormula = new Formula()
                            {
                                _operation = Operation.Operation.GetOperation("&", environment, new List<object>() { p0, p1 }),
                                Parameters = new List<object>() { p0, p1 },
                                isLC = countLC(p0, p1)
                            };
                        else
                            thisFormula = new Formula()
                            {
                                _operation = Operation.Operation.GetOperation("&", environment, new List<object>() { thisFormula, p1 }),
                                Parameters = new List<object>() { thisFormula, p1 },
                                isLC = countLC(thisFormula, p1)
                            };
                    }
                    return thisFormula;
                }

                //判断 比较 逻辑
                Splits = str.Split(new string[] { "<", ">", "{", "}", "=" }, StringSplitOptions.None);
                if (Splits.Length > 2)
                    return null;
                if (Splits.Length == 2)
                {
                    object p0 = FindObject(Splits[0].Trim(),environment);
                    object p1 = FindObject(Splits[1].Trim(), environment);
                    if (p1 == null || p0 == null)
                        return null;

                    string oName = "";
                    if (str.Contains('<'))
                        oName = "<";
                    else if (str.Contains('>'))
                        oName = ">";
                    else if (str.Contains('{'))
                        oName = "<=";
                    else if (str.Contains('}'))
                        oName = ">=";
                    else if (str.Contains('='))
                        oName = "=";
                    if (oName == "")
                        return null;


                    return new Formula()
                    {
                        _operation = Operation.Operation.GetOperation(oName, environment, new List<object>() { p0, p1 }),
                        Parameters = new List<object>() { p0, p1 },
                        isLC = countLC(p0, p1)
                    };
                }

                //判断 加减 逻辑
                for(int i = str.Length-1; i >=0; i--)
                    if (str[i] == '+' || str[i] == '-')
                    {
                        object p1 = FindObject(str.Substring(0, i).Trim(), environment);
                        object p2 = FindObject(str.Substring(i+1, str.Length - i-1).Trim(), environment);
                        if (p1 == null || p2 == null)
                            return null;

                        return new Formula()
                        {
                            _operation = Operation.Operation.GetOperation(str[i].ToString(), environment, new List<object>() { p1, p2 }),
                            Parameters = new List<object>() { p1, p2 },
                            isLC = countLC(p1, p2)
                        };
                    }


                //判断 乘除 逻辑
                for (int i = str.Length - 1; i >= 0; i--)
                    if (str[i] == '*' || str[i] == '/')
                    {
                        object p1 = FindObject(str.Substring(0, i).Trim(), environment);
                        object p2 = FindObject(str.Substring(i + 1, str.Length - i-1).Trim(), environment);
                        if (p1 == null || p2 == null)
                            return null;

                        return new Formula()
                        {
                            _operation = Operation.Operation.GetOperation(str[i].ToString(), environment, new List<object>() { p1, p2 }),
                            Parameters = new List<object>() { p1, p2 },
                            isLC = countLC(p1, p2)
                        };
                    }


                return null;
            }

            private bool countLC(object p1, object p2)
            {
                bool lc1 = false;
                if (p1 is Formula) lc1 = ((Formula)p1).isLC;
                else if (p1 is Parameter && ((Parameter)p1).Name == "time") lc1 = true;
                else if (p1 is Parameter && ((Parameter)p1)._formula !=null ) lc1 =(( (Parameter)p1)._formula).isLC ;
                else if (p1 is Operation.Operation) lc1 = ((Operation.Operation)p1).isLC;
                bool lc2 = false;
                if (p2 is Formula) lc2 = ((Formula)p2).isLC;
                else if (p2 is Parameter && ((Parameter)p2).Name == "time") lc2 = true;
                else if (p2 is Parameter && ((Parameter)p2)._formula != null) lc2 = (((Parameter)p2)._formula).isLC;
                else if (p2 is Operation.Operation) lc2 = ((Operation.Operation)p2).isLC;

                return lc1 | lc2;
            }

            private string MissParentheses(string str, Run.RunEnvironment environment)
            {
                List<int[]> ParenthesesIndex = new List<int[]>();

                //找出所有括号位置
                int count = 0;
                int bIndex = -1;
                for (int i = 0; i < str.Length; i++)
                    if (str[i] == '(')
                    {
                        if(bIndex ==-1)
                        bIndex = i;
                        count++;
                    }
                    else if (str[i] == ')')
                    {
                        count--;
                        if (count == 0)
                        {
                            ParenthesesIndex.Add(new int[] { bIndex, i });
                            bIndex = -1;
                        }
                        if (count < 0)
                            return null;
                    };

                if (count != 0) //小括号不匹配
                    return null;

                //清除括号
                List<string> cuts = new List<string>();
                bIndex = 0;
                foreach (int[] pIndexs in ParenthesesIndex)
                {
                    cuts.Add(str.Substring(bIndex, pIndexs[0] - bIndex));
                    string guid = Guid.NewGuid().ToString("N");
                    cuts.Add(guid);
                    #region 计算括号内
                    object f = FindObject(str.Substring(pIndexs[0] + 1, pIndexs[1] - pIndexs[0] - 1), environment);
                    if (f == null)
                        return null;
                    parentheses.Add(guid, f);
                    #endregion 
                    bIndex = pIndexs[1] + 1;
                }
                cuts.Add(str.Substring(bIndex, str.Length - bIndex));

                return string.Join(' ', cuts);
            }


            Regex FunctionRegex = new Regex(@"^[\w\,\[\]]+$");
            Regex NameRegex = new Regex(@"^\w+$");
            //Regex NumberRegex = new Regex(@"^(([0-9])+(\.([0-9]+))?)|[0-9]+$");
            Regex NumberRegex = new Regex(@"^\d+(\.\d{1,})?$");
            Regex TimeRegex = new Regex(@"^(20|21|22|23|[0-1]\d):[0-5]\d$");
            private Formula FindFunction(string str, Run.RunEnvironment environment)
            {
                string[] sp = str.Split(new char[] { '[', ']' }, StringSplitOptions.None);
                if (sp.Length != 3 || sp[2].Trim() != "" || !NameRegex.IsMatch(sp[0]))
                    return null;




               string[] spp = sp[1].Split(",");

                List<object> parameters = new List<object>();
                foreach (string pp in spp)
                {
                    string p = pp.Trim();
                    if (NumberRegex.IsMatch(p))
                    {
                        double vt = 0;
                        if (!double.TryParse(p, out vt))
                            return null;
                        parameters.Add(vt);
                    }

                    else if (NameRegex.IsMatch(p))
                    {
                        object pt = FindParameter(p, environment);
                        if (pt == null)
                            return null;
                        parameters.Add(pt);
                    }
                    else
                        return null;
                }
                Operation.Operation op = Operation.Operation.GetOperation(sp[0], environment, parameters);
                if (op == null)
                    return null;


                return new Formula()
                {
                    _operation = op,
                    Parameters = parameters,
                    isLC = op.isLC
                };


            }

            private object FindParameter(string str, Run.RunEnvironment environment)
            {
                return Parameter.GetParameter(str, environment);
            }

            private object FindObject(string str, Run.RunEnvironment environment)
            {
                if (TimeRegex.IsMatch(str))
                {
                    string[] vs = str.Split(":");
                    int hh, mm;
                    if (!int.TryParse(vs[0], out hh) || !int.TryParse(vs[1], out mm))
                        return null;
                    TimeSpan t = new TimeSpan(hh, mm, 0);
                    return t.TotalMinutes;
                }
                else if (NumberRegex.IsMatch(str))
                {
                    double v = 0;
                    if (double.TryParse(str, out v))
                        return v;
                    else
                        return null;
                }
                else if (NameRegex.IsMatch(str))
                {
                    if (parentheses.ContainsKey(str))
                        return parentheses[str];
                    else
                        return FindParameter(str, environment);
                }
                else if (FunctionRegex.IsMatch(str))
                    return FindFunction(str, environment);
                else
                {
                    FormulaReader fr = new FormulaReader(parentheses);
                    return fr.Read(str, environment);
                }
            }
        }
    }

}
