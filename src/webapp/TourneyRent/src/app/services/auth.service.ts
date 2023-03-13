import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Subject } from 'rxjs';
import { ADMINISTRATOR_ROLE } from '../constants';
import { LoginResponse } from '../models/login/login-response.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly TOKEN_COOKIE_PARAM = 'auth';

  public readonly isLoggedIn$: Subject<boolean>;

  constructor(private cookieService: CookieService) {
    this.isLoggedIn$ = new Subject<boolean>();
  }

  public login(loginResponse: LoginResponse): void {
    this.cookieService.set(
      this.TOKEN_COOKIE_PARAM,
      JSON.stringify(loginResponse)
    );
    this.isLoggedIn$.next(true);
  }

  public logout(): void {
    this.cookieService.delete(this.TOKEN_COOKIE_PARAM);
    this.isLoggedIn$.next(false);
  }

  public getAuthUserId(): string {
    const userInfo = this.getUserInfo();
    return userInfo.userId;
  }

  public isLoggedIn(): boolean {
    return this.cookieService.check(this.TOKEN_COOKIE_PARAM);
  }

  public hasRole(role: string): boolean {
    const userInfo = this.getUserInfo();
    return userInfo.roles.includes(role);
  }

  public isAdminOrOwner(userId: any): boolean {
    return this.hasRole(ADMINISTRATOR_ROLE) || this.getAuthUserId() == userId;
  }

  private getUserInfo(): LoginResponse {
    return JSON.parse(
      this.cookieService.get(this.TOKEN_COOKIE_PARAM)
    ) as LoginResponse;
  }
}
