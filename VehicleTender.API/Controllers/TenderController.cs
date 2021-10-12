using BusinessLogic;
using BusinessLogic.DataTransferObjects.MobileDTO;
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
    public class TenderController : ApiController
    {
        private MainBLL mainBLL = new MainBLL();
        private MobileLogic mobileLogic = new MobileLogic();


        // Tender Actions

        [HttpGet]
        [Route("api/tenders")]
        public IHttpActionResult AllTenders()
        {
            try
            {
                var tenderList = mobileLogic.GetAllTenders();
                if (tenderList == null)
                {
                    return NotFound();
                }
                return Ok(tenderList);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }

        }

        [HttpPost]
        [Route("api/tender")]
        public IHttpActionResult AddTender(TenderMobileDTO tenderData)
        {
            try
            {
                var savedTender = mobileLogic.SaveTenderInDb(tenderData);
                if (savedTender == null)
                {
                    return NotFound();
                }
                return Ok(savedTender);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        [HttpPut]
        [Route("api/tender")]
        public IHttpActionResult EditTender(TenderMobileDTO editData)
        {
            try
            {
                int tenderId = editData.Id;
                int statusId = editData.StatusId;
                var editedTender = mobileLogic.EditTender(tenderId, statusId);
                if(editedTender == null)
                {
                    return NotFound();
                }
                return Ok(editedTender);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        [HttpGet]
        [Route("api/tenderstocks")]
        public IHttpActionResult GetTenderStocks()
        {
            try
            {
                var tenderStocks = mobileLogic.AllTenderStocks();
                if (tenderStocks == null)
                {
                    return NotFound();
                }
                return Ok(tenderStocks);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        [HttpPost]
        [Route("api/tenderstock")]
        public IHttpActionResult AddTenderStock(TenderStockMobileDTO tStockData)
        {
            try
            {
                var savedData = mobileLogic.AddTenderStock(tStockData);
                if (savedData == null)
                {
                    return NotFound();
                }
                return Ok(savedData);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        [HttpPut]
        [Route("api/tenderstock")]
        public IHttpActionResult EditTenderStock(TenderStockMobileDTO editData)
        {
            try
            {
                var editedData = mobileLogic.EditTenderStock(editData);
                if(editedData == null)
                {
                    return NotFound();
                }
                return Ok(editedData);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        [HttpGet]
        [Route("api/tenderusers")]
        public IHttpActionResult GetTenderUsers()
        {
            try
            {
                var tenderUsers = mobileLogic.AllTenderUsers();
                if (tenderUsers == null)
                {
                    return NotFound();
                }
                return Ok(tenderUsers);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        [HttpPost]
        [Route("api/tenderuser")]
        public IHttpActionResult AddTenderUser(TenderUserMobileDTO tUserData)
        {
            try
            {
                var response = mobileLogic.AddTenderUser(tUserData);
                if (response == null)
                {
                    return NotFound();
                }
                return Ok(response);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }


        // Tender Status Actions

        [HttpGet]
        [Route("api/status")]
        public IHttpActionResult AllTenderStatuses()
        {
            try
            {
                var statuses = mobileLogic.GetTenderStatuses();
                if (statuses == null)
                {
                    return NotFound();
                }
                return Ok(statuses);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        [HttpPost]
        [Route("api/status")]
        public IHttpActionResult AddTenderStatus(TenderStatus tenderStatus)
        {
            try
            {
                var savedTenderStatus = mobileLogic.AddTenderStatus(tenderStatus);
                if (savedTenderStatus == null)
                {
                    return NotFound();
                }
                return Ok(savedTenderStatus);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        // Bid actions

        [HttpGet]
        [Route("api/bids")]
        public IHttpActionResult AllBids()
        {
            try
            {
                var bids = mobileLogic.AllBids();
                if (bids == null)
                {
                    return NotFound();
                }
                return Ok(bids);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        [HttpPost]
        [Route("api/bid")]
        public IHttpActionResult AddBid(BidMobileDTO bidData)
        {
            try
            {
                var savedBid = mobileLogic.SaveBid(bidData);
                if (savedBid == null)
                {
                    return NotFound();
                }
                return Ok(savedBid);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        [HttpPut]
        [Route("api/bid")]
        public IHttpActionResult SetWiningBid(BidMobileDTO bidData)
        {
            try
            {
                var changedBid = mobileLogic.SetWiningBid(bidData);
                if (changedBid == null)
                {
                    return NotFound();
                }
                return Ok(changedBid);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }
    }
}
