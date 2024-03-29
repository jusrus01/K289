import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Subject } from 'rxjs';
import { ADMINISTRATOR_ROLE } from '../constants';
import { LoginResponse } from '../models/login/login-response.model';
import { RentalCartService } from './rental-cart.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly TOKEN_COOKIE_PARAM = 'auth';

  public readonly isLoggedIn$: Subject<boolean>;

  constructor(
    private cookieService: CookieService,
    private rentalCartService: RentalCartService
  ) {
    this.isLoggedIn$ = new Subject<boolean>();
  }

  public login(loginResponse: LoginResponse): void {
    this.cookieService.set(
      this.TOKEN_COOKIE_PARAM,
      JSON.stringify(loginResponse)
    );
    this.rentalCartService.reset();
    this.isLoggedIn$.next(true);
  }

  public logout(): void {
    this.cookieService.delete(this.TOKEN_COOKIE_PARAM);
    this.rentalCartService.reset();

    setTimeout(() => {
      this.isLoggedIn$.next(false);
      this.cookieService.delete(this.TOKEN_COOKIE_PARAM);
    }, 300);

    setTimeout(() => {
      this.cookieService.delete(this.TOKEN_COOKIE_PARAM);
    }, 1000);
  }

  public getAuthUserId(): string {
    const userInfo = this.getUserInfo();
    return userInfo?.userId ?? '';
  }

  public isLoggedIn(): boolean {
    return this.cookieService.check(this.TOKEN_COOKIE_PARAM);
  }

  public hasRole(role: string): boolean {
    const userInfo = this.getUserInfo();
    return userInfo === null || userInfo.roles.includes(role);
  }

  public isAdminOrOwner(userId: any): boolean {
    return (
      (this.hasRole(ADMINISTRATOR_ROLE) || this.getAuthUserId() == userId) &&
      this.isLoggedIn()
    );
  }

  public isOwner(userId: any): boolean {
    return this.getAuthUserId() == userId;
  }

  private getUserInfo(): LoginResponse | null {
    try {
      const parsedUserInfo = JSON.parse(
        this.cookieService.get(this.TOKEN_COOKIE_PARAM)
      );
      return parsedUserInfo as LoginResponse;
    } catch {
      return null;
    }
  }
}
