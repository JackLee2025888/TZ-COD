using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.OperationLibrary.Formula
{
    public class Parameter
    {
        public Parameter()
        {
          
        }

        public string Name { set; get; }

        public  Formula _formula = null;

        private double _value = double.NaN;

        private Run.RunEnvironment _environment = null;
        public double Value
        {
            get
            {
                if (Name == "ztbefore")
                {
                    int i = 1;

                }
                if (Name == "ztindex")
                {
                    int i = 1;

                }

                if (Globe.DeriveValues.ContainsKey(Name))
                    return Globe.DeriveValues[Name];
                else if (_formula != null)
                    return _formula.Value;
                else if (!double.IsNaN(_value))
                    return _value;
                else if (Name == "time" && _environment != null)
                    return _environment.thisTime.TotalMinutes;
                else
                    return double.NaN;
            }
        }


        public static object GetParameter(string name, Run.RunEnvironment environment)
        {
            if(name.ToLower() == "time")
            {
                return new Parameter()
                {
                    Name = "time",
                    _formula = null,
                    _value = double.NaN,
                    _environment = environment 
                };
            }
            #region 检查系统参数
            Operation.Operation op = Operation.Operation.GetOperation(name, environment, new List<object>());
            if (op != null)
            {
                return new Formula()
                {
                    _operation = op,
                    Parameters = new List<object>(),
                    isLC = op.isLC
                };
            }
            #endregion

            #region 检查自定义参数
            if (Globe.UserSettings.SavedParameters.ContainsKey(name))
            {
                Parameter p = new Parameter()
                {
                    Name = Globe.UserSettings.SavedParameters[name].Name
                };
                if (Globe.UserSettings.SavedParameters[name].FormulaString != "")
                    p._formula = new Formula.FormulaReader().Read(Globe.UserSettings.SavedParameters[name].FormulaString, environment);
                else
                    p._value = Globe.UserSettings.SavedParameters[name].value;

                return p;
            }
            #endregion 

            return null;
        }

    }


}
