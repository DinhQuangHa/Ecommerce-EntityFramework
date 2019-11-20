using Api.Dto;
using Api.Helper;
using Api.RespronseModel;
using EcommerDatabase;
using EcommerDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace WepApi.Controllers
{
    [RoutePrefix("api/category")]
    public class SupplierController : ApiController
    {
        private readonly EcommerceDbContext _context = new EcommerceDbContext();

        #region Method
        private Supplier GetSupplierById(Guid Id)
        {
            if (Id == null)
                return null;
            else
            {
                var supplier = _context.Suppliers.FirstOrDefault(c => c.Id == Id);
                return supplier;
            }
        }

        private Supplier GetSupplierByDelete(DeleteIdDto dto)
        {
            if (dto.Id == Guid.Empty)
                return null;

            var supplier = GetSupplierById(dto.Id);
            return supplier;
        }
        #endregion

        #region Api
        [HttpGet]
        [Route("get-supplier-by-id/{id}")]
        public Supplier GetsupplierByIdApi(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                return GetSupplierById(Id);
            }
            return null;
        }

        [HttpGet]
        [Route("get-suppliers")]
        public List<SupplierRespronseModel> GetSuppliers()
        {
            var deleteStatus = new int[] { 0, 1 };
            var suppliers = _context.Suppliers.Where(c => deleteStatus.Contains((int)c.Status)).Select(s => new SupplierRespronseModel
            {
                Id = s.Id,
                Name = s.Name,
                CodeName = s.CodeName,
                Phone = s.Phone,
                Email = s.Email
            }).ToList();
            //select * from Category where Status in (0,1)
            return suppliers;
        }

        [HttpPost]
        [Route("delete-list-Supplier")]
        public HttpResponseMessage DeleteSupplier(List<DeleteIdDto> dtos)
        {
            if (dtos != null)
            {
                var Suppliers = new List<Supplier>();
                foreach (var item in dtos)
                {
                    var supplier = GetSupplierByDelete(item);
                    if (supplier != null)
                    {
                        Suppliers.Add(supplier);
                    }
                }

                if (Suppliers.Count > 0)
                {
                    _context.Suppliers.RemoveRange(Suppliers);
                    _context.SaveChanges();

                    return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                    {
                        StatusCode = HttpStatusCode.OK,
                        ErrorMessage = "",
                        ResponseMessage = "Delete Supplier success"
                    });
                }

                return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                {
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "",
                    ResponseMessage = "Can't find Supplier with list Id"
                });
            }

            return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
            {
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessage = "",
                ResponseMessage = "Parammeter is not invalid"
            });
        }


        [HttpPost]
        [Route("delete-Supplier")]
        public HttpResponseMessage DeleteSupplier(DeleteIdDto dto)
        {
            var supplier = GetSupplierByDelete(dto);
            // Xoa Cung
            if (supplier == null)
            {
                return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                {
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "",
                    ResponseMessage = "Can't not find Supplier with list Id " + dto.Id
                });
            }
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();

            return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
            {
                StatusCode = HttpStatusCode.OK,
                ErrorMessage = "",
                ResponseMessage = "Delete Supplier Width Id " + dto.Id + " success"
            });
        }

        [HttpPost]
        [Route("add-new-Supplier")]
        public HttpResponseMessage AddNewSupplier(SupplierDto dto)
        {
            if (dto == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new ResponseDto
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessage = "Parameter null",
                    ResponseMessage = ""
                });
            }

            if (dto.Id != Guid.Empty)
            {
                var supplier = GetSupplierById(dto.Id);

                if (supplier != null)
                {
                    supplier.Name = dto.Name;
                    supplier.CodeName = dto.CodeName;
                    supplier.Phone = dto.Phone;
                    supplier.Email = dto.Email;
                    supplier.UpdatedDate = DateTime.Now;
                }
                else
                {
                    return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                    {
                        StatusCode = HttpStatusCode.OK,
                        ErrorMessage = "",
                        ResponseMessage = "Can't Find Supllier Width Id" + dto.Id
                    });
                }
            }
            else
            {
                var supllier = new Supplier()
                {
                    Name = dto.Name,
                    CodeName = dto.CodeName,
                    Phone = dto.Phone,
                    Email = dto.Email
                };
                _context.Suppliers.Add(supllier);
            }

            _context.SaveChanges();

            return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
            {
                StatusCode = HttpStatusCode.OK,
                ErrorMessage = "",
                ResponseMessage = "Add Supplier success"
            });
            #endregion
        }
    }
}
