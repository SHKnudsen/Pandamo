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
    public static class SelectColumns
    {
        private const string UrlPrefix = @"api/select_columns";

        public static DataFrame ByDatatype(DataFrame dataframe, List<string> datatypes)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;
            arguments.include = JToken.FromObject(datatypes);

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriPostCaller(PythonConstants.webUri + UrlPrefix + "/by_datatype/", arguments);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }
    }
}
