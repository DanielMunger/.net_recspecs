using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recspecs.Models
{      
    [Table("Divisions")] 
    public class Division
    {
        public Division()
        {
            this.Teams = new HashSet<Team>();
        }
        [Key]
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string SkillLevel { get; set; }
        public int MaxTeams { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
    }
}
