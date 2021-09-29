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
    [Authorize(Roles = "admin")]
    [EnableCors(origins: "https://localhost:44341/swagger/docs/v1", headers: "*", methods: "*")]
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
                return Ok(manufacturerList);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("api/manufacturer")]
        public IHttpActionResult AddManufacturer(Manufacturer manufacturerData)
        {
            try
            {
                var savedManufacturer = mobileLogic.SaveManufacturerInDb(manufacturerData);
                return Ok(savedManufacturer);
            }
            catch (Exception)
            {

                throw;
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
                return Ok(carModels);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("api/carmodel")]
        public IHttpActionResult AddCar(CarModel carData)
        {
            try
            {
                var savedCar = mobileLogic.AddCar(carData);
                return Ok(savedCar);
            }
            catch (Exception)
            {

                throw;
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
                return Ok(stocks);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("api/stock")]
        public IHttpActionResult AddStock(StockInfo stockData)
        {
            try
            {
                var savedStock = mobileLogic.SaveStockInDb(stockData);
                return Ok(savedStock);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
