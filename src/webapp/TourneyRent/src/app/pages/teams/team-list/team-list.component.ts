import { Component, OnInit } from '@angular/core';
import { TeamResource } from 'src/app/resources/team.resource';
import { RoutingService } from 'src/app/services/routing.service';
import { ProfileResource } from 'src/app/resources/profile.resource';
import { AuthService } from 'src/app/services/auth.service';

export interface Team {
  id: number;
  name: string;
  description: string;
}

export interface Member {
  teamId: number;
  userId: string;
  role: string;
  profile: Profile | null;
}

export interface TeamWithMembers {
  team: Team;
  members: Member[];
  teamLeader: Member | null;
}

export interface Profile {
  firstName: string;
  lastName: string;
  imageId: string | null;
}

@Component({
  selector: 'app-team-list',
  templateUrl: './team-list.component.html',
  styleUrls: ['./team-list.component.scss'],
})
export class TeamListComponent implements OnInit {
  public teamsWithMembers: TeamWithMembers[] = [];

  constructor(
    public teamResource: TeamResource,
    public routing: RoutingService,
    public profileResource: ProfileResource,
    public authService: AuthService
  ) {}

  private userId: any;

  ngOnInit(): void {
    this.teamResource.getAllTeams().subscribe((teams: Team[]) => {
      teams.forEach((team: Team) => {
        this.teamResource
          .getTeamMembers(team.id)
          .subscribe((members: Member[]) => {
            const teamWithMembers: TeamWithMembers = {
              team: team,
              members: [],
              teamLeader: null,
            };
            members.forEach((member: Member) => {
              this.profileResource
                .getProfile(member.userId)
                .subscribe((profile) => {
                  const memberWithProfile: Member & { profile: Profile } = {
                    ...member,
                    profile,
                  };
                  teamWithMembers.members.push(memberWithProfile);

                  const teamLeaderWithProfile: Member & { profile: Profile } = {
                    ...member,
                    profile,
                  };

                  this.userId = this.authService.getAuthUserId();
                  const LeaveButton = false;
                  if (
                    member.userId == this.userId &&
                    member.role != 'TeamLeader'
                  ) {
                    const LeaveButton = true;
                  }

                  if (member.role === 'TeamLeader') {
                    teamWithMembers.teamLeader = teamLeaderWithProfile;
                  }
                });
            });
            this.teamsWithMembers.push(teamWithMembers);
          });
      });
    });
  }

  public leaveTeam(event: Event, teamId: any, member: any): void {
    event?.stopPropagation();
    this.teamResource
      .removeTeamMember(teamId, this.authService.getAuthUserId())
      .subscribe(() => {
        member.userId = null;
      });
  }
}
