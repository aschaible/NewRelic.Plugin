namespace NewRelicPlugin.RestApi.Models
{
    public class Metric
    {
        public string Category { get; set; }

        public string Label { get; set; }

        public string Units { get; set; }

        public MetricValue Value { get; set; }

        public string MetricName
        {
            get
            {
                var path = $"Component/{Category}";

                if (!string.IsNullOrWhiteSpace(Label))
                {
                    path += $"/{Label}";
                }

                if (!string.IsNullOrWhiteSpace(Units))
                {
                    path += $"[{Units}]";
                }

                return path;
            }
        }
    }
}
