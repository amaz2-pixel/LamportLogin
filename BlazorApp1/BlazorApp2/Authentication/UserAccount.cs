using System.ComponentModel.DataAnnotations;

namespace BlazorApp2.Authentication
{
    public class UserAccount
    {
        [Key]
        public string ID { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public string Role { get; set; }

        public int n {  get; set; }
    }
}
