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

namespace DynamoPandas.Pandamo.Statistics
{
    public static class Stats
    {
        private const string UrlPrefix = @"api/statistics";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static object ZValue(DataFrame dataframe, List<string> columns)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.columns = JToken.FromObject(columns);

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/z_score/", arguments);
            object output = JsonHelper.Deserialize(dataframeJson);
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static DataFrame DropOutliers(DataFrame dataframe, DesignScript.Builtin.Dictionary zValues, int standardDeviation)
        {
            string jsonStr = dataframe.InternalDfJson;
            Dictionary<string, object> zValsDict = DictionaryHelpers.ToCDictionary(zValues);

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.z_values = JToken.FromObject(zValsDict);
            arguments.standard_deviation = JToken.FromObject(standardDeviation);

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/drop_outliers/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataframe"></param>
        /// <param name="zValues"></param>
        /// <param name="standardDeviation"></param>
        /// <returns></returns>
        public static object NaiveBayes(List<List<object>> trainingFeatures, List<object> trainingTarget, List<List<object>> testFeatures)
        {
            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.traning_features = JToken.FromObject(trainingFeatures);
            arguments.traning_targets = JToken.FromObject(trainingTarget);
            arguments.test_features = JToken.FromObject(testFeatures);

            string prediction = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/naive_bayes/", arguments);
            object output = JArray.Parse(prediction).ToObject<List<object>>();
            return output;
        }
    }
}
