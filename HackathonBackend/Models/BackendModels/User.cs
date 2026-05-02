namespace Models.Backend
{
    public class User
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required byte[] Password { get; set; }
        public required string Email { get; set; }


        public Guid ID { get; set; }
        public uint Points { get; set; }

        public List<string> Badges { get; set; } = [];

        public string EcoPalName { get; set; } = string.Empty;

        public List<string> Tasks { get; set; } = [];
    }
}
