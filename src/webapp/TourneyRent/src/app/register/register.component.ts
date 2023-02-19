import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RoutingService } from '../services/routing.service';
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
    private routing: RoutingService
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
      // TODO: Handle differently?
      console.error('Invalid input');
      return;
    }

    this.resource
      .register(this.registerForm.value)
      .subscribe(() => this.routing.goToLogin());
  }

  public redirectToLogin(): void {
    this.routing.goToLogin();
  }
}
