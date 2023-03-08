using TourneyRent.DataLayer.Enumerators;

namespace TourneyRent.Presentation.Api.Views.TeamMembers
{
    public class TeamMemberView
    {
        public int TeamId { get; set; }
        public string UserId { get; set; }
        public TeamRole Role { get; set; }
    }
}
