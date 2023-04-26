import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RentalResource } from 'src/app/resources/rental.resource';
import { RoutingService } from 'src/app/services/routing.service';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import {ThemePalette} from '@angular/material/core';
import {ProgressSpinnerMode} from '@angular/material/progress-spinner';


@Component({
  selector: 'app-rental-details',
  templateUrl: './rental-details.component.html',
  styleUrls: ['./rental-details.component.scss']
})
export class RentalDetailsComponent implements OnInit {
  item: any;

  constructor(public dialog: MatDialog, private dataService: RentalResource, private route: ActivatedRoute, public routing: RoutingService) { }

  ngOnInit(): void {
    const itemId = Number(this.route.snapshot.paramMap.get('id'));
    this.dataService.getItemById(itemId).subscribe((data: any) => {
      this.item = data;
    });
  }

  openDialog(enterAnimationDuration: string, exitAnimationDuration: string): void {
    this.dialog.open(DialogAnimationsExampleDialog, {
      width: '400px',
      height: '200px',
      enterAnimationDuration,
      exitAnimationDuration,
    });
  }
}

@Component({
  selector: 'dialog-animations-example-dialog',
  templateUrl: './dialog-animations-example-dialog.html',
})
export class DialogAnimationsExampleDialog {
  constructor(public dialogRef: MatDialogRef<DialogAnimationsExampleDialog>) {}
}

@Component({
  selector: 'progress-spinner-configurable-example',
  templateUrl: 'progress-spinner-configurable-example.html',
  styleUrls: ['progress-spinner-configurable-example.css'],
})
export class ProgressSpinnerConfigurableExample {
}


// import { Component, OnInit } from '@angular/core';
// import { ActivatedRoute, Router } from '@angular/router';
// import { RentalResource } from 'src/app/resources/rental.resource';
// import { RoutingService } from 'src/app/services/routing.service';

// @Component({
//   selector: 'app-rental-details',
//   templateUrl: './rental-details.component.html',
//   styleUrls: ['./rental-details.component.scss']
// })
// export class RentalDetailsComponent implements OnInit {
//   item: any;
//   showDialog = false;
//   showSuccess = false;

//   constructor(private dataService: RentalResource, private route: ActivatedRoute, public routing: RoutingService) { }

//   ngOnInit(): void {
//     const itemId = Number(this.route.snapshot.paramMap.get('id'));
//     this.dataService.getItemById(itemId).subscribe((data: any) => {
//       this.item = data;
//     });
//   }

//   pay(): void {
//     this.showDialog = true;
//   }

//   confirmPayment(): void {
//     this.showDialog = false;
//     this.showSuccess = true;
//     setTimeout(() => {
//       this.showSuccess = false;
//     }, 2000);
//     setTimeout(() => {
//       // Perform the transaction here
//     }, 4000);
//   }
// }



