using BusinessLogic;
using BusinessLogic.DataTransferObjects.MobileDTO;
using DataAccessLayer_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using VehicleTender.API.Helpers;

namespace VehicleTender.API.Controllers
{
    [Authorize]
    //[EnableCors(origins: "https://localhost:44341/swagger/docs/v1", headers: "*", methods: "*")]
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
                    var errorMsg = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("Data not found")),
                        ReasonPhrase = "Data not found"
                    };
                    throw new HttpResponseException(errorMsg);
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
        public IHttpActionResult AddTender(Tender tenderData)
        {
            try
            {
                var savedTender = mobileLogic.SaveTenderInDb(tenderData);
                if (savedTender == null)
                {
                    var errorMsg = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("Data not found")),
                        ReasonPhrase = "Data not found"
                    };
                    throw new HttpResponseException(errorMsg);
                }
                return Ok(savedTender);
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
                    var errorMsg = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("Data not found")),
                        ReasonPhrase = "Data not found"
                    };
                    throw new HttpResponseException(errorMsg);
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
        public IHttpActionResult AddTenderStock(TenderStock tStockData)
        {
            try
            {
                var savedData = mobileLogic.AddTenderStock(tStockData);
                if (savedData == null)
                {
                    var errorMsg = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("Data not found")),
                        ReasonPhrase = "Data not found"
                    };
                    throw new HttpResponseException(errorMsg);
                }
                return Ok(savedData);
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
                    var errorMsg = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("Data not found")),
                        ReasonPhrase = "Data not found"
                    };
                    throw new HttpResponseException(errorMsg);
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
        public IHttpActionResult AddTenderUser(TenderUser tUserData)
        {
            try
            {
                var response = mobileLogic.AddTenderUser(tUserData);
                if (response == null)
                {
                    var errorMsg = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("Data not found")),
                        ReasonPhrase = "Data not found"
                    };
                    throw new HttpResponseException(errorMsg);
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
                    var errorMsg = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("Data not found")),
                        ReasonPhrase = "Data not found"
                    };
                    throw new HttpResponseException(errorMsg);
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
                    var errorMsg = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("Data not found")),
                        ReasonPhrase = "Data not found"
                    };
                    throw new HttpResponseException(errorMsg);
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
                    var errorMsg = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("Data not found")),
                        ReasonPhrase = "Data not found"
                    };
                    throw new HttpResponseException(errorMsg);
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
                    var errorMsg = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("Data not found")),
                        ReasonPhrase = "Data not found"
                    };
                    throw new HttpResponseException(errorMsg);
                }
                return Ok(savedBid);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }
    }
}
