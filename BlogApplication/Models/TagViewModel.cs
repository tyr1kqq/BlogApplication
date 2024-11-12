namespace BlogApplication.Models
{
    public class TagViewModel
    {
        public int TagID { get; set; }
        public string TagName { get; set; }

        public virtual ICollection<ArticleTagViewModel> ArticleTags { get; set; }
    }
}
