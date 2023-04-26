import { HttpClient } from '@angular/common/http';
import { RoutingService } from 'src/app/services/routing.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-rental-create',
  templateUrl: './rental-create.component.html',
  styleUrls: ['./rental-create.component.scss']
})
export class RentalCreateComponent {

  //public pictureSource!: string | ArrayBuffer | null;
  //public pictureFile!: any;

  name: string = '';
  description: string = '';
  // imag: string = '';
  periodStart: string = '';
  periodEnd: string = '';
  price: number = 0;
  bankAccountName: string = '';
  bankAccountNumber: string = '';
  transactionReason: string = '';
  // image?: File;
  // imagestring: string = 'aa';

constructor(private http: HttpClient, public routing: RoutingService) {}

  submit() {
    const data = {
      name: this.name,
      description: this.description,
      // imageFile: this.image,
      // image: this.imagestring,
      periodStart: this.periodStart,
      periodEnd: this.periodEnd,
      price: this.price,
      bankAccountName: this.bankAccountName,
      bankAccountNumber: this.bankAccountNumber,
      transactionReason: this.transactionReason
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
