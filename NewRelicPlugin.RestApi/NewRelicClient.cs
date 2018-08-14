using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NewRelicPlugin.RestApi.JsonConverter;
using NewRelicPlugin.RestApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NewRelicPlugin.RestApi
{
    public interface INewRelicClient
    {
        Task<PostMetricResponse> PostMetric(MetricSubmission metricSubmission);
    }

    public class NewRelicClient : INewRelicClient, IDisposable
    {
        private readonly string JsonMediaType = "application/json";
        private readonly string _licenseKey;
        private readonly HttpClient _httpClient;
        private readonly string _rootUrl;

        public NewRelicClient(string licenseKey, string rootUrl = null)
        {
            if (rootUrl == null)
            {
                rootUrl = "https://platform-api.newrelic.com/";
            }

            _licenseKey = licenseKey;

            _httpClient = new HttpClient { BaseAddress = new Uri(rootUrl) };
            _httpClient.DefaultRequestHeaders.Add("X-License-Key", _licenseKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMediaType));
        }

        public async Task<PostMetricResponse> PostMetric(MetricSubmission metricSubmission)
        {
            var returnVal = new PostMetricResponse();

            var json = JsonConvert.SerializeObject(metricSubmission, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new List<Newtonsoft.Json.JsonConverter>
                {
                    new MetricListJsonConverter()
                }
            });

            using (var content = new StringContent(json))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue(JsonMediaType);

                var result = await _httpClient.PostAsync("platform/v1/metrics", content);

                returnVal.IsSuccessStatusCode = result.IsSuccessStatusCode;
                returnVal.StatusCode = result.StatusCode;

                if (result.IsSuccessStatusCode)
                {

                }
            }

            return returnVal;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
