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
using System.Web.Http;

namespace WepApi.Controllers
{
    [RoutePrefix("api/category")]
    public class ProductController : ApiController
    {
        private readonly EcommerceDbContext _context = new EcommerceDbContext();
        #region Method
        private Product GetProductById(Guid Id)
        {
            if (Id == null)
                return null;
            else
            {
                var product = _context.Products.FirstOrDefault(c => c.Id == Id);
                return product;
            }
        }

        private Product GetProductByDelete(DeleteIdDto dto)
        {
            if (dto.Id == Guid.Empty)
                return null;

            var product = GetProductById(dto.Id);
            return product;
        }
        #endregion
        #region Api

        #region Tu Lam Them

        ////Lay Du Lieu View Lon
        [HttpGet]
        [Route("Get-BigView")]
        public List<ProductRespronseModel> GetBigView()
        {
            var bigView = (from p in _context.Products
                           where p.View > 50 && p.Price > 500
                           orderby p.Name ascending
                           select new ProductRespronseModel
                           {
                               Id = p.Id,
                               Name = p.Name,
                               UrlName = p.UrlName,
                               Sku = p.Sku,
                               Price = p.Price,
                               View = p.View
                           }).ToList();
            return bigView;
        }

        //Lay Du Lieu Ngay
        [HttpGet]
        [Route("Get-Date")]
        public List<ProductRespronseModel> GetDate()
        {
            var day = DateTime.Parse("09-19-2000");
            var productDate = (from p in _context.Products
                               where p.UpdatedDate > day
                               orderby p.Name ascending
                               select new ProductRespronseModel
                               {
                                   Id = p.Id,
                                   Name = p.Name,
                                   UrlName = p.UrlName,
                                   Sku = p.Sku,
                                   Price = p.Price,
                                   View = p.View
                               }).ToList();
            return productDate;
        }

        //Lay Du Lieu Value > 500000
        [HttpGet]
        [Route("Get-Product-ProfuctPrice")]
        public List<ProductPriceRespronModel> GetProductProductPrice(Guid Id)
        {
            var Price = (from pp in _context.ProductPrices
                         join p in _context.Products
                         on pp.ProductId equals p.Id
                         where pp.ProductId == Id || pp.Value > 500000
                         orderby pp.Value descending
                         select new ProductPriceRespronModel
                         {
                             Id = pp.Id,
                             MaxQuantity = pp.MaxQuantity,
                             MinQuantity = pp.MinQuantity,
                             Value = pp.Value,
                             ProductId = pp.ProductId
                         }).ToList();
            return Price;
        }

        [HttpGet]
        [Route("Get-ProductPrice")]
        public List<ProductPrice> GetListProductPrice()
        {
            var productPrice = (from p in _context.ProductPrices
                                orderby p.Value
                                select p).ToList();
            return productPrice;
        }

        #endregion

        [HttpGet]
        [Route("get-product-by-id/{id}")]
        public Product GeProductByIdApi(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                return GetProductById(Id);
            }
            return null;
        }

        [HttpGet]
        [Route("get-product")]
        public List<ProductRespronseModel> GetProduct()
        {
            var deleteStatus = new int[] { 0, 1 };
            var product = _context.Products.Where(c => deleteStatus.Contains((int)c.Status)).Select(s => new ProductRespronseModel
            {
                Id = s.Id,
                Name = s.Name,
                UrlName = s.UrlName,
                Sku = s.UrlName,
                PublicationDate = s.PublicationDate,
                Price = s.Price,
                View = s.View,
                Description = s.Description,
                ShortDescription = s.ShortDescription

            }).ToList();
            //select * from Category where Status in (0,1)
            return product;
        }

        [HttpPost]
        [Route("delete-list-product")]
        public HttpResponseMessage DeleteProduct(List<DeleteIdDto> dtos)
        {
            if (dtos != null)
            {
                var products = new List<Product>();
                foreach (var item in dtos)
                {
                    var product = GetProductByDelete(item);
                    if (product != null)
                    {
                        products.Add(product);
                    }
                }

                if (products.Count > 0)
                {
                    _context.Products.RemoveRange(products);
                    _context.SaveChanges();

                    return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                    {
                        StatusCode = HttpStatusCode.OK,
                        ErrorMessage = "",
                        ResponseMessage = "Delete product success"
                    });
                }

                return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                {
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "",
                    ResponseMessage = "Can't find product with list Id"
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
        [Route("delete-product")]
        public HttpResponseMessage DeleteProduct(DeleteIdDto dto)
        {
            var product = GetProductByDelete(dto);
            // Xoa Cung
            if (product == null)
            {
                return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                {
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "",
                    ResponseMessage = "Can't not find product with list Id " + dto.Id
                });
            }
            _context.Products.Remove(product);
            _context.SaveChanges();

            return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
            {
                StatusCode = HttpStatusCode.OK,
                ErrorMessage = "",
                ResponseMessage = "Delete product Width Id " + dto.Id + " success"
            });
        }

        [HttpPost]
        [Route("add-new-product")]
        public HttpResponseMessage AddNewProduct(ProductDto dto)
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
                var product = GetProductById(dto.Id);

                if (product != null)
                {
                    product.UpdatedDate = DateTime.Now;
                    product.Name = dto.Name;
                    product.UrlName = dto.UrlName;
                    product.Sku = dto.UrlName;
                    product.PublicationDate = dto.PublicationDate;
                    product.Price = dto.Price;
                    product.View = dto.View;
                    product.Description = dto.Description;
                    product.ShortDescription = dto.ShortDescription;
                    product.CategoryId = dto.CategoryId;
                    product.SupplierId = dto.SupplierId;
                    product.ManufactureId = dto.ManufactureId;

                }
                else
                {
                    return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                    {
                        StatusCode = HttpStatusCode.OK,
                        ErrorMessage = "",
                        ResponseMessage = "Can't Find product Width Id" + dto.Id
                    });
                }
            }
            else
            {
                var product = new Product()
                {
                    Name = dto.Name,
                    UrlName = dto.UrlName,
                    Sku = dto.Sku,
                    PublicationDate = dto.PublicationDate,
                    Price = dto.Price,
                    View = dto.View,
                    Description = dto.Description,
                    ShortDescription = dto.ShortDescription,
                    CategoryId = dto.CategoryId,
                    SupplierId = dto.SupplierId,
                    ManufactureId = dto.ManufactureId
                };
                _context.Products.Add(product);
            }

            _context.SaveChanges();

            return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
            {
                StatusCode = HttpStatusCode.OK,
                ErrorMessage = "",
                ResponseMessage = "Add product success"
            });
        }
        #endregion
    }
}
