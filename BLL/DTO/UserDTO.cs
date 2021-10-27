using System.Collections.Generic;

namespace BLL.DTO
{
    /// <summary>
    /// UserDTO for ForumUser entity (asp.net Identity)
    /// </summary>
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<string> Role { get; set; }
    }
}
