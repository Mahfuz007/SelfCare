using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class QueryRequestBase
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 100;
    }
}
