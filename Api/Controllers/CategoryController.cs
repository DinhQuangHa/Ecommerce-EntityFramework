using Api.Dto;
using EcommerDatabase.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace WepApi.Controllers
{
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiController
    {
        [HttpPost]
        [Route("Update-category")]
        public IHttpActionResult UpdateCategory(int Id, string Name)
        {
            //Save category to db
            return Ok("Update Successfully");
        }

        [HttpPost]
        //Cach 1 routing
        //[Route("api/Category/save-Category")]
        [Route("Save-category")]
        public HttpResponseMessage SaveCategory(CategoryDto dto)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new CategoryDto()
            {
                Name = "Dinh Quang Ha",
                Sort = 1,
                IsDisplayHomePage = true,
                Status = Status.Active,
                ParentId = null
            }, new JsonMediaTypeFormatter(), "application/json");
            //return object
        }

        // GET: api/Category
        [HttpGet]
        //api/Category/Get-Resource?Id=10&Message=Dinh Quang Ha = query string
        [Route("Get-Resource")]
        //api/Category/GetResource?Id=10&Message=Dinh Quang Ha = query string
        public IHttpActionResult GetResource(int Id, string Message)
        {
            if (Id % 2 == 0)
            {
                return Ok(Message + " success");
            }
            return Ok(Message + " False");
        }

        [HttpGet]
        [Route("Test-Api/{Id:int:min(1):max(100)}/{Message}")]
        //api/category/Test-Api/10/Dinh Quang Ha
        public IHttpActionResult TestApi(int Id, string Message)
        {

            return Ok("Dinh Quang Ha");
        }

        // PHAN TU TAO
        //public IHttpActionResult GetString()
        //{
        //    //return new string[] { "Điện Thoại", "Máy Tính Bảng" };
        //    var Result = new string[] { "Điện Thoại", "Máy Tính Bảng" };
        //    return Ok(Result);
        //}

        // GET: api/Category/5
        //public IHttpActionResult Get(string userName, string pass)
        //{
        //    return Ok(userName + "Pass: " + pass );
        //}

        // POST: api/Category
        //public IHttpActionResult InsertUserProfile([FromBody]UserProFileDto user)
        //{
        //    return Ok("Insert success " + user.Name);
        //}

        // PUT: api/Category/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Category/5
        //public void Delete(int id)
        //{
        //}
    }

    public class UserProFileDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
    }
}