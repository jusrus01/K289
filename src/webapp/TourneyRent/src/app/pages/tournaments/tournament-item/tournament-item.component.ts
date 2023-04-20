import { Component } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { ChooseTeamDialog } from 'src/app/common/dialogs/choose-team/choose-team.dialog';
import { LeaveTournamentDialog } from 'src/app/common/dialogs/leave-tournament/leave-tournament.dialog';
import { PayProcessingDialog } from 'src/app/common/dialogs/pay-processing/pay-processing.dialog';
import { TeamResource } from 'src/app/resources/team.resource';
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
    private teamResource: TeamResource,
    public dialog: MatDialog,
    public authService: AuthService,
    private routing: RoutingService
  ) {}

  public tournament: any;
  public teams: any;

  private _tournamentId: any;

  ngOnInit() {
    this.teamResource
      .getUserTeams(this.authService.getAuthUserId())
      .subscribe(response => this.teams = response);

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
    this._selectTeam()
      .subscribe((teamId: any) => {
        if (this.tournament.entryFee <= 0) {
          this._internalJoin(teamId);
        } else {
          const dialogRef = this.dialog.open(PayProcessingDialog, {
            width: '600px',
            data: { entryFee: this.tournament.entryFee },
            disableClose: true,
          });
      
          dialogRef.afterClosed().subscribe((result) => {
            if (!result) {
              return;
            }
            this._internalJoin(teamId);
          });
        }
      })
  }

  private _selectTeam(): any {
    if (this.teams.length == 0) {
      return of(null);
    }

    const dialogRef = this.dialog.open(ChooseTeamDialog, {
      width: '600px',
      data: this.teams,
      disableClose: true,
    });

    return dialogRef.afterClosed();
  }

  private _internalJoin(teamId: any) {
    const data = { teamId: teamId };
    this.resource
      .joinTournament(this._tournamentId, data)
      .subscribe((x) => { 
        this.tournament.isJoined = true;
        // will not work when displaying more info
        // change if we will need a better representation
        this.tournament.participants.push({});
      });
  }

  public leave(): void {
    const dialogRef = this.dialog.open(LeaveTournamentDialog, {
      width: '600px',
      disableClose: true,
    });

    dialogRef.afterClosed().subscribe(result => {
      if (!result) {
        return;
      }

      this.resource.leaveTournament(this._tournamentId).subscribe(() => {
        // will not work if we will display more info later
        // change if we will need a better representation
        this.tournament.participants.pop();

        this.tournament.isJoined = false;
      });      
    });
  }

  public isFull(): boolean {
    return this.getParticipantCount() == this.tournament.participantCount; 
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

// bad
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
