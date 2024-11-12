namespace BlogApplication.Models
{
    public class CommentViewModel
    {
        public int CommentID { get; set; } 
        public int ArticleID { get; set; }
        public int UserID { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ArticleViewModel Article { get; set; }
        public virtual UserViewModel User { get; set; }
    }
}
