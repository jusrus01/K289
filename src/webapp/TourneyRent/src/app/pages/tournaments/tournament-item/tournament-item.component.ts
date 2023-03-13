import { Component } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { TournamentResource } from 'src/app/resources/tournament.resource';
import { AuthService } from 'src/app/services/auth.service';
import { RoutingService } from 'src/app/services/routing.service';

@Component({
  selector: 'app-tournament-item',
  templateUrl: './tournament-item.component.html',
  styleUrls: ['./tournament-item.component.scss'],
})
export class TournamentItemComponent {
  constructor(
    private route: ActivatedRoute,
    private resource: TournamentResource,
    public dialog: MatDialog,
    public authService: AuthService,
    private routing: RoutingService
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

  openDialog(
    enterAnimationDuration: string,
    exitAnimationDuration: string
  ): void {
    const dialogRef = this.dialog.open(ConfirmDeleteDialogTemp, {
      width: '250px',
      enterAnimationDuration,
      exitAnimationDuration,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (!result) {
        return;
      }

      this.resource
        .deleteTournament(this.tournament.id)
        .subscribe((x) => this.routing.goToTournaments());
    });
  }

  public join(): void {}
}

@Component({
  template: `<h1 mat-dialog-title>Delete tournament</h1>
    <div mat-dialog-content>Are you sure?</div>
    <div mat-dialog-actions>
      <button
        mat-button
        mat-dialog-close
        cdkFocusInitial
        [mat-dialog-close]="true"
      >
        Confirm
      </button>
      <button mat-button mat-dialog-close [mat-dialog-close]="false">
        Cancel
      </button>
    </div>`,
})
export class ConfirmDeleteDialogTemp {
  constructor(public dialogRef: MatDialogRef<ConfirmDeleteDialogTemp>) {}
}
