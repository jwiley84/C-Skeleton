using System.ComponentModel.DataAnnotations;
 
namespace skeleton.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters")]
        [Display(Name = "First Name")]
        public string firstName { get; set; }

        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters")]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
 
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email { get; set; }
 
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"(?=.*\d)(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Password must contain at least one uppercase, one lowercase, and one number.")]
        [Display(Name = "Password")]
        public string password { get; set; }
 
        [Compare("password", ErrorMessage = "Password and confirmation must match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string confirm { get; set; }
    }
}

