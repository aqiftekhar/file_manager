namespace FileManagerBackend.Models
{
    public class Volume
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<File> Files { get; set; }
    }
}

