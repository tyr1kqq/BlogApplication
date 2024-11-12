namespace BlogApplication.Models
{
    public class ArticleTagViewModel
    {
        public int ArticleID { get; set; }
        public int TagID { get; set; } 

        public virtual ArticleViewModel Article { get; set; }
        public virtual TagViewModel Tag { get; set; }
    }
}
