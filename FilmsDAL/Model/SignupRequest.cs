using System.ComponentModel.DataAnnotations;

namespace Films.DAL.Model
{
    public class SignupRequest
    {
        [Required]
        [Display(Name = "NickName")]
        public string NickName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characaters long!")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and its confirmation do not match")]
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set;  }
        public int Birthday { get; set;  }
    }
}
