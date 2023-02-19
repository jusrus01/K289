import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly _TOKEN_COOKIE_PARAM = 'hasToken';

  constructor(private cookieService: CookieService) {}

  public logIn(): void {
    this.cookieService.set(this._TOKEN_COOKIE_PARAM, 'true');
  }

  public logOut(): void {
    this.cookieService.delete(this._TOKEN_COOKIE_PARAM);
  }

  public isLoggedIn(): boolean {
    return this.cookieService.check(this._TOKEN_COOKIE_PARAM);
  }
}