using System.ComponentModel.DataAnnotations;
 
namespace skeleton.Models
{
    public class LoginViewModel : BaseEntity
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string login_email { get; set; }
 
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string login_password { get; set; }
    }
}
