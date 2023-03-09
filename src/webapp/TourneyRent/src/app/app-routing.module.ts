import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guard/auth.guard';
import { GuestGuard } from './guard/guest.guard';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { RegisterComponent } from './pages/register/register.component';
import { TeamCreateComponent } from './pages/team-create/team-create/team-create.component';

export const ROUTES: Routes = [
  { path: 'profile', component: ProfileComponent, data: { title: 'Profile' }, canActivate: [AuthGuard]},
  { path: 'login', component: LoginComponent, data: { title: 'Login' }, canActivate: [GuestGuard] },
  { path: 'register', component: RegisterComponent, data: { title: 'Register' }, canActivate: [GuestGuard] },
  { path: 'team/create', component: TeamCreateComponent, data: {title: 'Team Create'}},
  { path: '', component: HomeComponent, data: { title: 'Home' }, canActivate: [AuthGuard] },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(ROUTES)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
