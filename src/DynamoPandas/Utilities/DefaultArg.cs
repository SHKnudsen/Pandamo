using Autodesk.DesignScript.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoPandas.Pandamo.Utilities
{
    [IsVisibleInDynamoLibrary(false)]
    public static class DefaultArg
    {
        public static object GetNull()
        {
            return null;
        }
    }
}
