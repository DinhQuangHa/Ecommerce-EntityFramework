using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerDatabase.Entities
{
    public class ProductStatus : BaseEntities
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ProductId { get; set; }
    }
}
