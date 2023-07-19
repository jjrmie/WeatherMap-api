using Core.Interfaces;
using Core.Models;
using Infrastructure.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    
    public class WeatherRepository : IWeatherRepository
    {
        private readonly IApiClient _apiClient;
        public WeatherRepository(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<WeatherMapResponseModel> GetWeatherByCountryCity(string country, string city)
        {
            var parameter = string.Format("q={0},{1}", city, country);

            return await _apiClient.GetBasicAsync(parameter);
        }
    }


}
