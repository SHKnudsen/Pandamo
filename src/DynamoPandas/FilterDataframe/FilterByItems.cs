using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamoPandas.PythonProcess;
using DynamoPandas.Constants;
using DynamoPandas.Pandas;
using DesignScript.Builtin;

namespace DynamoPandas.FilterDataframe
{
    public static class FilterByItems
    {
        public static DataFrame ByItems(DataFrame dataframe, List<string> items, int axis)
        {
            string pythonScriptPath = @"filters\filter_dataframe.py";
            string jsonstr = dataframe.ToJsonStr();
            string itemsString = String.Join(",", items);
            string argumentString = string.Format("{0} {1} {2} {3}", pythonScriptPath, jsonstr, itemsString, axis);
            string processOutput = NewProcess.CreateNewProcess(argumentString);
            return new DataFrame(processOutput);
        }
    }
}
