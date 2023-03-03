using TourneyRent.DataLayer.Models;

namespace TourneyRent.Presentation.Api.Views.Teams
{
    public class TeamView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<TeamMember> Members { get; set; }
    }
}
