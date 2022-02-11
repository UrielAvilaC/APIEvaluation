using Model.ProductData;
using Model.ViewModels.Auth;
using Model.ViewModels.Common;
using Model.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Product")]
    public class ProductController : ApiController
    {
        [HttpPost]
        [Route("Select")]
        public IHttpActionResult Select(ProductViewModel model)
        {
            return Ok(new ProductData().Select(model));
        }
        [HttpPost]
        [Route("Save")]
        public IHttpActionResult Save(ProductViewModel model)
        {
            if(HttpContext.Current.Cache["UserId"] != null)
            {
                var userId = HttpContext.Current.Cache["UserId"];
                model.RegistrationPerson = new PersonViewModel
                {
                    UserId = userId?.ToString()
                };
                return Ok(new ProductData().Save(model).Success);
            }
            return Ok("Reload");
            
        }

        [HttpGet]
        [Route("Delete")]
        public IHttpActionResult Delete(int id)
        {
            if (HttpContext.Current.Cache["UserId"] != null)
            {
                var userId = HttpContext.Current.Cache["UserId"];
                return Ok(new ProductData().Delete(id,Convert.ToInt32(userId)));
            }
            return Ok("Reload");
        }
        [HttpGet]
        [Route("Recover")]
        public IHttpActionResult Recover(int id)
        {
            if (HttpContext.Current.Cache["UserId"] != null)
            {
                var userId = HttpContext.Current.Cache["UserId"];
                return Ok(new ProductData().Recover(id, Convert.ToInt32(userId)));
            }
            return Ok("Reload");
        }
        [HttpGet]
        [Route("ViewRecord")]
        public IHttpActionResult ViewRecord(int id)
        {
            return Ok(new ProductData().ViewRecord(id));
        }
        [HttpGet]
        [Route("Log")]
        public IHttpActionResult Log(int id)
        {
            return Ok(new ProductData().ViewLog(id));
        }

    }
}
