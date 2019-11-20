using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerDatabase.Entities
{
    public class Category : BaseEntities
    {
        #region Atrribute
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public Guid? ParentId { get; set; }
        public int Sort { get; set; }
        public bool IsDisplayHomePage { get; set; }
        #endregion

        #region Related
        public ICollection<Product> Products { get; set; }
        #endregion
    }
}
