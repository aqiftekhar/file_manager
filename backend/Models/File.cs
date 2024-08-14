namespace FileManagerBackend.Models
{
    public class File
    {
        public int Id { get; set; }
        public int VolumeId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool SavePaper { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public byte[] BinaryData { get; set; }
        public Volume Volume { get; set; }
        public ICollection<TagAssignment> TagAssignments { get; set; }
    }
}

