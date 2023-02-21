import { Inject, Injectable, Type } from '@angular/core';
import { Route, Router, ROUTES } from '@angular/router';
import { HomeComponent } from '../pages/home/home.component';
import { LoginComponent } from '../pages/login/login.component';
import { RegisterComponent } from '../pages/register/register.component';

@Injectable({
  providedIn: 'root'
})
export class RoutingService {
  constructor(@Inject(ROUTES) private _routes: Route[][], private _router: Router) {}
  
  public goToLogin(): void {
    this.navigateToComponent(LoginComponent);
  }

  public goToRegister(): void {
    this.navigateToComponent(RegisterComponent);
  }

  public goToHome(): void {
    this.navigateToComponent(HomeComponent);
  }

  private navigateToComponent(component: Type<any>): void {
    this._router.navigate([this.getRouteFromComponent(component)]);
  }

  private getRouteFromComponent(component: Type<any>): string {
    for (const routeArray of this._routes) {
        const path = routeArray.find(route => route.component == component)?.path;
        if (path || path === '') {
            return path;            
        }
    }

    throw new Error('Invalid component or path not registered');
  }
}