import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TournamentResource } from 'src/app/resources/tournament.resource';

@Component({
  selector: 'app-tournament-item',
  templateUrl: './tournament-item.component.html',
  styleUrls: ['./tournament-item.component.scss'],
})
export class TournamentItemComponent {
  constructor(
    private route: ActivatedRoute,
    private resource: TournamentResource
  ) {}

  public tournament: any;

  ngOnInit() {
    this.route.paramMap.subscribe((paramMap) => {
      const id = paramMap.get('id');
      this.resource.getTournament(id).subscribe((response) => {
        this.tournament = response;
      });
    });
  }

  public join(): void {}
}
