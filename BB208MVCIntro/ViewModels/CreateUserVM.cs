using System.ComponentModel.DataAnnotations;

namespace BB208MVCIntro.ViewModels
{
    public class CreateUserVM
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(20)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
       
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Confirm password do not match password")]

        public string ConfirmPassword { get; set; }

    }
}
