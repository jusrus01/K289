import { Component } from '@angular/core';
import { TournamentResource } from 'src/app/resources/tournament.resource';
import { RoutingService } from 'src/app/services/routing.service';
import { TournamentService } from 'src/app/services/tournament.service';

@Component({
  selector: 'app-tournament-list',
  templateUrl: './tournament-list.component.html',
  styleUrls: ['./tournament-list.component.scss'],
})
export class TournamentListComponent {
  public tournaments: any;

  constructor(
    private tournamentResource: TournamentResource,
    public routing: RoutingService,
    private tournamentService: TournamentService
  ) {}

  ngOnInit() {
    this.tournamentResource
      .getAllTournaments()
      .subscribe((response) => (this.tournaments = response));
  }

  public getParticipantCount(tournament: any): number {
    return tournament.participants.reduce((x: any) => x + 1, 0);
  }

  public getStatus(tournament: any): any {
    return this.tournamentService.getTournamentStatus(tournament).message;
  }
}
