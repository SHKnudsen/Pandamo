using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace DynamoPandas
{
    public static class PythonRestCall
    {

        /// <summary>
        /// C# to call Python HttpWeb RESTful API
        /// </summary>
        /// <param name="uirWebAPI">UIR web api link</param>
        /// <param name="exceptionMessage">Returned exception message</param>
        /// <returns>Web response string</returns>
        public static string webUriCaller(string uirWebAPI)
        {
            string exceptionMessage = string.Empty;
            string webResponse = string.Empty;
            try
            {
                Uri uri = new Uri(uirWebAPI);
                WebRequest httpWebRequest = WebRequest.Create(uri);
                httpWebRequest.ContentType = "application/json";
                WebResponse httpWebResponse = httpWebRequest.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    webResponse = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                exceptionMessage = $"An error occurred. {ex.Message}";
            }
            return webResponse;
        }
    }
}
