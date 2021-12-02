using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class Delivery
    {
        public int DeliveryId { get; set; }
        public string DeliveryName { get; set; }
        public string DeliveryAddress { get; set; }

        public int OrderId { get; set; }
    }
}
