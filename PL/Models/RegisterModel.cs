using System.ComponentModel.DataAnnotations;

namespace WebForum.Models
{
    /// <summary>
    /// View Model for register form
    /// </summary>
    public class RegisterModel
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

        /// <summary>
        /// Account password confirmation
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Account user address
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Account user name
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}