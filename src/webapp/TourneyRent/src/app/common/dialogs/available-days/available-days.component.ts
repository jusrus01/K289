import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-available-days',
  templateUrl: './available-days.component.html',
  styleUrls: ['./available-days.component.scss']
})
export class AvailableDaysComponent {
  public dates: any;

  constructor(public dialogRef: MatDialogRef<AvailableDaysComponent>, @Inject(MAT_DIALOG_DATA) public data: any) {
    this.data.days = this.data.days.map((date : any) => ({date: date, isSelected: false}));
    this.changePage({ pageIndex: 0, pageSize: 5 } as PageEvent);
  }

  changePage(e: PageEvent) {
    this.dates = [];
    for (let page = e.pageIndex * e.pageSize; page < e.pageIndex * e.pageSize + e.pageSize; page++) {
      if (this.data.days.length <= page) {
        return;
      }

      this.dates.push(this.data.days[page]);
    }
  }

  confirm() {
    this.dialogRef.close(this.data.days.filter((i: any) => i.isSelected).map((i: any) => i.date));
  }

  getTotalPrice() {
    return this.data.days.filter((i: any) => i.isSelected).length * this.data.price;
  }

  hasDates(dates: any) {
    return dates?.length > 0;
  }
}
