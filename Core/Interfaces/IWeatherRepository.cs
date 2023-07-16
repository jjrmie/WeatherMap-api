using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{

    public interface IWeatherRepository
    {
        //List<DocumentView> GetById(Guid supplierId, string fromDate, string toDate);
        Task<WeatherMapResponseModel> GetWeatherByCountryCity(string country, string city);
    }
}
