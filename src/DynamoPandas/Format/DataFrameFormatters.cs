using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamoPandas.Pandas;

namespace DynamoPandas.Format
{
    public static class DataFrameFormatters
    {
        public static string Tabulate(DataFrame dataFrame)
        {
            string formattedDataframe = DynamoPandas.PythonRestTest
                .CSharpPythonRestfulApiSimpleTest("http://127.0.0.1:5000/api/v1.0/tabulate_dataframe", dataFrame.InternalDfJson);
            return formattedDataframe;
        }
    }
}
