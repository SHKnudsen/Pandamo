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
    public static class PythonRestTest
    {

        /// <summary>
        /// C# test to call Python HttpWeb RESTful API
        /// </summary>
        /// <param name="uirWebAPI">UIR web api link</param>
        /// <param name="exceptionMessage">Returned exception message</param>
        /// <returns>Web response string</returns>
        public static string CSharpPythonRestfulApiSimpleTest(string uirWebAPI, string arguments)
        {
            string exceptionMessage = string.Empty;
            string webResponse = string.Empty;
            try
            {
                Uri uri = new Uri(uirWebAPI);
                WebRequest httpWebRequest = WebRequest.Create(uri);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {                 
                    streamWriter.Write(arguments);
                }
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

        /// <summary>
        /// C# test to call Python HttpWeb RESTful API
        /// </summary>
        /// <param name="uirWebAPI">UIR web api link</param>
        /// <param name="exceptionMessage">Returned exception message</param>
        /// <returns>Web response string</returns>
        public static string test2(string uirWebAPI)
        {
            string exceptionMessage = string.Empty;
            string webResponse = string.Empty;
            try
            {
                Uri uri = new Uri(uirWebAPI);
                WebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    // Build employee test JSON objec
                    dynamic employee = new JObject();
                    employee.username = "theUserName";
                    employee.password = "thePassword";
                    string s = employee.ToString();
                    streamWriter.Write(employee.ToString());
                }
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
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
