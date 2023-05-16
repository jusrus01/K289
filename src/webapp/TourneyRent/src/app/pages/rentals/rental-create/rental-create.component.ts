import { RoutingService } from 'src/app/services/routing.service';
import { Component, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RentalResource } from 'src/app/resources/rental.resource';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-rental-create',
  templateUrl: './rental-create.component.html',
  styleUrls: ['./rental-create.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class RentalCreateComponent {
  public createForm!: FormGroup;
  public showBankAccountForm = true;

  public pictureSource!: string | ArrayBuffer | null;
  public pictureFile!: any;

  constructor(
    private rentalResource: RentalResource,
    private formBuilder: FormBuilder,
    public routing: RoutingService
  ) {}

  ngOnInit() {
    this.createForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      bankAccountName: ['', Validators.required],
      bankAccountNumber: ['', Validators.required],
      transactionReason: [''],
      price: [
        1,
        [
          Validators.required,
          Validators.min(1),
          Validators.max(1000),
          Validators.pattern(/^[0-9]+(\.[0-9]+)?$/),
        ],
      ],
    });
  }

  public create(): void {
    if (!this.createForm.valid) {
      return;
    }

    const formData = new FormData();
    Object.keys(this.createForm.controls).forEach((key) => {
      const val = this.createForm.get([key])?.value;
      formData.append(key, val);
    });
    formData.append('imageFile', this.pictureFile ?? null);
    formData.append('availableAt', JSON.stringify(this.daysSelected ?? []));

    this.rentalResource.createItem(formData).subscribe((_) => {
      this.routing.goToRental();
    });
  }

  onEntryFeeChange(): void {
    const price = this.createForm.get(['price'])?.value;

    this.showBankAccountForm = price > 0;

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

  daysSelected: any[] = [];
  // event: any;
  isSelected = (event: any) => {
    const date =
      event.getFullYear() +
      '-' +
      ('00' + (event.getMonth() + 1)).slice(-2) +
      '-' +
      ('00' + event.getDate()).slice(-2);
    //return this.daysSelected.find(x => x == date) ? "selected" : null;
    return this.daysSelected.find((x) => x == date) ? 'selected' : '';
  };

  select(event: any, calendar: any) {
    const date =
      event.getFullYear() +
      '-' +
      ('00' + (event.getMonth() + 1)).slice(-2) +
      '-' +
      ('00' + event.getDate()).slice(-2);
    const index = this.daysSelected.findIndex((x) => x == date);
    if (index < 0) this.daysSelected.push(date);
    else this.daysSelected.splice(index, 1);

    calendar.updateTodaysDate();
  }
}
