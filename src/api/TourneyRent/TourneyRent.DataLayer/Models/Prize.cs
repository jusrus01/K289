using System.ComponentModel.DataAnnotations;
using TourneyRent.Contracts.Models;

namespace TourneyRent.DataLayer.Models
{
    public class Prize : IImage
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? ImageId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public int? TournamentId { get; set; }
        public Tournament Tournament { get; set; }
    }
}