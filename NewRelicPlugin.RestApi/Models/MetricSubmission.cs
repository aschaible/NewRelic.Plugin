using System.Collections.Generic;

namespace NewRelicPlugin.RestApi.Models
{
    public class MetricSubmission
    {
        public Agent Agent { get; set; }

        public List<Component> Components { get; set; }
    }
}
