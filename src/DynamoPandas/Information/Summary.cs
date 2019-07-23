using DynamoPandas.Pandamo.Constants;
using DynamoPandas.Pandamo.Pandas;
using DynamoPandas.Pandamo.Utilities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoPandas.Pandamo.Information
{
    public static class Summary
    {
        private const string UrlPrefix = @"api/summary";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static object Sum(DataFrame dataframe, int axis = 0)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.axis = axis;

            string response = DynamoPandas.Pandamo.PythonRestCall
                .webUriCaller(PythonConstants.webUri + UrlPrefix + "/sum/", arguments);
            object summaryDict = JsonHelper.Deserialize(response);
            return summaryDict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <returns></returns>
        public static object CumulativeSum(DataFrame dataframe)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;

            string response = DynamoPandas.Pandamo.PythonRestCall
                .webUriCaller(PythonConstants.webUri + UrlPrefix + "/cumulative_sum/", arguments);
            object summaryDict = JsonHelper.Deserialize(response);
            return summaryDict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <returns></returns>
        public static object MaxValues(DataFrame dataframe)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;

            string response = DynamoPandas.Pandamo.PythonRestCall
                .webUriCaller(PythonConstants.webUri + UrlPrefix + "/max_value/", arguments);
            object summaryDict = JsonHelper.Deserialize(response);
            return summaryDict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <returns></returns>
        public static object MinValues(DataFrame dataframe)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;

            string response = DynamoPandas.Pandamo.PythonRestCall
                .webUriCaller(PythonConstants.webUri + UrlPrefix + "/min_value/", arguments);
            object summaryDict = JsonHelper.Deserialize(response);
            return summaryDict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static object MaxValueIndex(DataFrame dataframe, int axis)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.axis = axis;

            string response = DynamoPandas.Pandamo.PythonRestCall
                .webUriCaller(PythonConstants.webUri + UrlPrefix + "/max_index_value/", arguments);
            object summaryDict = JsonHelper.Deserialize(response);
            return summaryDict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static object MinValueIndex(DataFrame dataframe, int axis)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.axis = axis;

            string response = DynamoPandas.Pandamo.PythonRestCall
                .webUriCaller(PythonConstants.webUri + UrlPrefix + "/min_index_value/", arguments);
            object summaryDict = JsonHelper.Deserialize(response);
            return summaryDict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <returns></returns>
        public static DataFrame Describe(DataFrame dataframe)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;

            string response = DynamoPandas.Pandamo.PythonRestCall
                .webUriCaller(PythonConstants.webUri + UrlPrefix + "/describe/", arguments);
            DataFrame df = new DataFrame(response);
            return df;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <returns></returns>
        public static object Median(DataFrame dataframe)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;

            string response = DynamoPandas.Pandamo.PythonRestCall
                .webUriCaller(PythonConstants.webUri + UrlPrefix + "/median/", arguments);
            object summaryDict = JsonHelper.Deserialize(response);
            return summaryDict;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <returns></returns>
        public static object Mean(DataFrame dataframe)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;

            string response = DynamoPandas.Pandamo.PythonRestCall
                .webUriCaller(PythonConstants.webUri + UrlPrefix + "/mean/", arguments);
            object summaryDict = JsonHelper.Deserialize(response);
            return summaryDict;
        }

        public static object Unique(DataFrame dataframe)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;

            string response = DynamoPandas.Pandamo.PythonRestCall
                .webUriCaller(PythonConstants.webUri + UrlPrefix + "/unique/", arguments);
            object summaryDict = JsonHelper.Deserialize(response);
            return summaryDict;
        }
    }
}
