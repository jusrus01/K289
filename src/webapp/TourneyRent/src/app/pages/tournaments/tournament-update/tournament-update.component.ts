import { Component } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { TournamentResource } from 'src/app/resources/tournament.resource';
import { RoutingService } from 'src/app/services/routing.service';

@Component({
  selector: 'app-tournament-update',
  templateUrl: './tournament-update.component.html',
  styleUrls: ['./tournament-update.component.scss'],
})
export class TournamentUpdateComponent {
  public pictureSource!: string | ArrayBuffer | null;
  public pictureFile!: any;
  public updateForm: FormGroup;

  public showBankAccountForm = true;

  private _tournamentId: any;
  public tournament: any;

  constructor(
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private tournamentResource: TournamentResource,
    private routing: RoutingService
  ) {
    this.updateForm = this.formBuilder.group({
      name: ['', Validators.required],
      startDate: ['', [Validators.required, this.startDateValidator()]],
      startTime: [],
      endDate: ['', [Validators.required, this.endDateValidator()]],
      endTime: [],
      bankAccountName: ['', Validators.required],
      bankAccountNumber: ['', Validators.required],
      transactionReason: [''],
    });
  }

  ngOnInit() {
    this.startRecheckCycleForFormControl('startDate', 'startTime');
    this.startRecheckCycleForFormControl('endDate', 'endTime');

    this.route.paramMap.subscribe((paramMap) => {
      this._tournamentId = paramMap.get('id');
      this.tournamentResource
        .getTournament(this._tournamentId)
        .subscribe((response) => {
          this.tournament = response;
          this.tournament.startDate = new Date(this.tournament.startDate);
          this.tournament.endDate = new Date(this.tournament.endDate);
          this.updateForm.patchValue(this.tournament);

          this.showBankAccountForm = this.tournament.entryFee > 0;
        });
    });
  }

  public update(): void {
    const controlNames = ['bankAccountName', 'bankAccountNumber'];
    for (const controlName of controlNames) {
      const control = this.updateForm.get(controlName);
      if (this.showBankAccountForm) {
        control?.setValidators([Validators.required]);
      } else {
        control?.clearValidators();
      }
      control?.updateValueAndValidity();
    }

    if (!this.updateForm.valid) {
      return;
    }

    const formData = new FormData();
    Object.keys(this.updateForm.controls).forEach((key) => {
      let val = this.updateForm.get([key])?.value;
      if (val instanceof Date) {
        val = val.toUTCString();
      }
      formData.append(key, val);
    });

    formData.append('imageFile', this.pictureFile ?? null);

    this.tournamentResource
      .updateTournament(formData, this._tournamentId)
      .subscribe(() => this.routing.goToTournaments());
  }

  onFileUpload(event: any, upload: any): void {
    this.pictureFile = event.target.files[0];

    const reader = new FileReader();
    reader.onload = () => (this.pictureSource = reader.result);
    reader.readAsDataURL(this.pictureFile);
    upload.value = null;
    this.updateForm.patchValue({
      pictureFile: this.pictureFile,
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

      const endDate = this.updateForm?.get(['endDate'])?.value;
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

      const startDate = this.updateForm?.get(['startDate'])?.value;
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
      const dateControl = this.updateForm?.controls[controlKey];
      const timeControl = this.updateForm?.controls[timeControlKey];
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
