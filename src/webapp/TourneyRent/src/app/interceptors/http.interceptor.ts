import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
  HttpResponse,
} from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { catchError, tap } from 'rxjs/operators';
import { RoutingService } from '../services/routing.service';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { ErrorSnackComponent } from '../common/snacks/error/error.snack';
import { SuccessSnackComponent } from '../common/snacks/error/success.snack';

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

    if (
      status == this.UNAUTHORIZED ||
      status == this.FORBIDDEN ||
      status == this.METHOD_NOT_ALLOWED
    ) {
      this.authService.logout();
      this.routing.goToLogin();
      return of(err.message);
    }

    this.snackBar.openFromComponent(ErrorSnackComponent, {
      duration: 1500,
      data: err.error.ErrorMessage,
      panelClass: ['blue-snackbar-error'],
    } as MatSnackBarConfig);
    return of(err.message);
  }

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (request.method === 'OPTIONS') {
      return next
        .handle(request.clone({ withCredentials: true }))
        .pipe(catchError((err) => this.handleAuthError(err)));
    }

    return next.handle(request.clone({ withCredentials: true })).pipe(
      tap((event) => {
        if (
          event instanceof HttpResponse &&
          (request.method === 'POST' || request.method === 'PUT')
        ) {
          this.snackBar.openFromComponent(SuccessSnackComponent, {
            duration: 1000,
            panelClass: ['blue-snackbar'],
          });
        }
      }),
      catchError((err) => this.handleAuthError(err))
    );
  }
}
