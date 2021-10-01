using BusinessLogic;
using BusinessLogic.DataTransferObjects.MobileDTO;
using DataAccessLayer_DAL;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using VehicleTender.API.Helpers;

namespace VehicleTender.API.Controllers
{
    [Authorize]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private MobileLogic mobileLogic = new MobileLogic();
        private ApplicationUserManager _userManager;

        public UserController()
        {
        }

        public UserController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        [Route("api/users")]
        public IHttpActionResult GetAllUsers()
        {
            try
            {
                var users = mobileLogic.GetUsersList();
                if (users == null)
                {
                    return NotFound();
                }
                return Ok(users);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        [HttpGet]
        [Route("api/user")]
        public IHttpActionResult GetUserByEmail(string email)
        {
            try
            {
                var userByEmail = mobileLogic.GetUserByEmail(email);
                if (userByEmail == null)
                {
                    return NotFound();
                }
                return Ok(userByEmail);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("api/user")]
        public async Task<IHttpActionResult> SaveUser(RegisterMobileUserDTO model)
        {
            if (ModelState.IsValid)
            {
                MainBLL mainBLL = new MainBLL();
                var role = mainBLL.GetRoles().FirstOrDefault(r => r.Id == model.RoleId);
                string jsonMessage;
                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DealerName = model.DealerName,
                    Email = model.Email,
                    UserName = model.Email,
                    isActive = model.isActive,
                };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, role.Name);
                    return Ok(user);
                }
                jsonMessage = result.Errors.FirstOrDefault(x => x.Contains("Email"));
                return BadRequest(jsonMessage);
            }
            return BadRequest("Please check your information.");
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        [Route("api/user")]
        public IHttpActionResult EditUser(UserMobileDTO userData)
        {
            try
            {
                var changedUser = mobileLogic.EditUser(userData);
                if (changedUser == null)
                {
                    return NotFound();
                }
                UserManager.AddToRoleAsync(changedUser.Id, changedUser.RoleName);
                return Ok(changedUser);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }

        [HttpGet]
        [Route("api/roles")]
        public IHttpActionResult GetAllRoles()
        {
            try
            {
                var roles = mobileLogic.AllRoles();
                if (roles == null)
                {
                    return NotFound();
                }
                return Ok(roles);
            }
            catch (Exception error)
            {
                ErrorHandler errorHandler = new ErrorHandler(error);
                return errorHandler.HandleError();
            }
        }
    }
}
