using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamoPandas.Pandamo.Pandas;
using DynamoPandas.Pandamo.Constants;
using Newtonsoft.Json.Linq;

namespace DynamoPandas.Pandamo.Select
{
    public static class SelectRows
    {
        private const string UrlPrefix = @"api/select_rows";

        /// <summary>
        /// Selects rows of a dataframe where a value's substring in the specified column
        /// matches the matchString provided
        /// </summary>
        /// <param name="dataframe">DynamoPandas.Pandamo dataframe object</param>
        /// <param name="column">The column to search for the matchString</param>
        /// <param name="matchString">Get's the rows where the columns value matches this substring</param>
        /// <returns></returns>
        public static DataFrame ByMatch(DataFrame dataframe, string column, string matchString)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.column = column;
            arguments.matchString = matchString;

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/by_match/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }
        /// <summary>
        /// Selects rows of a dataframe where the containsString value is in a value
        /// in the specified column
        /// </summary>
        /// <param name="dataframe"></param>
        /// <param name="column"></param>
        /// <param name="containsString"></param>
        /// <returns></returns>
        public static DataFrame ByContains(DataFrame dataframe, string column, string containsString)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.column = column;
            arguments.containsString = containsString;

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/by_contains/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public static DataFrame ByIndex(DataFrame dataframe, List<int> rowIndex)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.rowIndex = JToken.FromObject(rowIndex);

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/by_index/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <param name="rowLabel"></param>
        /// <param name="columnLabel"></param>
        /// <returns></returns>
        public static DataFrame ByLabel(DataFrame dataframe, List<object> rowLabel, List<object> columnLabel)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.rowLabel = JToken.FromObject(rowLabel);
            arguments.columnLabel = JToken.FromObject(columnLabel);

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/by_label/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static DataFrame ByExpression(DataFrame dataframe, string expression)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.boolExpression = expression;

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/by_bool_expression/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }
    }
}