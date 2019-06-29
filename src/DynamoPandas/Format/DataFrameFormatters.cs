using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamoPandas.Pandas;
using DynamoPandas.Constants;

namespace DynamoPandas.Format
{
    public static class DataFrameFormatters
    {
        public static string Tabulate(DataFrame dataframe)
        {
            string jsonStr = dataframe.InternalDfJson;
            string dataframeJson = DynamoPandas.PythonRestCall
                .webUriCaller(PythonConstants.webUri + "tabulate_dataframe/" + jsonStr);
            return dataframeJson;
        }
    }
}
