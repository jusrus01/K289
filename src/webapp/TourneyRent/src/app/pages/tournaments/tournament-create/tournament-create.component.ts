import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { TournamentResource } from 'src/app/resources/tournament.resource';
import { RoutingService } from 'src/app/services/routing.service';

@Component({
  selector: 'app-tournament-create',
  templateUrl: './tournament-create.component.html',
  styleUrls: ['./tournament-create.component.scss'],
})
export class TournamentCreateComponent {
  public pictureSource!: string | ArrayBuffer | null;
  public pictureFile!: any;
  public createForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private tournamentResource: TournamentResource,
    private routing: RoutingService,
  ) {
    this.createForm = this.formBuilder.group({
      name: ['', Validators.required],
      startDate: [new Date(), Validators.required],
      endDate: [new Date(), Validators.required],
      entryFee: [1, [Validators.required, Validators.min(0), Validators.max(1000), Validators.pattern(/^[0-9]+(\.[0-9]+)?$/)]],
      participantCount: [1, [Validators.required, Validators.min(1), Validators.max(1000), Validators.pattern(/^[0-9]*$/)]],
    });
  }

  public create(): void {
    if (!this.createForm.valid) {
      return;
    }

    const startDate = this.createForm.get(['startDate'])?.value as Date;
    const endDate =  this.createForm.get(['endDate'])?.value as Date;
    if (startDate >= endDate && startDate.getTime() >= endDate.getTime()) {
      return;
    }

    if (startDate < new Date()) {
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

    this.tournamentResource
      .createTournament(formData)
      .subscribe(() => this.routing.goToTournaments());
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