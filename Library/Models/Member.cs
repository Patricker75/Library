namespace Library.Models
{
    public partial class Member
    {
        public int ID { get; set; }
        public string Username { get; set; }

        public string FirstName { get; set; }
        public char MiddleName { get; set; }
        public string LastName { get; set; }

        public int PhoneNum { get; set; }
    }
}
