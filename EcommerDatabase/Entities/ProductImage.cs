using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerDatabase.Entities
{
    public class ProductImage : BaseEntities
    {
        public string ImageLink { get; set; }
        public string SequenceNo { get; set; }
        public Guid ProductId { get; set; }
    }
}
