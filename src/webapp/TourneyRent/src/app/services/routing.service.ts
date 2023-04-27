import { Inject, Injectable, Type } from '@angular/core';
=
  public goToRentalView(): void {
    this.navigateToComponent(RentalViewComponent);
  }

  public goToTournamentCreate(): void {
    this.navigateToComponent(TournamentCreateComponent);
  }

  public goToTournamentUpdate(): void {
    this.navigateToComponent(TournamentUpdateComponent);
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