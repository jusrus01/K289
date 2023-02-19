import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { catchError } from 'rxjs/operators';
import { RoutingService } from '../services/routing.service';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { ErrorSnackComponent } from '../common/snacks/error/error.component';

@Injectable()
export class HttpClientInterceptor implements HttpInterceptor {
  private readonly UNAUTHORIZED = 401;
  private readonly FORBIDDEN = 403;
  private readonly METHOD_NOT_ALLOWED = 405;

  constructor(
    private authService: AuthService,
    private routing: RoutingService,
    private snackBar: MatSnackBar
  ) {}

  private handleAuthError(err: HttpErrorResponse): Observable<any> {
    const status = err.status;

    if (status == this.UNAUTHORIZED ||
        status == this.FORBIDDEN ||
        status == this.METHOD_NOT_ALLOWED) {
            this.authService.logOut();
            this.routing.goToLogin();
            return of(err.message);
        }

        this.snackBar.openFromComponent(ErrorSnackComponent, {
            duration: 1500,
            data: err.error.ErrorMessage
        } as MatSnackBarConfig);
        return of(err.message);
  }

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next
      .handle(request.clone({withCredentials: true}))
      .pipe(catchError((err) => this.handleAuthError(err)));
  }
}
