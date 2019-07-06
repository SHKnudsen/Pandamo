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
                .webUriCaller(PythonConstants.webUri + "api/select_rows/by_match/", arguments);
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
                .webUriCaller(PythonConstants.webUri + "api/select_rows/by_contains/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }
    }
}