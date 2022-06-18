namespace WebStoreASP.Models
{
    public class User
    {
        public int id { set; get; }
        public string username { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string password { get; set; }

        public User(int id, string username, string password)
        {
            this.id = id;
            this.username = username;
            this.name = string.Empty;
            this.surname = string.Empty;
            this.password = password;
        }
        public User(int id, string username, string name, string surname, string password)
        {
            this.id = id;
            this.username = username;
            this.name = name;
            this.surname = surname;
            this.password = password;
        }
    }
}
