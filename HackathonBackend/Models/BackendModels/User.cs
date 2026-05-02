namespace Models.Backend
{
    [Serializable]
    public class User : DTO.User
    {
        public Guid ID { get; set; }
    }
}
