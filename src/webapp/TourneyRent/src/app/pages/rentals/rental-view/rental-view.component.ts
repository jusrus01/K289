import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-rental-view',
  templateUrl: './rental-view.component.html',
  styleUrls: ['./rental-view.component.scss']
})
export class RentalViewComponent {
  items: any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
  this.loadItems();
  }

  loadItems() {
    this.http.get<any[]>('http://localhost:5000/RentalItem').subscribe((response) => {
      this.items = response;
    }, (error) => {
      console.error('Error loading items:', error);
    });
  }
}
