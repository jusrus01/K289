import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { TournamentResource } from 'src/app/resources/tournament.resource';
import { RoutingService } from 'src/app/services/routing.service';
import { MatSnackBar, } from '@angular/material/snack-bar';
import { SuccessSnackComponent } from 'src/app/common/snacks/error/success.snack';

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
    private snackBar: MatSnackBar
  ) {
    this.createForm = this.formBuilder.group({
      name: ['', Validators.required],
      startDate: [new Date(), Validators.required],
      startTime: [new Date(), Validators.required],
      endDate: [new Date(), Validators.required],
      endTime: [new Date(), Validators.required],
      entryFee: [1, [Validators.required, Validators.min(0), Validators.max(1000)]],
      participantCount: [1, [Validators.required, Validators.min(1), Validators.max(1000)]],
      pictureFile: null,
    });
  }

  public create(): void {
    if (!this.createForm.valid) {
      return;
    }

    this.tournamentResource
      .createTournament(this.createForm.value)
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