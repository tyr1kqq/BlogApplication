using System.Data;

namespace BlogApplication.Models
{
    public class UserRoleViewModel
    {
        public int UserId { get; set; }
        public UserViewModel User { get; set; }

        public int RoleId { get; set; }
        public RoleViewModel Role { get; set; }
    }
}
