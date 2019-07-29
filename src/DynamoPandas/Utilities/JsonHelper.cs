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

        public static object DeserializeWithNodeStructure(string json)
        {
            JObject jObject = JObject.Parse(json);
            List<JProperty> jProperty = jObject.Properties().ToList();
            List<JToken> cols = jProperty.Where(prop => prop.Name == "columns").Values().ToList();
            List<JToken> data = jProperty.Where(prop => prop.Name == "data").Values().ToList();
            List<JToken> index = jProperty.Where(prop => prop.Name == "index").Values().ToList();
            List<string> keys = new List<string>();
            List<object> values = new List<object>();

            var dataValues = data.Values().ToList();
            var transposedValues = dataValues
                .SelectMany(inner => inner.Select((item, test) => new { item, test }))
                .GroupBy(i => i.test, i => i.item)
                .Select(g => g.ToList())
                .ToList();

            List<JToken> keyTokens = index.Values().ToList();
            int idx = 0;
            foreach (var col in cols.Values().ToList())
            {
                string key = col.Value<string>();
                keys.Add(key);
                List<string> subKeys = new List<string>();
                foreach (JToken token in keyTokens)
                {
                    subKeys.Add(token.Value<string>());
                }
                List<object> subValues = new List<object>();
                foreach (JToken valueToken in transposedValues[idx])
                {
                    subValues.Add(ToObject(valueToken));
                }

                Dictionary<string, object> dic = subKeys.Zip(subValues, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
                values.Add(dic);
                idx++;
            }

            Dictionary<string, object> dict = keys.Zip(values, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
            return dict;
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
