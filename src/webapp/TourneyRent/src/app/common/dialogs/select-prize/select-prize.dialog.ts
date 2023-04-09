import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-select-prize',
  templateUrl: './select-prize.dialog.html',
})
export class SelectPrizeDialog {
  constructor(public dialogRef: MatDialogRef<SelectPrizeDialog>, @Inject(MAT_DIALOG_DATA) public data: any) {
  }

  public choosePrize(prize: any): void {
    this.dialogRef.close(prize);
  }
}
