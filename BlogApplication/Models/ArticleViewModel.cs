using System.Xml.Linq;

namespace BlogApplication.Models
{
    public class ArticleViewModel
    {
        public int ArticleID { get; set; } 
        public string Title { get; set; }
        public string Content { get; set; }
        public int? UserID { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual UserViewModel User { get; set; }
        public virtual ICollection<CommentViewModel> Comments { get; set; }
        public virtual ICollection<ArticleTagViewModel> ArticleTags { get; set; }
    }
}
