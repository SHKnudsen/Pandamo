using Autodesk.DesignScript.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoPandas.Pandamo.Constants
{
    [IsVisibleInDynamoLibrary(false)]
    public class PythonConstants
    {
        public const string webUri = "http://127.0.0.1:5000/";
        public const string StartServerBat = @"C:\Users\SylvesterKnudsen\Miniconda3\Scripts\startPandamoServer.bat";
    }
}
