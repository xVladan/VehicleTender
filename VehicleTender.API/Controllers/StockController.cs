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
                    return NotFound();
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
                    return NotFound();
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
                    return NotFound();
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
                    return NotFound();
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
                    return NotFound();
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
                    return NotFound();
                }
                return Ok(savedStock);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        [HttpPut]
        [Route("api/stock")]
        public IHttpActionResult EditStock(StockInfoMobileDTO editData)
        {
            try
            {
                var data = mobileLogic.EditStock(editData);
                if(data == null)
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
