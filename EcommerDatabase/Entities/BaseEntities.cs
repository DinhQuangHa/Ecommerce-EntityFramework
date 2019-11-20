using EcommerDatabase.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerDatabase.Entities
{
    public class BaseEntities
    {
        public BaseEntities()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.Now;
        }
        [Key]
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid? CreateBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Status Status { get; set; }
    }
}
