import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { catchError, EMPTY } from 'rxjs';
import { RegisterResource } from './register.resource';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
  public hidePassword: boolean;
  public registerForm: FormGroup;

  constructor(
    private resource: RegisterResource,
    private formBuilder: FormBuilder,
    private router: Router
  ) {
    this.hidePassword = true;
    this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  ngOnInit() {}

  public register(): void {
    if (!this.registerForm.valid) {
      // TODO: Handle differently
      console.error('Invalid input');
      return;
    }

    this.resource
      .register(this.registerForm.value)
      .pipe(
        catchError((error) => {
          console.error(error);
          return EMPTY;
        })
      )
      .subscribe((response) => this.router.navigate(['/login']));
  }
}
