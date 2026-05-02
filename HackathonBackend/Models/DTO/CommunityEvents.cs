namespace Models.DTO
{
    public class CommunityEvents
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Location { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = [];
        public string ImageUrl { get; set; } = string.Empty;
        public string OriginalUrl { get; set; } = string.Empty;
    }

    public class CommunityEventBatchRequest
    {
        public int StartIndex { get; set; } = 0;
        public int EndIndex { get; set; } = 4;
        public List<string>? Tags { get; set; }
        public Guid UserID { get; set; }
    }
}