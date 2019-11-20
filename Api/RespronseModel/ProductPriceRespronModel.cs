using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.RespronseModel
{
    public class ProductPriceRespronModel
    {
        public Guid Id { get; set; }
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public Decimal Value { get; set; }
        public Guid ProductId { get; set; }
    }
}