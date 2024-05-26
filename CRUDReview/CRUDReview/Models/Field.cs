namespace CRUDReview.Models
{
    public class Field
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}
