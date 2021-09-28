using BusinessLogic;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VehicleTender.Controllers
{ 
    [Authorize]
    public class HomeController : Controller
    {
        private MainBLL mainBLL = new MainBLL();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetTenders()
        {
            try
            {
                string userId = User.Identity.GetUserId();
                bool userRole = User.IsInRole("admin");
                var allTenders = mainBLL.HomeTable(userId,userRole);
                return Json(allTenders, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult Tender()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetTender(string Id)
        {
            try
            {
                var tender = mainBLL.TenderInfo(Id);
                return Json(tender, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public JsonResult GetTenderCars(int Id)
        {
            try
            {
                var data = mainBLL.GetTenderCars(Id);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public JsonResult GetTenderBids(int Id)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                var data = mainBLL.GetBids(Id, userId);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}