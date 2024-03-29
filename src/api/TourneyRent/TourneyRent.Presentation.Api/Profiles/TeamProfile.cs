﻿using AutoMapper;
using TourneyRent.DataLayer.Models;
using TourneyRent.Presentation.Api.Views.TeamMembers;
using TourneyRent.Presentation.Api.Views.Teams;

namespace TourneyRent.Presentation.Api.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, TeamView>();
            CreateMap<TeamCreate, Team>();
            CreateMap<TeamUpdate, Team>();

            CreateMap<TeamMember, TeamMemberView>();
            CreateMap <TeamMemberCreate,TeamMember> ();
            CreateMap <TeamMemberUpdate, TeamMember> ();
        }

    }
}
