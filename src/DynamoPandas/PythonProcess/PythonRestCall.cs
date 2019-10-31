using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Autodesk.DesignScript.Runtime;
using DynamoPandas.Pandamo.Utilities;

namespace DynamoPandas.Pandamo
{
    [IsVisibleInDynamoLibrary(false)]
    public static class PythonRestCall
    {

        /// <summary>
        /// C# to call Python HttpWeb RESTful API
        /// </summary>
        /// <param name="apiUri">Uri web api link</param>
        /// <param name="exceptionMessage">Returned exception message</param>
        /// <returns>Web response string</returns>
        public static string webUriPostCaller(string apiUri, JObject argumentDict, string method = "POST")
        {
            try
            {
                string webResponse = string.Empty;
                Uri uri = new Uri(apiUri);
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = method;
                using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(argumentDict.ToString());
                }

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    webResponse = streamReader.ReadToEnd();
                }
                return webResponse;
            }
            catch (WebException ex)
            {
                string genericWarningMessage = $"An error occurred:\n";
                if (ex.Status == WebExceptionStatus.ConnectFailure)
                {
                    return genericWarningMessage + ex.Message;
                }  
                var speceficWarningMessage = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                
                return genericWarningMessage + speceficWarningMessage;
            }
        }

        public static string webUriGetCaller(string apiUri, string method="GET")
        {
            string html = string.Empty;
            Uri uri = new Uri(apiUri);

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(apiUri);
            httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;
            httpWebRequest.Method = method;

            using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            return html;
        }
    }
}
