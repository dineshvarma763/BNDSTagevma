using System;
using System.Collections.Generic;
using System.Text;

namespace Bnd.Core.Models
{
    public class PageResponse<T>
    {
        public T? Data { get; set; }
    }
}
