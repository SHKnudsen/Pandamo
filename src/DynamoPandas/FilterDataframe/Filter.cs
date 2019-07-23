using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamoPandas.Pandamo.Constants;
using DynamoPandas.Pandamo.Pandas;
using DesignScript.Builtin;
using Newtonsoft.Json.Linq;

namespace DynamoPandas.Pandamo.FilterDataframe
{
    public static class Filter
    {
        /// <summary>
        /// Filters a dataframe by the given items.
        /// To filter by columns set axis = 1
        /// To filter by index set axis = 0
        /// </summary>
        /// <param name="dataframe">DynamoPandas.Pandamo Dataframe object</param>
        /// <param name="items">List of strings, with items to filer out</param>
        /// <param name="axis">0=indexes, 1=columns</param>
        /// <returns></returns>
        public static DataFrame ByItems(DataFrame dataframe, List<string> items, int axis)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.items = JToken.FromObject(items);
            arguments.axis = axis;

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriCaller(PythonConstants.webUri + "api/filter_dataframe/by_items/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }

        /// <summary>
        /// Filters a dataframe by a given regex.
        /// To filter by columns set axis = 1
        /// To filter by index set axis = 0
        /// </summary>
        /// <param name="dataframe">DynamoPandas.Pandamo Dataframe object</param>
        /// <param name="item">regex string to filter by</param>
        /// <param name="axis">0=indexes, 1=columns</param>
        /// <returns></returns>
        public static DataFrame ByRegex(DataFrame dataframe, string item, int axis)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.item = item;
            arguments.axis = axis;

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriCaller(PythonConstants.webUri + "api/filter_dataframe/by_regex/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }
        /// <summary>
        /// Filters a dataframe by a given substring.
        /// To filter by columns set axis = 1
        /// To filter by index set axis = 0
        /// </summary>
        /// <param name="dataframe">DynamoPandas.Pandamo Dataframe object</param>
        /// <param name="item">substring to filter by</param>
        /// <param name="axis">0=indexes, 1=columns</param>
        /// <returns></returns>
        public static DataFrame ByContains(DataFrame dataframe, string item, int axis)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.item = item;
            arguments.axis = axis;

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriCaller(PythonConstants.webUri + "api/filter_dataframe/by_contains/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }
    }
}
