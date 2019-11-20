using Api.Dto;
using Api.Helper;
using Api.RespronseModel;
using EcommerDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using EcommerDatabase.Entities;

namespace WepApi.Controllers
{
    [RoutePrefix("api/category")]
    public class ManufacturerController : ApiController
    {
        private readonly EcommerceDbContext _context = new EcommerceDbContext();

        #region Method
        private Manufacturer GetManufacturerById(Guid Id)
        {
            if (Id == null)
                return null;
            else
            {
                var manufacturer = _context.Manufacturers.FirstOrDefault(c => c.Id == Id);
                return manufacturer;
            }
        }

        private Manufacturer GetManufacturerByDelete(DeleteIdDto dto)
        {
            if (dto.Id == Guid.Empty)
                return null;

            var manufacturer = GetManufacturerById(dto.Id);
            return manufacturer;
        }
        #endregion
        #region Api

        [HttpGet]
        [Route("get-Manufacture-by-id/{id}")]
        public Manufacturer GetManufacturerByIdApi(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                return GetManufacturerById(Id);
            }
            return null;
        }

        [HttpGet]
        [Route("get-Manufacturer")]
        public List<ManufacturerRespronseModel> GetManufacturers()
        {
            var deleteStatus = new int[] { 0, 1 };
            var suppliers = _context.Manufacturers.Where(c => deleteStatus.Contains((int)c.Status)).Select(s => new ManufacturerRespronseModel
            {
                Id = s.Id,
                Name = s.Name,
                CodeName = s.CodeName,
                Description = s.Description,
                Website = s.Website,
                Logo = s.Logo,
                SiteId = s.SiteId
            }).ToList();
            //select * from Category where Status in (0,1)
            return suppliers;
        }

        [HttpPost]
        [Route("delete-list-Manufacturer")]
        public HttpResponseMessage DeleteManufacturer(List<DeleteIdDto> dtos)
        {
            if (dtos != null)
            {
                var manufacturers = new List<Manufacturer>();
                foreach (var item in dtos)
                {
                    var manufacturer = GetManufacturerByDelete(item);
                    if (manufacturer != null)
                    {
                        manufacturers.Add(manufacturer);
                    }
                }

                if (manufacturers.Count > 0)
                {
                    _context.Manufacturers.RemoveRange(manufacturers);
                    _context.SaveChanges();

                    return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                    {
                        StatusCode = HttpStatusCode.OK,
                        ErrorMessage = "",
                        ResponseMessage = "Delete Manufacture success"
                    });
                }

                return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                {
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "",
                    ResponseMessage = "Can't find Manufacture with list Id"
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
        [Route("delete-manufacture")]
        public HttpResponseMessage DeleteManufacture(DeleteIdDto dto)
        {
            var manufacturer = GetManufacturerByDelete(dto);
            // Xoa Cung
            if (manufacturer == null)
            {
                return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                {
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "",
                    ResponseMessage = "Can't not find Manufacture with list Id " + dto.Id
                });
            }
            _context.Manufacturers.Remove(manufacturer);
            _context.SaveChanges();

            return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
            {
                StatusCode = HttpStatusCode.OK,
                ErrorMessage = "",
                ResponseMessage = "Delete Manufacture Width Id " + dto.Id + " success"
            });
        }

        [HttpPost]
        [Route("add-new-Mannufacture")]
        public HttpResponseMessage AddNewManufacture(ManufacturerDto dto)
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
                var manufacturer = GetManufacturerById(dto.Id);

                if (manufacturer != null)
                {
                    manufacturer.Name = dto.Name;
                    manufacturer.CodeName = dto.CodeName;
                    manufacturer.Description = dto.Description;
                    manufacturer.Website = dto.Website;
                    manufacturer.Logo = dto.Logo;
                    manufacturer.SiteId = dto.SiteId;
                    manufacturer.UpdatedDate = DateTime.Now;
                }
                else
                {
                    return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                    {
                        StatusCode = HttpStatusCode.OK,
                        ErrorMessage = "",
                        ResponseMessage = "Can't Find Manufacture Width Id" + dto.Id
                    });
                }
            }
            else
            {
                var manufacture = new EcommerDatabase.Entities.Manufacturer()
                {
                    Name = dto.Name,
                    CodeName = dto.CodeName,
                    Description = dto.Description,
                    Website = dto.Website,
                    Logo = dto.Logo,
                    SiteId = dto.SiteId
                };
                _context.Manufacturers.Add(manufacture);
            }

            _context.SaveChanges();

            return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
            {
                StatusCode = HttpStatusCode.OK,
                ErrorMessage = "",
                ResponseMessage = "Add Manufacture success"
            });
            #endregion
        }
    }
}
