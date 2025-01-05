

namespace BackendStore.Model
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
