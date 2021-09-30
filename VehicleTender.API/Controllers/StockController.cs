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
    [Authorize(Roles = "admin")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StockController : ApiController
    {
        private MainBLL mainBLL = new MainBLL();
        private MobileLogic mobileLogic = new MobileLogic();


        // Manufacturer Actions

        [HttpGet]
        [Route("api/manufacturers")]
        public IHttpActionResult AllManufacturers()
        {
            try
            {
                var manufacturerList = mainBLL.GetManufacturers();
                if (manufacturerList == null)
                {
                    var errorMsg = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("Data not found")),
                        ReasonPhrase = "Data not found"
                    };
                    throw new HttpResponseException(errorMsg);
                }
                return Ok(manufacturerList);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        [HttpPost]
        [Route("api/manufacturer")]
        public IHttpActionResult AddManufacturer(Manufacturer manufacturerData)
        {
            try
            {
                var savedManufacturer = mobileLogic.SaveManufacturerInDb(manufacturerData);
                if (savedManufacturer == null)
                {
                    var errorMsg = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("Data not found")),
                        ReasonPhrase = "Data not found"
                    };
                    throw new HttpResponseException(errorMsg);
                }
                return Ok(savedManufacturer);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        // Car Model Actions 

        [HttpGet]
        [Route("api/carmodels")]
        public IHttpActionResult CarEntries()
        {
            try
            {
                var carModels = mobileLogic.GetAllCars();
                if (carModels == null)
                {
                    var errorMsg = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("Data not found")),
                        ReasonPhrase = "Data not found"
                    };
                    throw new HttpResponseException(errorMsg);
                }
                return Ok(carModels);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        [HttpPost]
        [Route("api/carmodel")]
        public IHttpActionResult AddCar(CarModelMobileDTO carData)
        {
            try
            {
                var savedCar = mobileLogic.AddCar(carData);
                if (savedCar == null)
                {
                    var errorMsg = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("Data not found")),
                        ReasonPhrase = "Data not found"
                    };
                    throw new HttpResponseException(errorMsg);
                }
                return Ok(savedCar);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        // Stock Actions

        [HttpGet]
        [Route("api/stocks")]
        public IHttpActionResult StockEntries()
        {
            try
            {
                var stocks = mobileLogic.GetAllStocks();
                if (stocks == null)
                {
                    var errorMsg = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("Data not found")),
                        ReasonPhrase = "Data not found"
                    };
                    throw new HttpResponseException(errorMsg);
                }
                return Ok(stocks);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        [HttpPost]
        [Route("api/stock")]
        public IHttpActionResult AddStock(StockInfoMobileDTO stockData)
        {
            try
            {
                var savedStock = mobileLogic.SaveStockInDb(stockData);
                if (savedStock == null)
                {
                    var errorMsg = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("Data not found")),
                        ReasonPhrase = "Data not found"
                    };
                    throw new HttpResponseException(errorMsg);
                }
                return Ok(savedStock);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }
    }
}
