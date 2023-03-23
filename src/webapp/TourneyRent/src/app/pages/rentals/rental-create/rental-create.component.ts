import { HttpClient } from '@angular/common/http';
import { RoutingService } from 'src/app/services/routing.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-rental-create',
  templateUrl: './rental-create.component.html',
  styleUrls: ['./rental-create.component.scss']
})
export class RentalCreateComponent {

  public pictureSource!: string | ArrayBuffer | null;
  public pictureFile!: any;

  name: string = '';
  description: string = '';
  image: string = '';
  periodStart: string = '';
  periodEnd: string = '';
  price: number = 0;
  BankAccountName: string = '';
  BankAccountNumber: string = '';
  TransactionReason: string = '';
  imageFile?: File;

constructor(private http: HttpClient, public routing: RoutingService) {}

  submit() {
    const data = {
      name: this.name,
      description: this.description,
      imageFile: this.imageFile,
      image: this.image,
      periodStart: this.periodStart,
      periodEnd: this.periodEnd,
      price: this.price,
      BankAccountName: this.BankAccountName,
      BankAccountNumber: this.BankAccountNumber,
      TransactionReason: this.TransactionReason
    };

    this.http.post('http://localhost:5155/RentalItem', data).subscribe((response) => {
      console.log('Data submitted successfully:', response);
    }, (error) => {
      console.error('Error submitting data:', error);
    });

  }

  onImageSelected(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0];
    this.imageFile = file || undefined;
  }
}
