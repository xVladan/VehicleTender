using BusinessLogic;
using DataAccessLayer_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VehicleTender.Controllers
{
    [Authorize]
    public class StockController : Controller
    {
        private MainBLL mainBLL = new MainBLL();
        // Manufacturer Actions
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ManufacturerEntries()
        {
            try
            {
                var manufacturerLsit = mainBLL.GetManufacturers();
                return Json(manufacturerLsit, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult ManufacturerDropdown()
        {
            try
            {
                var dropdown = mainBLL.ManufacturerDropdown();
                return Json(dropdown, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public void AddManufacturer(Manufacturer manufacturerData)
        {
            try
            {
                mainBLL.SaveManufacturerInDB(manufacturerData);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public void EditManufacturer(int Id, string MFName)
        {
            try
            {
                mainBLL.EditDbManufacturer(Id, MFName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteManufacturer(int Id)
        {
            try
            {
                mainBLL.DeleteManufacturer(Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Car Actions
        public ActionResult CarModels()
        {
            return View();
        }

        [HttpGet]
        public JsonResult CarEntries()
        {
            try
            {
                var carmodels = mainBLL.CarEntries();
                return Json(carmodels, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult CarModelDropdown()
        {
            try
            {
                var dropdown = mainBLL.CarModelDropdown();
                return Json(dropdown, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public void CreateCar(CarModel carData)
        {
            try
            {
                mainBLL.CreateCar(carData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public void EditCar(CarModel carModel)
        {
            try
            {
                mainBLL.EditCar(carModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteCar(int Id)
        {
            try
            {
                mainBLL.DeleteCar(Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        // Stock Actions

        public ActionResult Stocks()
        {
            return View();
        }

        [HttpGet]

        public JsonResult StockEntries()
        {
            try
            {
                var stocks = mainBLL.StockEntries();
                return Json(stocks, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public void CreateStock(StockInfo stockData)
        {
            try
            {
                mainBLL.CreateStock(stockData);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public void EditStock(StockInfo stockModel)
        {
            try
            {
                mainBLL.EditStock(stockModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteStock(int Id)
        {
            try
            {
                mainBLL.DeleteStock(Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult StockDropdown()
        {
            try
            {
                var dropdown = mainBLL.StockDropdown();
                return Json(dropdown, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}