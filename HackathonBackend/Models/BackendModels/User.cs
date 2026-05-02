namespace Models.Backend
{
    public class User : DTO.User
    {
        public Guid ID { get; set; }
        public uint Points { get; set; }

        public List<string> Badges { get; set; } = [];

        public string EcoPalName { get; set; } = string.Empty;
    }
}
