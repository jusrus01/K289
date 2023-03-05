import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guard/auth.guard';
import { GuestGuard } from './guard/guest.guard';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { RentalCreateComponent } from './pages/rental-create/rental-create.component';
import { RentalViewComponent } from './pages/rental-view/rental-view.component';

export const ROUTES: Routes = [
  { path: 'login', component: LoginComponent, data: { title: 'Login' }, canActivate: [GuestGuard] },
  { path: 'register', component: RegisterComponent, data: { title: 'Register' }, canActivate: [GuestGuard] },
  { path: 'rental-create', component: RentalCreateComponent, data: { title: 'Rental Create' } },
  { path: 'rental-view', component: RentalViewComponent, data: { title: 'Rental View' } },
  { path: '', component: HomeComponent, data: { title: 'Home' }, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(ROUTES)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
