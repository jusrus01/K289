import { Component, OnInit } from '@angular/core';
import { TeamResource } from 'src/app/resources/team.resource';
import { RoutingService } from 'src/app/services/routing.service';

export interface Team {
  id: number;
  name: string;
  description: string;
}

export interface Member {
  id: number;
  name: string;
  email: string;
  teamId: number;
}

export interface TeamWithMembers {
  team: Team;
  members: Member[];
}

@Component({
  selector: 'app-team-list',
  templateUrl: './team-list.component.html',
  styleUrls: ['./team-list.component.scss']
})
export class TeamListComponent implements OnInit {
  public teams: Team[] = [];
  public teamsWithMembers: TeamWithMembers[] = [];

    constructor(
      public teamResource: TeamResource,
      public routing: RoutingService
    ) {}

    ngOnInit() {
      this.teamResource.getAllTeams().subscribe((teams: Team[]) => {
        this.teams = teams;
        for (const team of this.teams) {
          this.teamResource.getTeamMembers(team.id).subscribe((members: Member[]) => {
            this.teamsWithMembers.push({ team, members });
          });
        }
      });
    }
    
}
