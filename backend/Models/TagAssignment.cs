using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileManagerBackend.Models
{
    public class TagAssignment
    {
         [Key, Column(Order = 0)]
        public int FileId { get; set; }
        public int TagId { get; set; }

        [Key, Column(Order = 1)]
        public File File { get; set; }
        public Tag Tag { get; set; }

    }
}

