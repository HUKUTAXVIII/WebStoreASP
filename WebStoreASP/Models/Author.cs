namespace WebStoreASP.Models
{
    public class Author {
        public int id { set; get; }
        public string name { set; get; }
        public Author(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }



}
