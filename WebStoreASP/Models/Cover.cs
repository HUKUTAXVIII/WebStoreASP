namespace WebStoreASP.Models
{
    public class Cover
    {
        public int id { set; get; }
        public string name { set; get; }
        public string type { set; get; }
        public string content { set; get; }
        public Cover(int id, string name,string type, string content)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.content = content;
        }



    }
}
