import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-leave-tournament',
  templateUrl: './leave-tournament.dialog.html',
})
export class LeaveTournamentDialog {
  constructor(public dialogRef: MatDialogRef<LeaveTournamentDialog>) {
  }

  public confirm(): void {
    this.dialogRef.close(true);
  }

  public cancel(): void {
    this.dialogRef.close(false);
  }
}
