using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewRelicPlugin.RestApi.Models;

namespace NewRelicPlugin.RestApi.Tests
{
    [TestClass]
    public class NewRelicClientTests
    {
        [TestMethod]
        public async Task PostMetricTest()
        {
            var metricSubmission = new MetricSubmission
            {
                Agent = new Agent
                {
                    Host = "adamdesktop.mytrusense.com",
                    Pid = 1234,
                    Version = "0.0.1"
                },
                Components = new List<Component>
                {
                    new Component
                    {
                        Duration = 60,
                        Guid = "com.mytrusense.JiliaMonitor",
                        Name = "Jilia",
                        Metrics = new List<Metric>
                        {
                            new Metric
                            {
                                Value = new MetricValue
                                {
                                    Total = 22,
                                    Count = 1
                                },
                                Category = "servers",
                                Label = "online",
                                Units = "Count"
                            }
                        }
                    }
                }
            };

            var postMetricResponse = await GetNewRelicClient.PostMetric(metricSubmission);

            Assert.IsNotNull(postMetricResponse);
            Assert.IsTrue(postMetricResponse.IsSuccessStatusCode);
        }

        private NewRelicClient GetNewRelicClient => new NewRelicClient("abc123");
    }
}