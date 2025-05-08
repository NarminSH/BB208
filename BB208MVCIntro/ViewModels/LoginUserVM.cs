

using System.ComponentModel.DataAnnotations;

namespace BB208MVCIntro.ViewModels
{
    public class LoginUserVM
    {
        [Required]
        public string UsernameOrEmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public bool IsPersistent { get; set; }
    }
}
