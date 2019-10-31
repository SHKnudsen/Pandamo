using DynamoPandas.Pandamo.Constants;
using DynamoPandas.Pandamo.Pandas;
using DynamoPandas.Pandamo.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoPandas.Pandamo.Information
{
    public static class BasicInformation
    {
        private const string UrlPrefix = @"api/basic_information";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <returns></returns>
        public static Dictionary<string, int> Shape(DataFrame dataframe)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;

            string response = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/shape/", arguments);

            List<int> shapeData = JArray.Parse(response).ToObject<List<int>>();
            
            var shapeInfo = new Dictionary<string, int>()
            {
                { "Rows", shapeData[0] },
                { "Columns", shapeData[1]}

            };

            return shapeInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <returns></returns>
        public static object Index(DataFrame dataframe)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;

            string response = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/index/", arguments);
            object indexInfo = JsonHelper.Deserialize(response);
 
            return indexInfo;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <returns></returns>
        public static object Columns(DataFrame dataframe)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;

            string response = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/columns/", arguments);
            object columnsInfo = JsonHelper.Deserialize(response);
            return columnsInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <returns></returns>
        public static string Info(DataFrame dataframe)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;

            string response = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/info/", arguments);
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <returns></returns>
        public static object Count(DataFrame dataframe)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;

            string response = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/count/", arguments);
            object countInfo = JsonHelper.Deserialize(response);
            return countInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <returns></returns>
        public static object DataTypes(DataFrame dataframe)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;

            string response = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/datatypes/", arguments);
            object datatypesInfo = JsonHelper.Deserialize(response);
            return datatypesInfo;
        }
    }
}
