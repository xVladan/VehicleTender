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
    public class TenderStatusController : Controller
    {
        private MainBLL mainBLL = new MainBLL();

        // GET: TenderStatus
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult TenderStatusEntries()
        {
            try
            {
                var tenderStatusList = mainBLL.TenderStatusEntries();
                return Json(tenderStatusList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public void AddTenderStatus(TenderStatus tenderStatusData)
        {
            try
            {
                mainBLL.SaveTenderStatusInDb(tenderStatusData);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public void EditTenderStatusInDB(int Id, TenderStatus tenderStatus)
        {
            try
            {
                mainBLL.EditTenderStatus(Id, tenderStatus);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteTenderStatusInDB(int Id)
        {
            try
            {
                mainBLL.DeleteTenderStatus(Id);
            }
            catch (Exception)
            {

            }
        }
    }
}