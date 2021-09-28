using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace VehicleTender.API.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        private MainBLL mainBLL = new MainBLL();
        // GET api/values
        public IHttpActionResult Get()
        {
            var allUsers = mainBLL.GetUsersList();
            return Ok(allUsers);
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
