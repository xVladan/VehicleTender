using BusinessLogic;
using DataAccessLayer_DAL;
using DataAccessLayer_DAL.BusinessObjects;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VehicleTender.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private MainBLL mainBLL = new MainBLL();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetUsers()
        {
            try
            {
                var jsonResult = mainBLL.GetUsersList();
                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult GetAdmins()
        {
            try
            {
                var roles = mainBLL.GetRoles().FirstOrDefault(x => x.Name == "admin");
                var dbUsers = mainBLL.GetUsersList().Where(user => user.RoleId == roles.Id).ToList();



                return Json(dbUsers, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult RoleDropdown()
        {
            try
            {
                var roleDropdown = mainBLL.GetRoles();
                return Json(roleDropdown, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public void EditUser(AspNetUsersMeta editData)
        {
            try
            {
                ApplicationUserManager userManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
                AspNetRolesMeta role = mainBLL.EditDbUser(editData);
                userManager.AddToRoleAsync(editData.Id, role.Name);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult DeleteUser(string Id)
        {
            try
            {
                mainBLL.DeleteUserFromDb(Id);
                return Json("success");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult UsersDropdown()
        {
            try
            {
                var role = mainBLL.GetRoles().FirstOrDefault(r => r.Name == "user");
                var dropdown = mainBLL.GetUsersList().Where(x => x.RoleId == role.Id);
                return Json(dropdown, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}