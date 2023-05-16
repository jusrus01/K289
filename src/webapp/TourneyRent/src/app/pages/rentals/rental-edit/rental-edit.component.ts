import { Component, OnInit } from '@angular/core';
import { RentalResource } from 'src/app/resources/rental.resource';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { RoutingService } from 'src/app/services/routing.service';

@Component({
  selector: 'app-rental-edit',
  templateUrl: './rental-edit.component.html',
  styleUrls: ['./rental-edit.component.scss'],
})
export class RentalEditComponent implements OnInit {
  public updateForm!: FormGroup;
  public showBankAccountForm = true;

  public item!: any;

  itemId = Number(this.route.snapshot.paramMap.get('itemId'));

  constructor(
    private route: ActivatedRoute,
    private itemService: RentalResource,
    public routing: RoutingService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.itemId = id;
    this.updateForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
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

    this.itemService.getItemById(id).subscribe((item) => {
      this.item = item;

      this.updateForm.patchValue(this.item);
    });
  }

  onEntryFeeChange(): void {
    const price = this.updateForm.get(['price'])?.value;

    this.showBankAccountForm = price > 0;

    // const controlNames = ['bankAccountName', 'bankAccountNumber'];
    // for (const controlName of controlNames) {
    //   const control = this.updateForm.get(controlName);
    //   if (this.showBankAccountForm) {
    //     control?.setValidators([Validators.required]);
    //   } else {
    //     control?.clearValidators();
    //   }
    //   control?.updateValueAndValidity();
    // }
  }

  submit(): void {
    if (!this.updateForm.valid) {
      return;
    }

    const formData = new FormData();
    Object.keys(this.updateForm.controls).forEach((key) => {
      const val = this.updateForm.get([key])?.value;
      formData.append(key, val);
    });

    this.itemService.updateItem(this.itemId, formData).subscribe((_) => {
      this.routing.goToRental();
    });
  }
}
