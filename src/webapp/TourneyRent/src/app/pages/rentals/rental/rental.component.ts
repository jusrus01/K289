import { Component, OnInit } from '@angular/core';
import { RoutingService } from 'src/app/services/routing.service';
import { RentalResource } from 'src/app/resources/rental.resource';

@Component({
  selector: 'app-rental',
  templateUrl: './rental.component.html',
  styleUrls: ['./rental.component.scss'],
})
export class RentalComponent implements OnInit {
  items: any[] = [];
  selectedItems: any[] = [];

  constructor(
    private rentalResource: RentalResource,
    public routing: RoutingService
  ) {}

  ngOnInit() {
    this.rentalResource.getItems().subscribe(
      (data) => {
        this.items = data;
      },
      (error) => {
        console.log(error);
      }
    );
  }

  getItems() {
    this.rentalResource.getItems().subscribe((items) => {
      this.items = items;
    });
  }

  onSelect(item: any) {
    if (this.selectedItems.includes(item)) {
      this.selectedItems = this.selectedItems.filter((i) => i !== item);
    } else {
      this.selectedItems.push(item);
    }
  }

  onDelete() {
    if (confirm('Are you sure you want to delete the selected items?')) {
      this.selectedItems.forEach((item) => {
        this.rentalResource.deleteItem(item.id).subscribe(
          (data) => {
            console.log(data);
            this.items = this.items.filter((i) => i !== item);
          },
          (error) => {
            console.log(error);
          }
        );
      });
      //clear
      this.selectedItems = [];
    }
  }
}
