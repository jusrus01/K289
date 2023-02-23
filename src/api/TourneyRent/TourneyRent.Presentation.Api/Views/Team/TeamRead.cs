using TourneyRent.DataLayer.Models;

namespace TourneyRent.Presentation.Api.Views.Team
{
    public class TeamRead
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<ApplicationUser> Players { get; set; }
    }
}
