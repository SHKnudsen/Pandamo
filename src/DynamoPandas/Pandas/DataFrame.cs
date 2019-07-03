using DesignScript.Builtin;
using DynamoPandas.Constants;
using DynamoPandas.PythonProcess;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Autodesk.DesignScript.Runtime;

namespace DynamoPandas.Pandas
{
    public class DataFrame
    {
        private string dataframeStr;

        public string DataFrameStr {
            get
            {
                string jsonStr = this.ToJsonStr();
                string pythonScriptPath = @"format\tabulate_dataframe.py";
                string argumentString = string.Format("{0} {1}", pythonScriptPath, jsonStr);
                string processOutput = NewProcess.CreateNewProcess(argumentString);
                return processOutput;
            }
        }

        internal DataFrame(string df)
        {
            dataframeStr = df;
        }

        private const string dataframe = "DataFrame";
        private const string elapsedtime = "ElapsedTime";
        [MultiReturn(new[] { dataframe, elapsedtime })]
        public static Dictionary<string,object> ByDictionary(Dictionary dataDictionary)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string jsonStr = JsonConvert.SerializeObject(dataDictionary, Formatting.None);
            string pythonScriptPath = @"create_dataframe\from_converted_dyn_dict.py";
            string argumentString = string.Format("{0} {1}", pythonScriptPath, jsonStr);
            string dataframeString = NewProcess.CreateNewProcess(argumentString);
            stopwatch.Stop();
            string time = stopwatch.ElapsedMilliseconds.ToString();
            DataFrame df = new DataFrame(dataframeString);
            //return new DataFrame(dataframeString);
            return new Dictionary<string,object>()
            {
                {dataframe, df },
                {elapsedtime, time}
            };
        }

        internal List<KeyValuePair<string,object>> ToDictionary()
        {
            List<string> splitstring = dataframeStr.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            List<string> keys = splitstring[0].Split(' ').ToList().Skip(2).ToList();
            List<List<string>> values = OrganizeValues(splitstring.Skip(1).ToList());
            var pairs = keys.Zip(values, (a, b) => new KeyValuePair<string, object>(a, b)).ToList();
            return pairs;
        }

        internal string ToJsonStr()
        {
            var dict = this.ToDictionary();
            string jsonStr = JsonConvert.SerializeObject(dict, Formatting.None);
            return jsonStr;
        }

        private static List<List<string>> OrganizeValues(List<string> values)
        {
            List<List<string>> organizedValues = new List<List<string>>();
            foreach (var str in values)
            {
                if (str == "")
                {
                    continue;
                }
                List<string> valueList = str.Split(' ').Skip(1).ToList();                
                organizedValues.Add(valueList.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct().ToList());
            }
            return Transpose(organizedValues);
        }

        private static List<List<string>> Transpose(List<List<string>> lists)
        {
            int maxLength = lists.Max(subList => subList.Count);
            List<List<string>> transposedList = Enumerable.Range(0, maxLength).Select(i => new List<string>()).ToList();

            foreach (var sublist in lists)
            {
                for (int i = 0; i < transposedList.Count; i++)
                {
                    transposedList[i].Add(i < sublist.Count ? sublist[i] : null);
                }
            }
            return transposedList;
        }
    }
}
