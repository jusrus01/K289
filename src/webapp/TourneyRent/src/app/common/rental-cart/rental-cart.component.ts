import { Component } from '@angular/core';
import { RentalCartService } from 'src/app/services/rental-cart.service';

@Component({
  selector: 'app-rental-cart',
  templateUrl: './rental-cart.component.html',
  styleUrls: ['./rental-cart.component.scss']
})
export class RentalCartComponent {
  public count: number;
  public price: any;

  constructor(private rentalCartService: RentalCartService) {
    this.count = rentalCartService.getItems().length;
    this.rentalCartService.itemsUpdated$.subscribe(i => {
      setTimeout(() => {
        const items = this.rentalCartService.getItems();
        this.count = items.length;
        this.price = items.reduce((acc: any, curr: any) => acc + curr.price, 0);
      }, 500);
    });
  }
}
