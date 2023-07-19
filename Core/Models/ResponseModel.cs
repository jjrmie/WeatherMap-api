using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class ResponseModel
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public string Data { get; set; }
        public bool status { get; set; }

        public static implicit operator Task<object>(ResponseModel v)
        {
            throw new NotImplementedException();
        }
    }
}
