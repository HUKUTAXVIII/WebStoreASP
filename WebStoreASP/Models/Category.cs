namespace WebStoreASP.Models
{
    public class Category
    {
        public int id { set; get; }
        public string name { set; get; }
        public Category(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }



}
