using Autodesk.DesignScript.Runtime;
using DynamoPandas.Pandamo.Constants;
using DynamoPandas.Pandamo.Pandas;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoPandas.Pandamo.Combine
{
    public static class CombineDataframe
    {
        public static DataFrame Merge(DataFrame leftDf, DataFrame rightDf, [DefaultArgument("null")] string leftOn, [DefaultArgument("null")] string rightOn, bool leftIndex = false, bool rightIndex = false, string how = "inner")
        {
            string leftDfJson = leftDf.InternalDfJson;
            string rightDfJson = rightDf.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.leftDf = leftDfJson;
            arguments.rightDf = rightDfJson;
            arguments.how = how;
            arguments.left_on = leftOn;
            arguments.right_on = rightOn;
            arguments.left_index = leftIndex;
            arguments.right_index = rightIndex;

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriCaller(PythonConstants.webUri + "api/combine/merge/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }

        public static DataFrame Concatenate(List<DataFrame> dataframes, int axis, bool ignoreIndex = false, string join = "outer")
        {
            List<string> dfJsonList = new List<string>();

            foreach (DataFrame d in dataframes)
            {
                dfJsonList.Add(d.InternalDfJson);
            }

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.df_json_list = JToken.FromObject(dfJsonList);
            arguments.axis = JToken.FromObject(axis);
            arguments.ignore_index = ignoreIndex;
            arguments.join = join;

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriCaller(PythonConstants.webUri + "api/combine/concatenate/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }
    }
}
