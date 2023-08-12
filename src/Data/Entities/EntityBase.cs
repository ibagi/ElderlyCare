using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElderyCare.Data.Entities
{
    public class EntityBase
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Created { get; set; } = DateTime.Now;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? Updated { get; set; }
        public bool Deleted { get; set; }
    }
}