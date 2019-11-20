using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.RespronseModel
{
    public class CategoryRespronseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDisplayHomePage { get; set; }
        public Guid? ParentId { get; set; }
        public int Sort { get; set; }
    }
}