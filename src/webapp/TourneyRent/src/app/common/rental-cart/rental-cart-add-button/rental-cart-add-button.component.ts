import { Component, Input } from '@angular/core';
import { RentalResource } from 'src/app/resources/rental.resource';
import { RentalCartItem, RentalCartService } from 'src/app/services/rental-cart.service';
import { AvailableDaysComponent } from '../../dialogs/available-days/available-days.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-rental-cart-add-button',
  templateUrl: './rental-cart-add-button.component.html',
  styleUrls: ['./rental-cart-add-button.component.scss']
})
export class RentalCartAddButtonComponent {
  @Input() item!: RentalCartItem;

  public selected: boolean = false;

  constructor(private rentalCartService: RentalCartService, private rentalResource: RentalResource, private dialog: MatDialog) {
    if (this.item === null) {
      throw "Reference to rental item is a must.";
    }
  }

  ngOnInit() {
    this.selected = !!this.rentalCartService.getItems().find(i => i.id == this.item?.id);
  }

  toggle(): void {
    if (!this.selected) {
      this.rentalResource.getAvailableDays(this.item.id).subscribe(availableDays => {
        const dialogRef = this.dialog.open(AvailableDaysComponent, {
          width: '600px',
          data: { days: availableDays, price: this.item.price },
          disableClose: true,
        });
        
        dialogRef.afterClosed().subscribe((selectedDays: Date[] | null) => {
          if (!selectedDays) {
            this.selected = false;
            return;
          }
          this.selected = true;
          this.rentalCartService.addItem({ id: this.item.id, selectedDays: selectedDays, price: this.item.price * selectedDays.length } as RentalCartItem);
        });
      });
    } else {
      this.rentalCartService.removeItem(this.item);
      this.selected = false;
    }

  }
}
