import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { SelectPrizeDialog } from 'src/app/common/dialogs/select-prize/select-prize.dialog';
import { ErrorSnackComponent } from 'src/app/common/snacks/error/error.snack';
import { PrizeResource } from 'src/app/resources/prize.resource';
import { TournamentResource } from 'src/app/resources/tournament.resource';
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

  constructor(
    private formBuilder: FormBuilder,
    private tournamentResource: TournamentResource,
    private prizeResource: PrizeResource,
    private routing: RoutingService,
    public dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {
    this.isLoading = true;
    this.createForm = this.formBuilder.group({
      name: ['', Validators.required],
      startDate: [new Date(), Validators.required],
      endDate: [new Date(), Validators.required],
      entryFee: [1, [Validators.required, Validators.min(0), Validators.max(1000), Validators.pattern(/^[0-9]+(\.[0-9]+)?$/)]],
      participantCount: [1, [Validators.required, Validators.min(1), Validators.max(1000), Validators.pattern(/^[0-9]*$/)]],
      bankAccountName: ['', Validators.required],
      bankAccountNumber: ['', Validators.required],
      transactionReason: ['']
    });
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
    Object.keys(this.createForm.controls).forEach(key => {
      let val =  this.createForm.get([key])?.value;
      if (val instanceof Date) {
        val = val.toUTCString();
      }
      formData.append(key, val);
    });

    formData.append('imageFile', this.pictureFile ?? null);
    formData.append('prizeId', this.selectedPrize?.id ?? '')

    this.tournamentResource
      .createTournament(formData)
      .subscribe(() => this.routing.goToTournaments());
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
}