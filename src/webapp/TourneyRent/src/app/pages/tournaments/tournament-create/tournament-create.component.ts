import { Component } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { PayProcessingDialog } from 'src/app/common/dialogs/pay-processing/pay-processing.dialog';
import { SelectPrizeDialog } from 'src/app/common/dialogs/select-prize/select-prize.dialog';
import { PrizeResource } from 'src/app/resources/prize.resource';
import { TournamentResource } from 'src/app/resources/tournament.resource';
import {
  RentalCartItem,
  RentalCartService,
} from 'src/app/services/rental-cart.service';
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

  public startTime: any;
  public endTime: any;

  constructor(
    private formBuilder: FormBuilder,
    private tournamentResource: TournamentResource,
    private prizeResource: PrizeResource,
    private routing: RoutingService,
    public dialog: MatDialog,
    private rentalCartService: RentalCartService
  ) {
    this.isLoading = true;
    this.rentalItems = this.rentalCartService.getItems();
    this.createForm = this.formBuilder.group({
      name: ['', Validators.required],
      startDate: [
        this.createDateFromCurrentDate(this.defaultStartDateOffset),
        [Validators.required, this.startDateValidator()],
      ],
      startTime: [],
      endDate: [
        this.createDateFromCurrentDate(this.defaultEndDateOffset),
        [Validators.required, this.endDateValidator()],
      ],
      endTime: [],
      entryFee: [
        1,
        [
          Validators.required,
          Validators.min(0),
          Validators.max(1000),
          Validators.pattern(/^[0-9]+(\.[0-9]+)?$/),
        ],
      ],
      participantCount: [
        1,
        [
          Validators.required,
          Validators.min(1),
          Validators.max(1000),
          Validators.pattern(/^[0-9]*$/),
        ],
      ],
      bankAccountName: ['', Validators.required],
      bankAccountNumber: ['', Validators.required],
      transactionReason: [''],
      prizeName: [''],
      prizeDescription: [''],
    });
  }

  createDateFromCurrentDate(offset: number) {
    const currentDate = new Date();
    const date = new Date();
    date.setDate(currentDate.getDate() + offset);
    return date;
  }

  ngOnInit() {
    this.startRecheckCycleForFormControl('startDate', 'startTime');
    this.startRecheckCycleForFormControl('endDate', 'endTime');

    this.prizeResource.getAvailablePrizes().subscribe((response) => {
      this.prizes = response;
      this.isLoading = false;
    });
  }

  public create(): void {
    if (!this.createForm.valid) {
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

        formData.append(
          'reservation',
          JSON.stringify(this.selectedRentalItems)
        );

        this.internalCreate(formData);
      });
    } else {
      this.internalCreate(formData);
    }
  }

  private internalCreate(formData: any) {
    Object.keys(this.createForm.controls).forEach((key) => {
      let val = this.createForm.get([key])?.value;
      if (val instanceof Date) {
        val = val.toUTCString();
      }
      formData.append(key, val);
    });

    formData.append('imageFile', this.pictureFile ?? null);

    if (this.selectedPrize && !this.selectedPrize.custom) {
      formData.append('prizeId', this.selectedPrize?.id ?? '');
    } else if (this.selectedPrize && this.selectedPrize.custom) {
      formData.append('prizeImageFile', this.prizePictureFile ?? null);
    }

    this.tournamentResource.createTournament(formData).subscribe(() => {
      for (const item of this.selectedRentalItems) {
        this.rentalCartService.removeItem(item);
      }
      this.routing.goToTournaments();
    });
  }

  isSelectingCustomPrize() {
    return this.selectedPrize && this.selectedPrize.custom;
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

      const controlNames = ['prizeName', 'prizeDescription'];
      for (const controlName of controlNames) {
        const control = this.createForm.get(controlName);
        if (this.isSelectingCustomPrize()) {
          control?.setValidators([Validators.required]);
        } else {
          control?.clearValidators();
        }
        control?.updateValueAndValidity();
      }
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

  prizePictureFile: any;
  prizePictureSource: any;
  onFileUpload2(event: any, upload: any): void {
    this.prizePictureFile = event.target.files[0];

    const reader = new FileReader();
    reader.onload = () => (this.prizePictureSource = reader.result);
    reader.readAsDataURL(this.prizePictureFile);
    upload.value = null;
  }

  selectRental(id: any) {
    const selectedRentalItem = this.selectedRentalItems.find(
      (item: any) => item.id == id
    );
    const cartRentalItem = this.rentalItems.find((item) => item.id == id);

    if (selectedRentalItem != null) {
      this.selectedRentalItems = this.selectedRentalItems.filter(
        (item: any) => item.id != id
      );
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

  startDateValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const startDate = control.value;
      const validationErrorResponse = { valid: false };
      const successResponse = null;

      const requestDate = new Date();
      if (startDate < requestDate) {
        return validationErrorResponse;
      }

      const endDate = this.createForm?.get(['endDate'])?.value;
      if (!endDate) {
        return successResponse;
      }

      if (endDate < startDate) {
        return validationErrorResponse;
      }

      return successResponse;
    };
  }

  endDateValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const endDate = control.value;
      const validationErrorResponse = { valid: false };
      const successResponse = null;

      const requestDate = new Date();
      if (endDate < requestDate) {
        return validationErrorResponse;
      }

      const startDate = this.createForm?.get(['startDate'])?.value;
      if (!startDate) {
        return successResponse;
      }

      if (endDate < startDate) {
        return validationErrorResponse;
      }

      return successResponse;
    };
  }

  private startRecheckCycleForFormControl(
    controlKey: string,
    timeControlKey: string
  ) {
    setTimeout(() => {
      const dateControl = this.createForm?.controls[controlKey];
      const timeControl = this.createForm?.controls[timeControlKey];
      if (dateControl && timeControl) {
        dateControl.setValue(
          this.applyTimeFromDate(dateControl.value, timeControl.value)
        );
        dateControl.updateValueAndValidity();
      }

      this.startRecheckCycleForFormControl(controlKey, timeControlKey);
    }, 1000);
  }

  private applyTimeFromDate(date: any, dateWithTime: any) {
    const hours = dateWithTime.getHours();
    const minutes = dateWithTime.getMinutes();
    const seconds = dateWithTime.getSeconds();

    date.setHours(hours);
    date.setMinutes(minutes);
    date.setSeconds(seconds);

    // Return the updated date object
    return date;
  }
}
