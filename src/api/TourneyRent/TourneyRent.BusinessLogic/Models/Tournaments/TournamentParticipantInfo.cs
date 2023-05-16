namespace TourneyRent.BusinessLogic.Models.Tournaments;

public class TournamentParticipantInfo
{
    public string UserId { get; set; }
    public string Email { get; set; }

    public bool IsWinner { get; set; }
}