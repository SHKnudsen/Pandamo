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
using System.Text.RegularExpressions;

namespace DynamoPandas.Pandas
{
    public class DataFrame
    {
        private string dataframeJson;

        public string DataFrameJson => this.dataframeJson.ToFormattedString();

        internal string InternalDfJson => this.dataframeJson;


        internal DataFrame(string dfJson)
        {
            dataframeJson = dfJson;
        }


        public static DataFrame ByDictionary(Dictionary dataDictionary)
        {
            var dict = DictionaryHelpers.ToCDictionary(dataDictionary);
            string jsonStr = JsonConvert.SerializeObject(dict, Formatting.None);
            string dataframeJson = DynamoPandas.PythonRestCall
                .webUriCaller(PythonConstants.webUri + "create_dataframe_from_dict/" + jsonStr);
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
