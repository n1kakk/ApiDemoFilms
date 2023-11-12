using System.ComponentModel.DataAnnotations;

namespace Films.DAL.Model
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "NickName is required.")]
        [Display(Name = "NickName")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
