using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Runtime.Remoting.Contexts;
using System.Web.Http;
using Api.Dto;
using Api.Helper;
using Api.RespronseModel;
using EcommerDatabase;
using EcommerDatabase.Entities;
using EcommerDatabase.Enums;

namespace WepApi.Controllers
{
    [RoutePrefix("api/category")]
    public class CategoryNewController : ApiController
    {
        private readonly EcommerceDbContext _context = new EcommerceDbContext();

        #region Method
        private Category GetCategoryById(Guid Id)
        {
            if (Id == null)
                return null;
            else
            {
                var category = _context.Categories.FirstOrDefault(c => c.Id == Id);
                return category;
            }
        }

        private Category GetCategoryByDelete(DeleteIdDto dto)
        {
            if (dto.Id == Guid.Empty)
                return null;

            var category = GetCategoryById(dto.Id);
            return category;
        }
        #endregion

        #region Api

        #region Tu Lam Them



        // Xoa Mem Category
        [HttpPost]
        [Route("Xoa-Mem-Category")]
        public HttpResponseMessage XoaMemCategory(List<DeleteIdDto> dtos)
        {
            if (dtos != null)
            {
                foreach (var item in dtos)
                {
                    var category = GetCategoryByDelete(item);
                    if (category != null)
                    {
                        category.Status = Status.Deleted;
                    }
                }

                if (dtos.Count > 0)
                {
                    _context.SaveChanges();
                    return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                    {
                        StatusCode = HttpStatusCode.OK,
                        ErrorMessage = "",
                        ResponseMessage = "Delete category success"
                    });
                }
                return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                {
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "",
                    ResponseMessage = "Can't find category with list Id"
                });
            }

