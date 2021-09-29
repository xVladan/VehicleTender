using BusinessLogic;
using DataAccessLayer_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace VehicleTender.API.Controllers
{
    [Authorize]
    [EnableCors(origins: "https://localhost:44341/swagger/docs/v1", headers: "*", methods: "*")]
    public class LocationController : ApiController
    {
        private MobileLogic mobileLogic = new MobileLogic();
        private MainBLL mainBLL = new MainBLL();
        
        [HttpGet]
        [Route("api/locations")]
        public IHttpActionResult AllLocations()
        {
            try
            {
                var locations = mainBLL.LocationEntries();
                return Ok(locations);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("api/location")]
        public IHttpActionResult AddLocation(Location locationData)
        {
            try
            {
                var data = mobileLogic.SaveLocationInDB(locationData);
                return Ok(data);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
