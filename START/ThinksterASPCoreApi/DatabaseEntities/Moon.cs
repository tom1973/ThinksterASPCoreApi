using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThinksterASPCoreApi.DatabaseEntities
{
    public class Moon
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int PlanetId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}