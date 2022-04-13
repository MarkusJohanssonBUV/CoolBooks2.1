namespace CoolBooks.Models
{
    public partial class BooksViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string ImagePath { get; set; }

        public List<int> Autors { get; set; }
        public List<int> Genres { get; set; }
    }
}
