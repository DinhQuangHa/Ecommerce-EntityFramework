using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerDatabase.Entities
{
    public class ProductPrice : BaseEntities
    {
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public Decimal Value { get; set; }
        public Guid ProductId { get; set; }
    }
}
