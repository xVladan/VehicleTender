using BusinessLogic;
using DataAccessLayer_DAL;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using VehicleTender.API.Helpers;

namespace VehicleTender.API.Controllers
{
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
                if (locations == null)
                {
                    return NotFound();
                }
                return Ok(locations);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        [HttpPost]
        [Route("api/location")]
        public IHttpActionResult AddLocation(Location locationData)
        {
            try
            {
                var data = mobileLogic.SaveLocationInDB(locationData);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }
    }
}
