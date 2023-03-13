import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-rental-create',
  templateUrl: './rental-create.component.html',
  styleUrls: ['./rental-create.component.scss']
})
export class RentalCreateComponent {
  name: string = '';
  description: string = '';
  image: string = '';
  periodStart: string = '';
  periodEnd: string = '';
  price: number = 0;

constructor(private http: HttpClient) {}

  submit() {
    const data = {
      name: this.name,
      description: this.description,
      image: this.image,
      periodStart: this.periodStart,
      periodEnd: this.periodEnd,
      price: this.price
    };

    this.http.post('http://localhost:5155/RentalItem', data).subscribe((response) => {
      console.log('Data submitted successfully:', response);
    }, (error) => {
      console.error('Error submitting data:', error);
    });
  }
}
