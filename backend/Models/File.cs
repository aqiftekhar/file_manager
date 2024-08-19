using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FileManagerBackend.Models
{
    public class File
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int VolumeId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool SavePaper { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public byte[] BinaryData { get; set; }

        //Added Navigation
        public Volume? Volume { get; set; }
        public ICollection<TagAssignment> TagAssignments { get; set; }

        [NotMapped]
        public IEnumerable<Tag> Tags => TagAssignments?.Select(ta => ta.Tag);
    }
}

