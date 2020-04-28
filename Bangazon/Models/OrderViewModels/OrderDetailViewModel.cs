using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Bangazon.Models.OrderViewModels
{
    public class OrderDetailViewModel
    {
        public Order Order { get; set; }
      
        public IEnumerable<OrderLineItem> LineItems { get; set; }
    }
}