import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { RoutingService } from 'src/app/services/routing.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  constructor(
    private routing: RoutingService,
    private authService: AuthService) {
  }
  // TODO: Subscribe to something. probably need an event.
  public showMenu = false;

  public toggleNavbar(): void {
    this.showMenu = !this.showMenu;
  }

  public goToHome(): void {
    this.routing.goToHome();
  }

  public goToRegister(): void {
    this.routing.goToRegister();
  }

  public goToLogin(): void {
    this.routing.goToLogin();
  }

  public logout(): void {
    this.authService.logout();
    this.routing.goToLogin();
  }
}
