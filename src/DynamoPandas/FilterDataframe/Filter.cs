using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamoPandas.PythonProcess;
using DynamoPandas.Constants;
using DynamoPandas.Pandas;
using DesignScript.Builtin;

namespace DynamoPandas.FilterDataframe
{
    public static class Filter
    {
        public static DataFrame ByItems(DataFrame dataframe, List<string> items, int axis)
        {
            string jsonStr = dataframe.InternalDfJson;
            string itemsStr = string.Join(",", items.ToList());
            string dataframeJson = DynamoPandas.PythonRestCall
                .webUriCaller(PythonConstants.webUri + "filter_dataframe/" + jsonStr + "/" + itemsStr + "/" + axis);
            DataFrame df = new DataFrame(dataframeJson);
            return df;
        }
    }
}
