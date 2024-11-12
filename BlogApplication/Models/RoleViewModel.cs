using BlogApplication;

namespace BlogApplication.Models
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserRoleViewModel> UserRoles { get; set; }
    }
}
