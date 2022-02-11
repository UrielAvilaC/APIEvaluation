using Model;
using Model.Auth;
using Model.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        [HttpPost]
        [Route("auth")]
        public IHttpActionResult Auth(UserViewModel ViewModel)
        {
            try
            {
                AuthData authData = new AuthData();
                var resultSet = authData.Auth(ViewModel);
                if (resultSet.Success)
                {
                 HttpContext.Current.Cache["UserId"] = resultSet.ObjectResult.UserId;

                }
                return Ok(resultSet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
