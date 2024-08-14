using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileManagerBackend.Models
{
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string TagName { get; set; }

        //Added Navigation
        public ICollection<TagAssignment> TagAssignments { get; set; }
    }
}

