using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerDatabase.Entities
{
    public class Product : BaseEntities
    {
        #region Atrribute
        public string Name { get; set; }
        public string UrlName { get; set; }
        public string Sku { get; set; }
        public DateTime PublicationDate { get; set; }
        public decimal Price { get; set; }
        public int View { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }

        #endregion

        #region Related
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public Guid CategoryId { get; set; }

        [ForeignKey("SupplierId")]
        public Supplier Supplier { get; set; }

        public Guid SupplierId { get; set; }

        [ForeignKey("ManufactureId")]
        public Manufacturer Manufacturer { get; set; }

        public Guid ManufactureId { get; set; }

        #endregion
    }
}
