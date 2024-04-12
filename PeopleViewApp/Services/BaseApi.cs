using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PeopleViewApp.Settings;
using System.Net.Http;

namespace PeopleViewApp.Services
{
    public class BaseApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppSettings _settings;

        public BaseApi(IHttpClientFactory httpClientFactory,
            IOptions<AppSettings> settings)
        {
            _httpClientFactory = httpClientFactory;
            _settings = settings.Value;
        }

        protected async Task<T> HttpRequestRun<T>(HttpMethod method, string path, T entety = null) where T : class
        {
            HttpClient client = _httpClientFactory.CreateClient();
            client.Timeout = new TimeSpan(0, 1, 0);
            HttpRequestMessage message = new(method, $"{_settings.DomenUrl}{path}");
            message.Headers.Add("Accept", "application/json");

            if (entety is not null
                && (method == HttpMethod.Post || method == HttpMethod.Put))
            {
                var t = JsonConvert.SerializeObject(entety);
                message.Content = new StringContent(JsonConvert.SerializeObject(entety), System.Text.Encoding.UTF8, "application/json");
            }

            var response = await client.SendAsync(message);

            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStringAsync();

                entety = JsonConvert.DeserializeObject<T>(contentStream);
            }

            return entety;
        }
    }
}