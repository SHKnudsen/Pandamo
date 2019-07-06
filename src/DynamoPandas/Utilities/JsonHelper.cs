using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DynamoPandas.Pandamo.Utilities
{
    public static class JsonHelper
    {
        public static object Deserialize(string json)
        {
            List<string> keys = new List<string>();
            List<object> values = new List<object>();
            JObject jObject = JObject.Parse(json);
            foreach (JProperty prop in jObject.Properties())
            {
                keys.Add(prop.Name);
                object value = (prop.Value.ToObject<object>() == null) ? "null" : ToObject(prop.Value);
                values.Add(value);
            }
            Dictionary<string, object> dic = keys.Zip(values, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
            return dic;
        }

        private static object ToObject(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    return token.Children<JProperty>()
                                .ToDictionary(prop => prop.Name,
                                              prop => ToObject(prop.Value));

                case JTokenType.Array:
                    return token.Select(ToObject).ToList();
                case JTokenType.Integer:
                case JTokenType.Float:
                case JTokenType.String:
                case JTokenType.Boolean:

                default:
                    return ((JValue)token).Value;
            }
        }
    }
}
