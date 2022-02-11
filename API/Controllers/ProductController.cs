using Model.ProductData;
using Model.ViewModels.Common;
using Model.ViewModels.Product;
using System.Collections.Generic;
using System.Linq;
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
            return Ok(new ProductData().Save(model).Success);
        }

        [HttpGet]
        [Route("Delete")]
        public IHttpActionResult Delete(int id)
        {
            return Ok(new ProductData().Delete(id));
        }


    }
}
