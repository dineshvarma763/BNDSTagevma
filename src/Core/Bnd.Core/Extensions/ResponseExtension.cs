using Bnd.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.Core.Extensions
{
    public static class ResponseExtension
    {
        public static HttpResponse<T> FormatResponse<T>(T data, string errorMessage = "")
        {
            var result = new HttpResponse<T>()
            {
                Success = false,
                ErrorMessage = errorMessage,
                Result = default(T)
            };

            if (data is not null)
            {
                result = new HttpResponse<T>()
                {
                    Result = data,
                    Success = true
                };
            }

            return result;
        }
    }
}
