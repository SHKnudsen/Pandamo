using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamoPandas.Constants;
using DynamoPandas.Pandas;
using Newtonsoft.Json.Linq;

namespace DynamoPandas.Reshape
{
    public static class ReshapeDataframe
    {
        /// <summary>
        /// 
        /// </summary>
        private const string UrlPrefix = @"api/reshape_dataframe";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <param name="columns"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public static DataFrame SortValues(DataFrame dataframe, List<string> columns, bool ascending)
        {
            string jsonStr = dataframe.InternalDfJson;
            string columnsStr = string.Join(",", columns.ToList());

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.columns = columnsStr;
            arguments.ascending = ascending;

            string dataframeJson = DynamoPandas.PythonRestCall
                .webUriCaller(PythonConstants.webUri + UrlPrefix + "/sort_values/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <param name="oldValues"></param>
        /// <param name="newValues"></param>
        /// <returns></returns>
        public static DataFrame RenameColumns(DataFrame dataframe, List<string> oldValues, List<string> newValues)
        {
            string jsonStr = dataframe.InternalDfJson;
            string oldValuesStr = string.Join(",", oldValues.ToList());
            string newValuesStr = string.Join(",", newValues.ToList());

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.old_value = oldValuesStr;
            arguments.new_value = newValuesStr;

            string dataframeJson = DynamoPandas.PythonRestCall
                .webUriCaller(PythonConstants.webUri + UrlPrefix + "/rename_columns/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <param name="index"></param>
        /// <param name="columns"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static DataFrame Pivot(DataFrame dataframe, string index, string columns, List<string> values)
        {
            string jsonStr = dataframe.InternalDfJson;
            string valuesStr = string.Join(",", values.ToList());

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.index = index;
            arguments.columns = columns;
            arguments.values = valuesStr;

            string dataframeJson = DynamoPandas.PythonRestCall
                .webUriCaller(PythonConstants.webUri + UrlPrefix + "/pivot_dataframe/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <param name="idVar"></param>
        /// <param name="valueVar"></param>
        /// <returns></returns>
        public static DataFrame Melt(DataFrame dataframe, List<string> idVar, List<string> valueVar)
        {
            string jsonStr = dataframe.InternalDfJson;
            string idVarStr = string.Join(",", idVar.ToList());
            string valueVarStr = string.Join(",", valueVar.ToList());

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.id_var = idVarStr;
            arguments.value_var = valueVarStr;

            string dataframeJson = DynamoPandas.PythonRestCall
                .webUriCaller(PythonConstants.webUri + UrlPrefix + "/melt_dataframe/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }
    }
}
