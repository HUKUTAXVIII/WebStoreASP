namespace WebStoreASP.Models
{
    public class Product
    {
        public int id { set; get; }
        public string name { set; get; }
        public float price { set; get; }
        public string description { set; get; }
        public int author_id { set; get; }
        public int publisher_id { set; get; }
        public int genre_id { set; get; }
        public int category_id{set;get;}
        public int cover_id{set;get;}


        public Product(int id, string name, float price, string description, int author_id, int publisher_id,int genre_id, int category_id)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.description = description;
            this.author_id = author_id;
            this.publisher_id = publisher_id;
            this.genre_id = genre_id;
            this.category_id = category_id;
            this.cover_id = 6;
        }
        public Product(int id, string name, float price, string description, int author_id, int publisher_id, int genre_id, int category_id,int cover_id)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.description = description;
            this.author_id = author_id;
            this.publisher_id = publisher_id;
            this.genre_id = genre_id;
            this.category_id = category_id;
            this.cover_id = cover_id;
        }

    }
}
