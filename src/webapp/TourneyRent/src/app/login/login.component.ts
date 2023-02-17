import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { catchError, EMPTY } from 'rxjs';
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
    private router: Router,
    private cookieService: CookieService
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
      // TODO: Handle differently
      console.error('Invalid input');
      return;
    }

    this.resource
      .login(this.loginForm.value)
      .pipe(
        catchError((error) => {
          console.error(error);
          return EMPTY;
        })
      )
      .subscribe((response) => {
        this.cookieService.set('hasToken', 'true');
        this.router.navigate(['/home']);
      });
  }
}
