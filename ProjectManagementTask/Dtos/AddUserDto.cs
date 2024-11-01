using ProjectManagement.Enums;
using ProjectManagement.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Dtos
{
    public class AddUserDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        [MinLength(2, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = Messages.Required)]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

        [DataType(DataType.Password), Display(Name = "Confirm password"),
            Compare("Password", ErrorMessage = Messages.ConfirmPassword)]
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = Messages.Required)]
        public ManagementRoles Role { get; set; }
    }
}
