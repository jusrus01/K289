import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { ErrorSnackComponent } from 'src/app/common/snacks/error/error.snack';
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

  private _tournamentId : any;
  public tournament : any;

  

  constructor(
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private tournamentResource: TournamentResource,
    private routing: RoutingService,
    private snackBar: MatSnackBar
  ) {
     this.updateForm = this.formBuilder.group({
       name: ['', Validators.required],
       startDate: [new Date(), Validators.required],
       endDate: [ new Date(), Validators.required],
       entryFee: [ 1, [Validators.required, Validators.min(0), Validators.max(1000), Validators.pattern(/^[0-9]+(\.[0-9]+)?$/)]],
       participantCount: [ 1, [Validators.required, Validators.min(1), Validators.max(1000), Validators.pattern(/^[0-9]*$/)]],
       bankAccountName: ['', Validators.required],
       bankAccountNumber: ['', Validators.required],
       transactionReason: ['']
     });
  }

  ngOnInit(){
    this.route.paramMap.subscribe((paramMap) => {
      this._tournamentId = paramMap.get('id');
      this.tournamentResource.getTournament(this._tournamentId).subscribe((response) => {
        this.tournament = response;
        this.tournament.startDate = new Date(this.tournament.startDate);
        this.tournament.endDate = new Date(this.tournament.endDate);
        console.log(this.tournament)
        this.updateForm.patchValue(this.tournament);
      });
      
    });

  }
  
  public update(): void {
    if (!this.updateForm.valid) {
      return;
    }

    const startDate = this.updateForm.get(['startDate'])?.value as Date;
    const endDate =  this.updateForm.get(['endDate'])?.value as Date;
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
    Object.keys(this.updateForm.controls).forEach(key => {
      let val =  this.updateForm.get([key])?.value;
      if (val instanceof Date) {
        val = val.toUTCString();
      }
      formData.append(key, val);
    });

    formData.append('imageFile', this.pictureFile ?? null);

    this.tournamentResource
      .updateTournament(formData,this._tournamentId)
      .subscribe(() => this.routing.goToTournaments());
  }

  onEntryFeeChange(): void {
    const entryFee = this.updateForm.get(['entryFee'])?.value;
    this.showBankAccountForm = entryFee > 0;

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
}