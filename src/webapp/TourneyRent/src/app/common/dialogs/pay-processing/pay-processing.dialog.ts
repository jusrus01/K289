import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-pay-processing-dialog',
  templateUrl: './pay-processing.dialog.html',
  styleUrls: ['./pay-processing.dialog.scss']
})
export class PayProcessingDialog {
  constructor(public dialogRef: MatDialogRef<PayProcessingDialog>, @Inject(MAT_DIALOG_DATA) public data: any) {
  
    if (data.entryFee <= 0) {
      this.dialogRef.close(false);  
    }
  }

  public isPaying = false;
  public status!: string;

  public pay(): void {
    this.isPaying = true;
    this.status = "Waiting for confirmation from external service";
    setTimeout(() => {
      this.status = "Processing";
      setTimeout(() => {
        this.status = "Saving"
        this.dialogRef.close(true);    
      }, 4000);
    }, 3000);
  }
}
