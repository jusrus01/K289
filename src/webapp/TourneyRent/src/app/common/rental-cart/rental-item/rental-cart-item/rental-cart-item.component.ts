import { Component, Input } from '@angular/core';
import { RentalResource } from 'src/app/resources/rental.resource';

@Component({
  selector: 'app-rental-cart-item',
  templateUrl: './rental-cart-item.component.html',
  styleUrls: ['./rental-cart-item.component.scss']
})
export class RentalCartItemComponent {
  @Input() id: number = 0;

  public item: any;
  public selected = false;

  constructor(private rentalResource: RentalResource) {
  }

  ngOnInit() {
    this.rentalResource.getItemById(this.id).subscribe(item => this.item = item);
  }
}
