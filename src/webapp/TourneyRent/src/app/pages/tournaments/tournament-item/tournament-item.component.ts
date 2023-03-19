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

  private _tournamentId: any;

  ngOnInit() {
    this.route.paramMap.subscribe((paramMap) => {
      this._tournamentId = paramMap.get('id');
      this.resource.getTournament(this._tournamentId).subscribe((response) => {
        this.tournament = response;
      });
    });
  }

  public getParticipantCount(): number {
    return this.tournament.participants.reduce((x: any) => x + 1, 0);
  }

  public join(): void {
    const data = { teamId: null }; // TODO: Update to allow choosing the team that the user is representing
    this.resource.joinTournament(this._tournamentId, data).subscribe((x) => this.tournament.isJoined = true);
  }

  public leave(): void {
    // TODO: Implement on next sprint
  }

  openDeleteDialog(
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
}

// Join dialog
// TODD: Export to another component
// @Component({
//   template: `<h1 mat-dialog-title>Delete tournament</h1>
//     <div mat-dialog-content>Are you sure?</div>
//     <div mat-dialog-actions>
//       <button
//         mat-button
//         mat-dialog-close
//         cdkFocusInitial
//         [mat-dialog-close]="true"
//       >
//         Confirm
//       </button>
//       <button mat-button mat-dialog-close [mat-dialog-close]="false">
//         Cancel
//       </button>
//     </div>`,
// })
// export class ConfirmJoinDialog {
//   constructor(public dialogRef: MatDialogRef<ConfirmJoinDialog>) {}
// }

// Delete dialog
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
