using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewRelicPlugin.RestApi.JsonConverter;
using NewRelicPlugin.RestApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NewRelicPlugin.RestApi.Tests
{
    [TestClass]
    public class MetricListJsonConverterTests
    {
        [TestMethod]
        public void VerifyJsonTest()
        {
            var expectedJson = "{\"Component/servers/online\":{\"count\":1,\"total\":21.0},\"Component/servers/offline\":{\"count\":1,\"total\":1.0}}";


            var metricList = new List<Metric>
            {
                new Metric
                {
                    Category = "servers",
                    Label = "online",
                    Value = new MetricValue
                    {
                        Total = 21,
                        Count = 1
                    }
                },
                new Metric
                {
                    Category = "servers",
                    Label = "offline",
                    Value = new MetricValue
                    {
                        Total = 1,
                        Count = 1
                    }
                }
            };

            var actualJson = JsonConvert.SerializeObject(metricList, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<Newtonsoft.Json.JsonConverter>
                {
                    new MetricListJsonConverter()
                }
            });

            Assert.AreEqual(expectedJson, actualJson);
        }
    }
}
