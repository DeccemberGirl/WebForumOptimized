using System.ComponentModel.DataAnnotations;

namespace WebForum.Models
{
    /// <summary>
    /// ViewModel for login form
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Account email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Account password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}