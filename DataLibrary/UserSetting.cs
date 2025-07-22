using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.DataLibrary
{
    public class UserSetting
    {
        public string UUID { set; get; }

        public string PCID { set; get; }

        public List <SystemParameterSetting> _SystemParameterSettings { set; get; }

        public List<Parameter> _SavedParameters { set; get; }
        public List<Condition> _Conditions { set; get; }
        public List<Policy> _Policies { set; get; }



        public Dictionary<string, SystemParameterSetting> SystemParameterSettings = new Dictionary<string, SystemParameterSetting>();
        public Dictionary<string, Parameter> SavedParameters = new Dictionary<string, Parameter>();
        public Dictionary<string, Condition> Conditions = new Dictionary<string, Condition>();
        public Dictionary<string, Policy> Policies = new Dictionary<string, Policy>();


        public UserSetting()
        {

        }

        public UserSetting(bool isNew)
        {
            _SystemParameterSettings = new  List<SystemParameterSetting>();
            _SavedParameters = new  List<Parameter>();
            _Conditions = new  List<Condition>();
            _Policies = new  List<Policy>();

            
        }

        public void LoadData()
        {
            foreach (SystemParameterSetting s in _SystemParameterSettings)
                SystemParameterSettings.Add(s.Name, s);
            foreach (Parameter s in _SavedParameters)
                SavedParameters.Add(s.Name, s);
            foreach (Condition s in _Conditions)
                Conditions.Add(s.UUID, s);
            foreach (Policy s in _Policies)
                Policies.Add(s.UUID, s);
        }
    }
}
