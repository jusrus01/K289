import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guard/auth.guard';
import { GuestGuard } from './guard/guest.guard';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { RegisterComponent } from './pages/register/register.component';
import { TeamCreateComponent } from './pages/teams/team-create/team-create.component';
import { RentalCreateComponent } from './pages/rental-create/rental-create.component';
import { RentalViewComponent } from './pages/rental-view/rental-view.component';
import { TournamentCreateComponent } from './pages/tournaments/tournament-create/tournament-create.component';
import { TournamentUpdateComponent } from './pages/tournaments/tournament-update/tournament-update.component';
import { TournamentItemComponent } from './pages/tournaments/tournament-item/tournament-item.component';
import { TournamentComponent } from './pages/tournaments/tournament/tournament.component';
import { TeamComponent } from './pages/teams/team/team.component';

export const ROUTES: Routes = [
  { path: 'tournament/create', component: TournamentCreateComponent, data: { title: 'Create' }, canActivate: [AuthGuard]},
  { path: 'tournament/:id', component: TournamentItemComponent, data: { title: 'Tournament'} },
  { path: 'tournament/update/:id', component: TournamentUpdateComponent, data: { title: 'Tournament Update'}, canActivate: [AuthGuard]},
  { path: 'tournament', component: TournamentComponent, data: { title: 'Tournaments' } },
  { path: 'profile', component: ProfileComponent, data: { title: 'Profile' }, canActivate: [AuthGuard]},
  { path: 'login', component: LoginComponent, data: { title: 'Login' }, canActivate: [GuestGuard] },
  { path: 'register', component: RegisterComponent, data: { title: 'Register' }, canActivate: [GuestGuard] },
  { path: 'team/create', component: TeamCreateComponent, data: {title: 'Team Create'}, canActivate: [AuthGuard]},
  { path: 'team', component: TeamComponent, data: { title: 'Teams' } },
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
