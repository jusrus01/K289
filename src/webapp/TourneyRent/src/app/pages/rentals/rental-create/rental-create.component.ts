import { HttpClient } from '@angular/common/http';
import { RoutingService } from 'src/app/services/routing.service';
import { Component, ViewEncapsulation } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatCalendarCellClassFunction } from '@angular/material/datepicker';

@Component({
  selector: 'app-rental-create',
  templateUrl: './rental-create.component.html',
  styleUrls: ['./rental-create.component.scss'],
  encapsulation:ViewEncapsulation.None
})
export class RentalCreateComponent {
  daysSelected: any[] = [];
  event: any;
  isSelected = (event: any) => {
    const date =
      event.getFullYear() +
      "-" +
      ("00" + (event.getMonth() + 1)).slice(-2) +
      "-" +
      ("00" + event.getDate()).slice(-2);
    //return this.daysSelected.find(x => x == date) ? "selected" : null;
    return (this.daysSelected.find(x => x == date)) ? "selected" : "";
  };

  select(event: any, calendar: any) {
    const date =
      event.getFullYear() +
      "-" +
      ("00" + (event.getMonth() + 1)).slice(-2) +
      "-" +
      ("00" + event.getDate()).slice(-2);
    const index = this.daysSelected.findIndex(x => x == date);
    if (index < 0) this.daysSelected.push(date);
    else this.daysSelected.splice(index, 1);

    calendar.updateTodaysDate();
  }

  name: string = '';
  description: string = '';
  // imag: string = '';
  //periodStart: string = '2023-03-08 00:00:00.0000000';
  //periodEnd: string = '2023-03-09 00:00:00.0000000';
  price: number = 0;
  bankAccountName: string = '';
  bankAccountNumber: string = '';
  transactionReason: string = '';
  //availableAt: string[] = [];
  ownerId: string = '2f2c8f88-466a-46a7-973e-2e846effdf24';
  imageId: string ='0288E19F-CDA1-4423-AD01-2C057CF428E6';
  //calendarItems?: Date[] = [];
  // image?: File;
  // imagestring: string = 'aa';

constructor(private http: HttpClient, public routing: RoutingService) {}

  submit() {
    const data = {
      Name: this.name,
      Description: this.description,
      // imageFile: this.image,
      // image: this.imagestring,
      //PeriodStart: this.periodStart,
      //PeriodEnd: this.periodEnd,
      Price: this.price,
      BankAccountName: this.bankAccountName,
      BankAccountNumber: this.bankAccountNumber,
      TransactionReason: this.transactionReason,
      //availableAt: this.availableAt,
      //OwnerId: this.ownerId,
      ImageFile: this.imageId,
      CalendarItems: this.daysSelected
    };

    this.http.post('http://localhost:5000/RentalItem', data).subscribe((response) => {
      console.log('Data submitted successfully:', response);
    }, (error) => {
      console.error('Error submitting data:', error);
    });

  }

  // onImageSelected(event: Event) {
  //   const file = (event.target as HTMLInputElement).files?.[0];
  //   this.image = file;
  // }
}
