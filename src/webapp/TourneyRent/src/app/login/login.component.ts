import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { RoutingService } from '../services/routing.service';
import { LoginResource } from './login.resource';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  public hidePassword: boolean;
  public loginForm: FormGroup;

  constructor(
    private resource: LoginResource,
    private formBuilder: FormBuilder,
    private routing: RoutingService,
    private authService: AuthService,
  ) {
    this.hidePassword = true;
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  ngOnInit() {}

  public login(): void {
    if (!this.loginForm.valid) {
      return;
    }

    this.resource
      .login(this.loginForm.value)
      .subscribe(() => {
        this.authService.logIn();
        this.routing.goToHome();
      });
  }

  public redirectToSignUp(): void {
    this.routing.goToRegister();
  }
}
