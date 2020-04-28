using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bangazon.Models.ProductTypeViewModels
{
    public class ProductList
    {
        public int TypeId { get; set; }

        public string Name { get; set; }

        public int ProductCount { get; set; }

        public IEnumerable<Product> Products { get; set; }

    }
}
