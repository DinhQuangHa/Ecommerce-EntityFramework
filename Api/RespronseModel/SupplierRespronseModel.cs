using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.RespronseModel
{
    public class SupplierRespronseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CodeName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}