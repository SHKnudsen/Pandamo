using DynamoPandas.Pandamo.Constants;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoPandas.Pandamo.Server
{
    public static class PandasServer
    {
        private const string UrlPrefix = @"api/server";

        public static string HasServerStarted()
        {
            string response = DynamoPandas.Pandamo.PythonRestCall.webUriGetCaller(PythonConstants.webUri + UrlPrefix + "/has_server_started/");
            return response;
        }
    }
}
