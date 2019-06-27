using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoPandas.Utilities
{
    public static class DictionaryHelpers
    {
        public static Dictionary<string,object> ToCDictionary(DesignScript.Builtin.Dictionary dictionary)
        {
            List<string> keys = dictionary.Keys.ToList();
            List<object> values = new List<object>();
            foreach (var value in dictionary.Values)
            {
                //TODO: add support for deeply nested dictionarys
                if (value.GetType() == typeof(DesignScript.Builtin.Dictionary))
                {
                    DesignScript.Builtin.Dictionary subDict = value as DesignScript.Builtin.Dictionary;
                    List<string> subKeys = subDict.Keys.ToList();
                    List<object> subVals = subDict.Values.ToList();
                    var newDict = subKeys.Zip(subVals, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
                    values.Add(newDict);
                }
                else
                {
                    values.Add(value);
                }
            }
            Dictionary<string,object> cDict = keys.Zip(values, (k, v) => new { k, v })
                .ToDictionary(x => x.k, x => x.v);
            return cDict;
        }
    }
}
