namespace WebStoreASP.Models
{
    public class Genre
    {
        public int id { set; get; }
        public string name { set; get; }
        public Genre(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }



}
