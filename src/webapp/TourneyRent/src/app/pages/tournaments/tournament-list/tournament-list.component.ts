import { Component } from '@angular/core';
import { TournamentResource } from 'src/app/resources/tournament.resource';
import { RoutingService } from 'src/app/services/routing.service';

@Component({
  selector: 'app-tournament-list',
  templateUrl: './tournament-list.component.html',
  styleUrls: ['./tournament-list.component.scss'],
})
export class TournamentListComponent {
  public tournaments: any;

  constructor(
    private tournamentResource: TournamentResource,
    public routing: RoutingService
  ) {}

  ngOnInit() {
    this.tournamentResource
      .getAllTournaments()
      .subscribe((response) => (this.tournaments = response));
  }

  public getStatus(tournament: any): any {
    const tournamentStartDate = new Date(tournament.startDate);
    const currentDate = new Date();
    if (tournamentStartDate > currentDate) {
      return 'Registration open';
    }

    if (tournamentStartDate < currentDate) {
      return 'Registration closed';
    }
  }
}
