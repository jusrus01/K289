using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourneyRent.DataLayer.Enumerators;

namespace TourneyRent.DataLayer.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string UserId { get; set; }
        public TeamRole Role { get; set; }

        public virtual Team Team { get; set; }
    }
}
