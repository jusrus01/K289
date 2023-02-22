import { Component } from '@angular/core';
import { RoutingService } from 'src/app/services/routing.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  constructor(private routing: RoutingService) {
  }

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
}
