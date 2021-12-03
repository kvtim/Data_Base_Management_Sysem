using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class FullProduct : Product
    {
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
    }
}
