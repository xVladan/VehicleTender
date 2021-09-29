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
    public class TenderController : ApiController
    {
        private MainBLL mainBLL = new MainBLL();
        private MobileLogic mobileLogic = new MobileLogic();


        // Tender Actions

        [HttpGet]
        [Route("api/tenders")]
        public IHttpActionResult AllTenders()
        {
            var tenderList = mobileLogic.GetAllTenders();
            return Ok(tenderList);

        }

        [HttpPost]
        [Route("api/tender")]
        public IHttpActionResult AddTender(Tender tenderData)
        {
            var savedTender = mobileLogic.SaveTenderInDb(tenderData);
            return Ok(savedTender);
        }

        // Tender Status Actions

        [HttpGet]
        [Route("api/status")]
        public IHttpActionResult AllTenderStatuses()
        {
            var statuses = mobileLogic.GetTenderStatuses();

            return Ok(statuses);
        }

        [HttpPost]
        [Route("api/status")]
        public IHttpActionResult AddTenderStatus(TenderStatus tenderStatus)
        {
            var savedTenderStatus = mobileLogic.AddTenderStatus(tenderStatus);
            return Ok(savedTenderStatus);
        }
    }
}
