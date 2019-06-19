using DesignScript.Builtin;
using DynamoPandas.Constants;
using DynamoPandas.PythonProcess;
using DynamoPandas.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Autodesk.DesignScript.Runtime;
using DynamoPandas;
using System.Text.RegularExpressions;

namespace DynamoPandas.Pandas
{
    public class DataFrame
    {
        private string dataframeJson;

        public string DataFrameJson { get { return this.dataframeJson.ToFormattedString(); } }


        internal DataFrame(string dfJson)
        {
            dataframeJson = dfJson;
        }


        public static DataFrame ByDictionary(Dictionary dataDictionary)
        {
            List<string> keys = dataDictionary.Keys.ToList();
            List<object> vals = dataDictionary.Values.ToList();
            var dict = keys.Zip(vals, (k, v) => new { k, v })
                .ToDictionary(x => x.k, x => x.v);
            string jsonStr = JsonConvert.SerializeObject(dict, Formatting.None);
            string dataframeString = DynamoPandas.PythonRestTest
                .CSharpPythonRestfulApiSimpleTest("http://127.0.0.1:5000/api/v1.0/create_dataframe_from_dict", jsonStr);
            DataFrame df = new DataFrame(dataframeString);
            return df;
        }

        public static Dictionary<string, object> ToDictionary(DataFrame dataFrame)
        {
            var obj = JObject.Parse(dataFrame.dataframeJson);
            var dict = new Dictionary<string, object>();
            foreach (var property in obj)
            {
                var name = property.Key;
                var value = property.Value;

                if (value is JArray)
                {
                    dict.Add(name, value.ToArray());
                }
                else if (value is JValue)
                {
                    dict.Add(name, value.ToString());
                }
                else if (value is JObject)
                {
                    foreach (var item in value)
                    {   
                        if (value is JArray)
                        {
                            dict.Add(name, value.ToArray());
                        }
                        else if (value is JValue)
                        {
                            dict.Add(name, value.ToString());
                        }
                    }
                }
                else
                {
                    throw new NotSupportedException("Invalid JSON token type.");
                }
            }

            return dict;
        }

    }
}
