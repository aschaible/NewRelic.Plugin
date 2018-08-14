using System;
using System.Collections.Generic;
using NewRelicPlugin.RestApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NewRelicPlugin.RestApi.JsonConverter
{
    public class MetricListJsonConverter : JsonConverter<List<Metric>>
    {
        public override void WriteJson(JsonWriter writer, List<Metric> value, JsonSerializer serializer)
        {
            writer.WriteStartObject();

            foreach (var metric in value)
            {
                writer.WritePropertyName(metric.MetricName);
                var token = JToken.FromObject(metric.Value, serializer);
                writer.WriteToken(token.CreateReader());
            }

            writer.WriteEndObject();
        }

        public override List<Metric> ReadJson(JsonReader reader, Type objectType, List<Metric> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return null; //return base.ReadJson(reader, objectType, existingValue, hasExistingValue, serializer);
        }
    }
}
