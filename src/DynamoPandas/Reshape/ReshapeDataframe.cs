using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Runtime;
using DynamoPandas.Pandamo.Constants;
using DynamoPandas.Pandamo.Pandas;
using Newtonsoft.Json.Linq;

namespace DynamoPandas.Pandamo.Reshape
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

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.columns = JToken.FromObject(columns);
            arguments.ascending = ascending;

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/sort_values/", arguments);
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

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.old_value = JToken.FromObject(oldValues);
            arguments.new_value = JToken.FromObject(newValues);

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/rename_columns/", arguments);
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

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.index = index;
            arguments.columns = columns;
            arguments.values = JToken.FromObject(values);

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/pivot_dataframe/", arguments);
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

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.id_var = JToken.FromObject(idVar);
            arguments.value_var = JToken.FromObject(valueVar);

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/melt_dataframe/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static DataFrame DropRows(DataFrame dataframe, List<int> index)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.indexToDrop = JToken.FromObject(index);

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/drop_rows/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static DataFrame DropColumns(DataFrame dataframe, List<string> columns)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.columnsToDrop = JToken.FromObject(columns);

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/drop_columns/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <param name="axis"></param>
        /// <param name="resetIndex"></param>
        /// <returns></returns>
        public static DataFrame ReverseDataframe(DataFrame dataframe, int axis = 1, bool resetIndex = false)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.axis = axis;
            arguments.reset_index = resetIndex;

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/reverse_dataframe/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <param name="columns"></param>
        /// <param name="numberTypes"></param>
        /// <returns></returns>
        public static DataFrame StringsToNumbers(DataFrame dataframe, List<string> columns, List<string> numberTypes)
        {
            //CONSIDER IF THIS NODE IS NEEDED!
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.strings = JToken.FromObject(columns);
            arguments.numberTypes = JToken.FromObject(numberTypes);

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/strings_to_numbers/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <returns></returns>
        public static DataFrame FillNa(DataFrame dataframe)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            
            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/fill_na/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static DataFrame ToDateTime(DataFrame dataframe, string column)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.column = column;

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/to_datetime/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <returns></returns>
        public static DataFrame DropNa(DataFrame dataframe)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/drop_na/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <returns></returns>
        public static DataFrame GetDummies(DataFrame dataframe)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/get_dummies/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }
    }
}
