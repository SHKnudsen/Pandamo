using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamoPandas.Pandamo.Pandas;
using DynamoPandas.Pandamo.Constants;
using Newtonsoft.Json.Linq;

namespace DynamoPandas.Pandamo.Format
{
    public static class DataFrameFormatters
    {
        public static string Tabulate(DataFrame dataframe)
        {
            string jsonStr = dataframe.InternalDfJson;

            // Build argument JSON objec
            dynamic arguments = new JObject();
            arguments.jsonStr = jsonStr;

            string dataframeJson = DynamoPandas.Pandamo.PythonRestCall
                .webUriCaller(PythonConstants.webUri + "api/format_dataframe/tabulate/", arguments);
            if (dataframeJson.StartsWith("An error occurred."))
            {
                throw new Exception(dataframeJson);            
            }
            return dataframeJson;

        }
    }
}
