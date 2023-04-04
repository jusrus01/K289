import { Component, OnInit } from '@angular/core';
import { TeamResource } from 'src/app/resources/team.resource';
import { RoutingService } from 'src/app/services/routing.service';

export interface Team {
  id: number;
  name: string;
  description: string;
}

export interface Member {
  teamId: number;
  userId: number;
  role: string;
  profile: Profile;
}

export interface TeamWithMembers {
  team: Team;
  members: Member[];
}

export interface Profile {
  firstName: string;
  lastName: string;
  imageId: number;
}

@Component({
  selector: 'app-team-list',
  templateUrl: './team-list.component.html',
  styleUrls: ['./team-list.component.scss']
})
export class TeamListComponent implements OnInit {
  public teamsWithMembers: TeamWithMembers[] = [];

  constructor(
    public teamResource: TeamResource,
    public routing: RoutingService
  ) {}

  ngOnInit(): void {
    this.teamResource.getAllTeams().subscribe((teams: Team[]) => {
      teams.forEach((team: Team) => {
        this.teamResource.getTeamMembers(team.id).subscribe((members: Member[]) => {
          const teamWithMembers: TeamWithMembers = {
            team: team,
            members: []
          };
          members.forEach((member: Member) => {
            this.teamResource.getProfile(member.userId).subscribe((profile) => {
              const memberWithProfile: Member & { profile: Profile } = { ...member, profile };
              teamWithMembers.members.push(memberWithProfile);
            });
          });
          this.teamsWithMembers.push(teamWithMembers);
        });
      });
    });
  }

}
