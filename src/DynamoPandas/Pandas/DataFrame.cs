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

        internal string InternalDfJson => this.dataframeJson;


        internal DataFrame(string dfJson)
        {
            dataframeJson = dfJson;
        }


        public static DataFrame ByDictionary(Dictionary dataDictionary)
        {
            var dict = DictionaryHelpers.ToCDictionary(dataDictionary);
            string jsonStr = JsonConvert.SerializeObject(dict, Formatting.None);
            string dataframeJson = DynamoPandas.PythonRestTest
                .CSharpPythonRestfulApiSimpleTest("http://127.0.0.1:5000/api/v1.0/create_dataframe_from_dict", jsonStr);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }

        public static object ToDictionary(DataFrame dataFrame)
        {
            object obj = JsonHelper.Deserialize(dataFrame.dataframeJson);
            return obj;
        }

    }
}
