namespace WebStoreASP.Models
{
    public class Publisher
    {
        public int id { set; get; }
        public string name { set; get; }
        public Publisher(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }



}
