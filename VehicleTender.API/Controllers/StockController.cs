using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VehicleTender.API.Controllers
{
    [Authorize(Roles = "admin")]
    public class StockController : ApiController
    {
        private MainBLL mainBLL = new MainBLL();
        public IHttpActionResult Get()
        {
            var users = mainBLL.GetUsersList();
            return Ok(users);
        }
    }
}
