import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-choose-winner',
  templateUrl: './choose-winner.dialog.html',
  styleUrls: ['./choose-winner.dialog.scss'],
})
export class ChooseWinnerDialog {
  constructor(
    public dialogRef: MatDialogRef<ChooseWinnerDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {}
}
