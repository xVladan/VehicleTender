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
    public class LocationController : Controller
    {
        private MainBLL mainBLL = new MainBLL();

        // GET: Location
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LocationEntries()
        {
            try
            {
                var locationList = mainBLL.LocationEntries();
                return Json(locationList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost]
        public void AddLocation(Location locationData)
        {
            try
            {
                mainBLL.SaveLocationInDB(locationData);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPost]
        public void EditLocation(int Id, Location LocName)
        {
            try
            {
                mainBLL.EditLocationInDB(Id, LocName);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void DeleteLocation(int Id)
        {
            try
            {
                mainBLL.DeleteLocationByID(Id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult LocationDropdown()
        {
            try
            {
                var dropdown = mainBLL.LocationDropdown();
                return Json(dropdown, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}