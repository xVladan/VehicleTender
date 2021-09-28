using BusinessLogic;
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

        public JsonResult GetTenders()
        {
            try
            {
                var allTenders = mainBLL.HomeTable();
                return Json(allTenders, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult Tender()
        {
            ViewBag.Id = Url.RequestContext.RouteData.Values["Id"];
            return View();
        }
    }
}