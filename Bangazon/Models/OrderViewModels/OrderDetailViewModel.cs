using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Bangazon.Models.OrderViewModels
{
    public class OrderDetailViewModel
    {
        //public OrderDetailViewModel(Order order)
        //{
        //    Order = order;
        //    LineItems = new List<OrderLineItem>();
        //}
        public Order Order { get; set; }
      
        public IEnumerable<OrderLineItem> LineItems { get; set; }
    }
}