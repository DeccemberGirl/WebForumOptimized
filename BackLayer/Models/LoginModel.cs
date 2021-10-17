using System.ComponentModel.DataAnnotations;

namespace BackLayer.Models
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