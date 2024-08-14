namespace FileManagerBackend.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagName { get; set; }
        public ICollection<TagAssignment> TagAssignments { get; set; }
    }
}

