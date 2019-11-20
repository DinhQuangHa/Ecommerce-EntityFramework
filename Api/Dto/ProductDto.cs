using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlName { get; set; }
        public string Sku { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
        public int View { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SupplierId { get; set; }
        public Guid ManufactureId { get; set; }
    }
}