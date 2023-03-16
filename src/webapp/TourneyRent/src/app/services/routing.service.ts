import { Inject, Injectable, Type } from '@angular/core';
import { Route, Router, ROUTES } from '@angular/router';
import { HomeComponent } from '../pages/home/home.component';
import { LoginComponent } from '../pages/login/login.component';
import { ProfileComponent } from '../pages/profile/profile.component';
import { RegisterComponent } from '../pages/register/register.component';
import { TournamentCreateComponent } from '../pages/tournaments/tournament-create/tournament-create.component';
import { TournamentComponent } from '../pages/tournaments/tournament/tournament.component';
import { RentalComponent } from '../pages/rentals/rental/rental.component';
import { RentalCreateComponent } from '../pages/rentals/rental-create/rental-create.component';

@Injectable({
  providedIn: 'root'
})
export class RoutingService {
  constructor(@Inject(ROUTES) private _routes: Route[][], private _router: Router) {}
  
  public goToTournamentCreate(): void {
    this.navigateToComponent(TournamentCreateComponent);
  }

  public goToTournaments(): void {
    this.navigateToComponent(TournamentComponent);
  }

  public goToRental(): void {
    this.navigateToComponent(RentalComponent);
  }

  public goToRentalCreate(): void {
    this.navigateToComponent(RentalCreateComponent);
  }

  public goToLogin(): void {
    this.navigateToComponent(LoginComponent);
  }

  public goToRegister(): void {
    this.navigateToComponent(RegisterComponent);
  }

  public goToHome(): void {
    this.navigateToComponent(HomeComponent);
  }

  public goToProfile(): void {
    this.navigateToComponent(ProfileComponent);
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