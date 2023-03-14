import { Inject, Injectable, Type } from '@angular/core';
import { Route, Router, ROUTES } from '@angular/router';
import { HomeComponent } from '../pages/home/home.component';
import { LoginComponent } from '../pages/login/login.component';
import { ProfileComponent } from '../pages/profile/profile.component';
import { RegisterComponent } from '../pages/register/register.component';
import { TeamCreateComponent } from '../pages/teams/team-create/team-create.component';
import { TeamComponent } from '../pages/teams/team/team.component';
import { TournamentCreateComponent } from '../pages/tournaments/tournament-create/tournament-create.component';
import { TournamentComponent } from '../pages/tournaments/tournament/tournament.component';

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

  public goToTeamsCreate(): void {
    this.navigateToComponent(TeamCreateComponent);
  }

  public goToTeams(): void {
    this.navigateToComponent(TeamComponent);
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