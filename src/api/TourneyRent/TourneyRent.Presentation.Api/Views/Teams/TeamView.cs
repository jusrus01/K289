using TourneyRent.DataLayer.Models;

namespace TourneyRent.Presentation.Api.Views.Teams
{
    public class TeamView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CreatorId { get; set; }

        public List<ApplicationUser> Players { get; set; }
    }
}
