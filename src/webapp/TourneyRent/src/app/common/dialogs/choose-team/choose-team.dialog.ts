import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-choose-team',
  templateUrl: './choose-team.dialog.html',
  styleUrls: ['./choose-team.dialog.scss']
})
export class ChooseTeamDialog {
  constructor(public dialogRef: MatDialogRef<ChooseTeamDialog>, @Inject(MAT_DIALOG_DATA) public data: any) {
  }

  public selectTeam(id: any): void {
    this.dialogRef.close(id);
  }
}
