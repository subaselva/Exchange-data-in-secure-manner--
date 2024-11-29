

namespace Exchange_data_in_secure_manner.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; } = null;
        public string? Password { get; set; }

    }
}
