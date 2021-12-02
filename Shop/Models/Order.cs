using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public int OrderPrice { get; set; }
        public DateTime CreatedTime { get; set; }
        enum Status
        {
            Formalizer,
            Sent,
            Delivered,
        }

        public int UserId { get; set; }
    }
}
