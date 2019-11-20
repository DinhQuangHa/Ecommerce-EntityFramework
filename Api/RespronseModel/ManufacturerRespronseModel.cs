using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.RespronseModel
{
    public class ManufacturerRespronseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CodeName { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Logo { get; set; }
        public Guid? SiteId { get; set; }
        
    }
}