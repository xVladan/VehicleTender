using BusinessLogic;
using BusinessLogic.DataTransferObjects.MobileDTO;
using DataAccessLayer_DAL;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace VehicleTender.API.Controllers
{
    [Authorize]
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
            var users = mobileLogic.GetUsersList();
            return Ok(users);
        }

        [HttpGet]
        [Route("api/user")]
        public IHttpActionResult GetUserByEmail(string email)
        {
            var userByEmail = mobileLogic.GetUserByEmail(email);
            return Ok(userByEmail);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [Route("api/user")]
        public async Task<IHttpActionResult> SaveUser(RegisterMobileUserDTO model)
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
            return Json(jsonMessage);
        }
    }
}
