using DesignScript.Builtin;
using DynamoPandas.Pandamo.Constants;
using DynamoPandas.Pandamo.Utilities;
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
using System.Collections.Specialized;


namespace DynamoPandas.Pandamo.Pandas
{
    public class DataFrame
    {
        private string dataframeJson;

        public string DataFrameJson => this.dataframeJson.ToFormattedString();

        internal string InternalDfJson => this.dataframeJson;


        internal DataFrame(string dfJson)
        {
            if (dfJson.StartsWith("An error occurred:"))
            {
                throw new Exception(dfJson);
            }
            dataframeJson = dfJson;
        }


        public static DataFrame ByDictionary(Dictionary dataDictionary)
        {
            var dict = DictionaryHelpers.ToCDictionary(dataDictionary);
            string jsonStr = JsonConvert.SerializeObject(dict, Formatting.None);

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriCaller(PythonConstants.webUri + "api/create_dataframe/by_dict/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }

        public static DataFrame ByExcel(string filePath, string sheetName)
        {
            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.filePath = filePath;
            arguments.sheetName = sheetName;

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriCaller(PythonConstants.webUri + "api/create_dataframe/by_excel/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }

        public static DataFrame ByColumnsValues(List<string> columns, List<List<object>> values)
        {
            OrderedDictionary orderedDictionary = new OrderedDictionary();
            for (int i = 0; i < columns.Count; i++)
            {
                orderedDictionary.Add(columns[i], values[i]);
            }
            string jsonStr = JsonConvert.SerializeObject(orderedDictionary, Formatting.None);
            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriCaller(PythonConstants.webUri + "api/create_dataframe/by_dict/", arguments);
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
