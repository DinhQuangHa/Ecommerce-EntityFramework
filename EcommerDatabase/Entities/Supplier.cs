using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerDatabase.Entities
{
    public class Supplier : BaseEntities
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [StringLength(20)]
        public string CodeName { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }

        #region Related
        public ICollection<Product> Products { get; set; }
        #endregion
    }
}
