using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Newtonsoft.Json;

namespace Infrastructure.Helper
{
    public class ApiClient : IApiClient
    {
        private readonly IConfiguration _config;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private string _apiKey;
        private string _baseUrl;

        public ApiClient(IConfiguration config)
        {
            _config = config;
            _apiKey = _config["OpenWeatherMapConfig:ApiKey"];
            _baseUrl = _config["OpenWeatherMapConfig:ApiEndpoint"];
        }

    public async Task<WeatherMapResponseModel> GetBasicAsync(string url)
    {
        url = string.Format("{0}{1}&appid={2}", _baseUrl, url, _apiKey);
        using (var client = new HttpClient())
        using (var request = new HttpRequestMessage(HttpMethod.Get, url))
        {
            try
            {
                HttpResponseMessage response = client.Send(request);

                var resultString = await response.Content.ReadAsStringAsync();
                    
                return JsonConvert.DeserializeObject<WeatherMapResponseModel>(resultString);
            }
            catch (Exception e)
            {
                _logger.Error(e, e.Message);
            }

                return new WeatherMapResponseModel();
        }
    }
}
}