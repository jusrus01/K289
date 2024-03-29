﻿using System.ComponentModel.DataAnnotations;
using TourneyRent.Contracts.Models;

namespace TourneyRent.Presentation.Api.Views.Account
{
    public record UpdateUserProfileArgsView(
        [Required] string Id,
        [Required] string Email,
        [Required] string FirstName,
        [Required] string LastName);
}