            return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
            {
                StatusCode = HttpStatusCode.OK,
                ErrorMessage = "Parameter null",
                ResponseMessage = ""
            });
        }

        //Them Du Lieu Category bang Linq
        [HttpPost]
        [Route("Add-Id-Category")]
        public HttpResponseMessage GetTakeSkipCategory(Category category)
        {
            if(category == null)
            {
                return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessage = "Parameter null",
                    ResponseMessage = ""
                });
            }
            var categories = new Category()
            {
                Name = category.Name,
                Description = category.Description,
                ParentId = category.ParentId,
                Sort = category.Sort,
                IsDisplayHomePage = category.IsDisplayHomePage,
                Status = category.Status
            };
            _context.Categories.Add(categories);
            _context.SaveChanges();
            return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
            {
                StatusCode = HttpStatusCode.OK,
                ErrorMessage = "",
                ResponseMessage = "Add Category success"
            });
        }

        //Lay Du Lieu Dung Take Skip
        [HttpGet]
        [Route("Get-TakeSkip-Category")]
        public List<CategoryRespronseModel> GetTakeSkipCategory()
        {
            var categories = (from c in _context.Categories
                             orderby c.Name ascending  // phải có orderby mới dùng được hàm Take And Skip
                             select new CategoryRespronseModel
                             {
                                 Sort = c.Sort,
                                 Id = c.Id,
                                 Name = c.Name,
                                 Description = c.Description,
                                 IsDisplayHomePage = c.IsDisplayHomePage,
                                 ParentId = c.ParentId
                             }).Take(15).Skip(3).ToList(); 
            return categories;
        }

        //Lay Du Lieu Sap Xep
        [HttpGet]
        [Route("Get-Category-Orderby-Sort")]
        public List<CategoryRespronseModel> GetCategoryOrderbySort()
        {
            var productOrderBySort = (from c in _context.Categories
                                      orderby c.Sort ascending
                                      select new CategoryRespronseModel
                                      {
                                          Sort = c.Sort,
                                          Id = c.Id,
                                          Name = c.Name,
                                          Description = c.Description,
                                          IsDisplayHomePage = c.IsDisplayHomePage,
                                          ParentId = c.ParentId
                                      }).ToList();
            return productOrderBySort;
        }

        //Lay Du Lieu Actvie
        [HttpGet]
        [Route("Get-Active-Product")]
        public List<ProductRespronseModel> GetActiveProduct(Guid Id)
        {
            var ActiveProduct = (from p in _context.Products
                                 join c in _context.Categories
                                 on p.CategoryId equals c.Id
                                 where p.CategoryId == Id && p.Status == Status.Active
                                 orderby p.Name ascending
                                 select new ProductRespronseModel
                                 {
                                     Id = p.Id,
                                     Name = p.Name,
                                     UrlName = p.UrlName,
                                     Sku = p.Sku,
                                     PublicationDate = p.PublicationDate,
                                     Price = p.Price,
                                     View = p.View,
                                     Description = p.Description,
                                     ShortDescription = p.ShortDescription
                                 }).ToList();
            return ActiveProduct;
        }


        #endregion

        [HttpGet]
        [Route("get-category-by-id/{id}")]
        public Category GetCategoryByIdApi(Guid Id)
        {
            if (Id != Guid.Empty)
            {
                return GetCategoryById(Id);
            }
            return null;
        }

        [HttpGet]
        [Route("get-categories")]
        public List<CategoryRespronseModel> GetCategorie()
        {
            var deleteStatus = new int[] { 0, 1 };
            var categories = _context.Categories.Where(c => deleteStatus.Contains((int)c.Status)).OrderBy(c => c.Sort).Select(s => new CategoryRespronseModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                IsDisplayHomePage = s.IsDisplayHomePage,
                ParentId = s.ParentId,
                Sort = s.Sort
            }).ToList();
            //select * from Category where Status in (0,1)
            return categories;
        }


        [HttpGet]
        [Route("get-product by-categoryId")]
        public List<ProductRespronseModel> GetProductByCategoryId(Guid id)
        {
            var product = (from p in _context.Products
                          join c in _context.Categories
                          on p.CategoryId equals c.Id
                          where p.CategoryId == id
                          orderby p.PublicationDate descending
                          select new ProductRespronseModel()
                          {
                              Id = p.Id,
                              Name = p.Name,
                              UrlName = p.UrlName,
                              Sku = p.Sku,
                              PublicationDate = p.PublicationDate,
                              Price = p.Price,
                              View = p.View,
                              Description = p.Description,
                              ShortDescription = p.ShortDescription
                          }).ToList();
            return product;
        }

        [HttpPost]
        [Route("delete-list-category")]
        public HttpResponseMessage DeleteCategories(List<DeleteIdDto> dtos)
        {
            if (dtos != null)
            {
                var categories = new List<Category>();
                foreach (var item in dtos)
                {
                    var category = GetCategoryByDelete(item);
                    if (category != null)
                    {
                        categories.Add(category);
                    }
                }

                if (categories.Count > 0)
                {
                    _context.Categories.RemoveRange(categories);
                    _context.SaveChanges();

                    return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                    {
                        StatusCode = HttpStatusCode.OK,
                        ErrorMessage = "",
                        ResponseMessage = "Delete category success"
                    });
                }

                return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                {
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "",
                    ResponseMessage = "Can't find categry with list Id"
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
        [Route("delete-category")]
        public HttpResponseMessage DeleteCategory(DeleteIdDto dto)
        {
            var category = GetCategoryByDelete(dto);
            // Xoa Cung
            if (category == null)
            {
                return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                {
                    StatusCode = HttpStatusCode.OK,
                    ErrorMessage = "",
                    ResponseMessage = "Can't not find category with list Id " + dto.Id
                });
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
            {
                StatusCode = HttpStatusCode.OK,
                ErrorMessage = "",
                ResponseMessage = "Delete category Width Id " + dto.Id + " success"
            });
        }

        [HttpPost]
        [Route("add-new-category")]
        public HttpResponseMessage AddNewOrEditCategory(CategoryDto dto)
        {
            if (dto == null) // luôn luôn phải có
            {
                return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ErrorMessage = "Parameter null",
                    ResponseMessage = ""
                });
            }

            if (dto.Id != Guid.Empty)
            {
                var category = GetCategoryById(dto.Id);

                if (category != null)
                {
                    //2 Update value from dto
                    category.Name = dto.Name;
                    category.Description = dto.Description;
                    category.IsDisplayHomePage = dto.IsDisplayHomePage;
                    category.ParentId = dto.ParentId;
                    category.Status = dto.Status;
                    category.Sort = dto.Sort;
                    category.UpdatedDate = DateTime.Now;
                }
                else
                {
                    return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
                    {
                        StatusCode = HttpStatusCode.OK,
                        ErrorMessage = "",
                        ResponseMessage = "Can't Find category Width Id" + dto.Id
                    });
                }
            }
            else
            {
                var category = new Category()
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    IsDisplayHomePage = dto.IsDisplayHomePage,
                    Status = dto.Status,
                    ParentId = dto.ParentId

                };
                _context.Categories.Add(category);
            }

            _context.SaveChanges();

            return RespronMessageHelper.ResponseMessage(Request, new ResponseDto
            {
                StatusCode = HttpStatusCode.OK,
                ErrorMessage = "",
                ResponseMessage = "Add Category success"
            });
        }
        #endregion
    }
}