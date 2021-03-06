﻿using Autodesk.DesignScript.Runtime;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DynamoPandas.Pandamo.Utilities
{
    [IsVisibleInDynamoLibrary(false)]
    public static class StringExtensions
    {
        public static string ToFormattedString(this string jsonStr)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(jsonStr);
            string formattedStr = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
            return formattedStr;
        }
    }
}
