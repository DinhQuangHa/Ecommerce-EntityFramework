 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.RespronseModel
{
    public class ProductRespronseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlName { get; set; }
        public string Sku { get; set; }
        public DateTime? PublicationDate { get; set; }
        public Decimal Price { get; set; }
        public int View { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
    }
}