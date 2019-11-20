using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerDatabase.Entities
{
    public class Manufacturer : BaseEntities
    {
        #region Atribute
        [MaxLength(200)]
        [Required]
        public string Name { get; set; }
        [MaxLength(20)]
        [Required]
        public string CodeName { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(100)]
        public string Website { get; set; }
        public string Logo { get; set; }
        public Guid? SiteId { get; set; }
        #endregion

        #region
        public ICollection<Product> Products { get; set; }
        #endregion
    }
}
