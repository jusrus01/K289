import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { PayProcessingDialog } from 'src/app/common/dialogs/pay-processing/pay-processing.dialog';
import { SelectPrizeDialog } from 'src/app/common/dialogs/select-prize/select-prize.dialog';
import { ErrorSnackComponent } from 'src/app/common/snacks/error/error.snack';
import { PrizeResource } from 'src/app/resources/prize.resource';
import { TournamentResource } from 'src/app/resources/tournament.resource';
import { RentalCartItem, RentalCartService } from 'src/app/services/rental-cart.service';
import { RoutingService } from 'src/app/services/routing.service';

@Component({
  selector: 'app-tournament-create',
  templateUrl: './tournament-create.component.html',
  styleUrls: ['./tournament-create.component.scss'],
})
export class TournamentCreateComponent {
  public isLoading: boolean;
  public prizes: any;
  public selectedPrize: any;

  public pictureSource!: string | ArrayBuffer | null;
  public pictureFile!: any;
  public createForm: FormGroup;

  public showBankAccountForm = true;

  public rentalItems: RentalCartItem[];

  public total: any = 0;
  
  // [{id: {id}, days: [{day}, {day}]}]
  public selectedRentalItems: any = [];

  private defaultStartDateOffset = 5;
  private defaultEndDateOffset = 10;

  constructor(
    private formBuilder: FormBuilder,
    private tournamentResource: TournamentResource,
    private prizeResource: PrizeResource,
    private routing: RoutingService,
    public dialog: MatDialog,
    private snackBar: MatSnackBar,
    private rentalCartService: RentalCartService
  ) {
    this.isLoading = true;
    this.rentalItems = this.rentalCartService.getItems();
    this.createForm = this.formBuilder.group({
      name: ['', Validators.required],
      startDate: [this.createDateFromCurrentDate(this.defaultStartDateOffset), Validators.required],
      endDate: [this.createDateFromCurrentDate(this.defaultEndDateOffset), Validators.required],
      entryFee: [1, [Validators.required, Validators.min(0), Validators.max(1000), Validators.pattern(/^[0-9]+(\.[0-9]+)?$/)]],
      participantCount: [1, [Validators.required, Validators.min(1), Validators.max(1000), Validators.pattern(/^[0-9]*$/)]],
      bankAccountName: ['', Validators.required],
      bankAccountNumber: ['', Validators.required],
      transactionReason: ['']
    });
  }

  createDateFromCurrentDate(offset: number) {
    const currentDate = new Date();
    const date = new Date();
    date.setDate(currentDate.getDate() + offset);
    return date;
  }

  ngOnInit() {
    this.prizeResource.getAvailablePrizes().subscribe(response => {
      this.prizes = response;
      this.isLoading = false;
    })
  }

  public create(): void {
    if (!this.createForm.valid) {
      return;
    }

    const startDate = this.createForm.get(['startDate'])?.value as Date;
    const endDate =  this.createForm.get(['endDate'])?.value as Date;
    if (startDate >= endDate && startDate.getTime() >= endDate.getTime()) {
      this.snackBar.openFromComponent(ErrorSnackComponent, {
        duration: 1500,
        data: "The start date cannot be after the end date",
      } as MatSnackBarConfig);
      return;
    }

    if (startDate < new Date()) {
      this.snackBar.openFromComponent(ErrorSnackComponent, {
        duration: 1500,
        data: "The start date has to be after today's date",
      } as MatSnackBarConfig);
      return;
    }

    const formData = new FormData();
    if (this.total > 0) {
      const dialogRef = this.dialog.open(PayProcessingDialog, {
        width: '600px',
        data: { entryFee: this.total },
        disableClose: true,
      });

      dialogRef.afterClosed().subscribe((result) => {
        if (!result) {
          return;
        }

        formData.append('reservation', JSON.stringify(this.selectedRentalItems));

        this.internalCreate(formData);
      });
    } else {
      this.internalCreate(formData);
    }
  }

  private internalCreate(formData: any) {
    Object.keys(this.createForm.controls).forEach(key => {
      let val = this.createForm.get([key])?.value;
      if (val instanceof Date) {
        val = val.toUTCString();
      }
      formData.append(key, val);
    });

    formData.append('imageFile', this.pictureFile ?? null);
    formData.append('prizeId', this.selectedPrize?.id ?? '');

    this.tournamentResource
      .createTournament(formData)
      .subscribe(() => {
        for (const item of this.selectedRentalItems) {
          this.rentalCartService.removeItem(item);
        }
        this.routing.goToTournaments();
      });
  }

  onEntryFeeChange(): void {
    const entryFee = this.createForm.get(['entryFee'])?.value;
    this.showBankAccountForm = entryFee > 0;

    const controlNames = ['bankAccountName', 'bankAccountNumber'];
    for (const controlName of controlNames) {
      const control = this.createForm.get(controlName);
      if (this.showBankAccountForm) {
        control?.setValidators([Validators.required]);
      } else {
        control?.clearValidators();
      }
      control?.updateValueAndValidity();
    }
  }

  selectPrize(): any {
    const dialogRef = this.dialog.open(SelectPrizeDialog, {
      width: '600px',
      data: this.prizes,
      disableClose: true,
    });

    dialogRef.afterClosed().subscribe((result) => {
      this.selectedPrize = result;
    });
  }

  onFileUpload(event: any, upload: any): void {
    this.pictureFile = event.target.files[0];

    const reader = new FileReader();
    reader.onload = () => (this.pictureSource = reader.result);
    reader.readAsDataURL(this.pictureFile);
    upload.value = null;
    this.createForm.patchValue({
      pictureFile: this.pictureFile,
    });
  }

  selectRental(id: any) {
    const selectedRentalItem = this.selectedRentalItems.find((item: any) => item.id == id);
    const cartRentalItem = this.rentalItems.find(item => item.id == id);

    if (selectedRentalItem != null) {
      this.selectedRentalItems = this.selectedRentalItems.filter((item: any) => item.id != id);
      this.total -= cartRentalItem?.price; // oof
      return;
    }
    
    const days = cartRentalItem?.selectedDays;
    this.total += cartRentalItem?.price; // oof
    this.selectedRentalItems.push({
      id: id,
      days: days,
    });
  }
}