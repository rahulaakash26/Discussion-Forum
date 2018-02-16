using System.ComponentModel.DataAnnotations;

namespace ForumProject1.Models
{
    public class LoginDetails
    {
        [Required(ErrorMessage = "This field is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }
    }
}