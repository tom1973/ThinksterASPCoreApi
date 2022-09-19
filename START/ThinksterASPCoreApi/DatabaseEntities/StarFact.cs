using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ThinksterASPCoreApi.DatabaseEntities
{
    public class StarFact
    {
        public int Id { get; set; }
        public int StarId { get; set; }
        [Required]
        public string Fact { get; set; }
        public string Source { get; set; }
    }
}
