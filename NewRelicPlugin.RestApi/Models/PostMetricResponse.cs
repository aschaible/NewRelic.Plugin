using System.Net;

namespace NewRelicPlugin.RestApi.Models
{
    public class PostMetricResponse
    {
        public bool IsSuccessStatusCode { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
