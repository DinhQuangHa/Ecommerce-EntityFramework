using EcommerDatabase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Dto
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sort { get; set; }
        public bool IsDisplayHomePage { get; set; }
        public Guid? ParentId { get; set; }
        public Status Status { get; set; }
    }
}