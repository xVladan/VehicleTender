using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataTransferObjects.MobileDTO
{
    public class RegisterMobileUserDTO
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Please enter your name.", MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Please enter your lastname.", MinimumLength = 1)]
        public string LastName { get; set; }

        public string UserName { get; set; }

        [Required]
        public bool isActive { get; set; }

        [Required]
        public string RoleId { get; set; }

        public string DealerName { get; set; }
        public int LocationId { get; set; }
    }
}
