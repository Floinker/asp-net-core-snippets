namespace CodeSnippets.Models
{
    public class Snippet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string Author { get; set; }

        public string Code { get; set; }

        public Snippet()
        {
            
        }
    }
}
