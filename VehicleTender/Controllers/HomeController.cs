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
                var allTenders = mainBLL.HomeTable(userId, userRole);
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
        public JsonResult GetTender(int Id)
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
                var adminRole = User.IsInRole("admin");
                
                if(adminRole == true)
                {
                    var data = mainBLL.GetTenderCars(Id, null);
                    return Json(data, JsonRequestBehavior.AllowGet);
                } else
                {
                    string userId = User.Identity.GetUserId();
                    var data = mainBLL.GetTenderCars(Id, userId);
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult GetBids(int id, int stockId)
        {
            try
            {
                var bids = mainBLL.BidsByTenderIdAndStockId(id, stockId);
                return Json(bids, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public void AddBid(int TenderStockId, double Price, int TenderId)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                mainBLL.AddBid(TenderStockId, Price, userId, TenderId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public void SelectWinnerBid(int Id, int tenderId, int stockId)
        {
            try
            {
                mainBLL.SaveWinnerBid(Id, tenderId, stockId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}