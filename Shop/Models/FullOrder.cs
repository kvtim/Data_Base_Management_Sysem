using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class FullOrder
    {
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public string OrderStatus { get; set; }
        public int OrderPrice { get; set; }
        public DateTime OrderTime { get; set; }
        public string Address { get; set; }

        public List<ProductForOrder> Products { get; set; } = new List<ProductForOrder>();
    }
}
