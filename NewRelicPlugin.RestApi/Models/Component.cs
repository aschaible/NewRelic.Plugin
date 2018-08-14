using System.Collections.Generic;

namespace NewRelicPlugin.RestApi.Models
{
    public class Component
    {
        public string Name { get; set; }

        public string Guid { get; set; }

        public int Duration { get; set; }

        public List<Metric> Metrics { get; set; }
    }
}
