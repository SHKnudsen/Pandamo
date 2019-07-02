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
    public static class Filter
    {
        /// <summary>
        /// Filters a dataframe by the given items.
        /// To filter by columns set axis = 1
        /// To filter by index set axis = 0
        /// </summary>
        /// <param name="dataframe">Pandamo Dataframe object</param>
        /// <param name="items">List of strings, with items to filer out</param>
        /// <param name="axis">0=indexes, 1=columns</param>
        /// <returns></returns>
        public static DataFrame ByItems(DataFrame dataframe, List<string> items, int axis)
        {
            string jsonStr = dataframe.InternalDfJson;
            string itemsStr = string.Join(",", items.ToList());
            string dataframeJson = DynamoPandas.PythonRestCall
                .webUriCaller(PythonConstants.webUri + "api/filter_dataframe/by_items/" + jsonStr + "/" + itemsStr + "/" + axis);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }
        /// <summary>
        /// Filters a dataframe by a given regex.
        /// To filter by columns set axis = 1
        /// To filter by index set axis = 0
        /// </summary>
        /// <param name="dataframe">Pandamo Dataframe object</param>
        /// <param name="item">regex string to filter by</param>
        /// <param name="axis">0=indexes, 1=columns</param>
        /// <returns></returns>
        public static DataFrame ByRegex(DataFrame dataframe, string item, int axis)
        {
            string jsonStr = dataframe.InternalDfJson;
            string dataframeJson = DynamoPandas.PythonRestCall
                .webUriCaller(PythonConstants.webUri + "api/filter_dataframe/by_regex/" + jsonStr + "/" + item + "/" + axis);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }
        /// <summary>
        /// Filters a dataframe by a given substring.
        /// To filter by columns set axis = 1
        /// To filter by index set axis = 0
        /// </summary>
        /// <param name="dataframe">Pandamo Dataframe object</param>
        /// <param name="item">substring to filter by</param>
        /// <param name="axis">0=indexes, 1=columns</param>
        /// <returns></returns>
        public static DataFrame ByContains(DataFrame dataframe, string item, int axis)
        {
            string jsonStr = dataframe.InternalDfJson;
            string dataframeJson = DynamoPandas.PythonRestCall
                .webUriCaller(PythonConstants.webUri + "api/filter_dataframe/by_contains/" + jsonStr + "/" + item + "/" + axis);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }
    }
}
