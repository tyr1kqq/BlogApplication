using System.Xml.Linq;

namespace BlogApplication.Models
{
    public class UserViewModel
    {
        public int UserID { get; set; } 
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<ArticleViewModel> Articles { get; set; }
        public virtual ICollection<CommentViewModel> Comments { get; set; }
        public virtual ICollection<UserRoleViewModel> UserRoles { get; set; } // Добавлено для связи с ролями.
    }
}
