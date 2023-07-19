using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper
{
    public interface IApiClient
    {
        Task<WeatherMapResponseModel> GetBasicAsync(string url);
    }
}
