using System.Collections.Generic;

namespace BLL.DTO
{
    /// <summary>
    /// UserDTO for ForumUser entity (asp.net Identity) which transfers the user information from DAL to BLL
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Id of the user
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// User profile password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// User`s username at the forum
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User`s real name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User`s address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The role of the user at the forum
        /// </summary>
        public List<string> Role { get; set; }
    }
}
