using System;

namespace StoZoo.Dog
{
    public class KeyValue
    {

        public KeyValue(string key, object value)
        {
            this.key = key;
            this.value = value;
        }
        public string key { set; get; }
        public object value { set; get; }
    }
}
