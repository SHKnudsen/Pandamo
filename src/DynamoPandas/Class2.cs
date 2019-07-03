using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript;
using DesignScript.Builtin;
using Newtonsoft.Json;
using DynamoPandas.Constants;
using DynamoPandas.PythonProcess;
using DynamoPandas.Pandas;

namespace DynamoPandas
{
    public static class Test
    {
        public static string HelloWorldTest(Dictionary message)
        {
            string pythonScript = @"test.py";
            string jsonstr = JsonConvert.SerializeObject(message, Formatting.None);
            string arguments = string.Format("{0} {1}", pythonScript, jsonstr);
            string output = NewProcess.CreateNewProcess(arguments);

            return output;
        }
    }
}