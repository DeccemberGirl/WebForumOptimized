using System.ComponentModel.DataAnnotations;

namespace WebForum.Models
{
    /// <summary>
    /// ViewModel for login form
    /// </summary>
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}