import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { NavbarComponent } from './common/navbar/navbar.component';
import { HomeComponent } from './pages/home/home.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { A11yModule } from '@angular/cdk/a11y';
import { CdkAccordionModule } from '@angular/cdk/accordion';
import { ClipboardModule } from '@angular/cdk/clipboard';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { CdkListboxModule } from '@angular/cdk/listbox';
import { PortalModule } from '@angular/cdk/portal';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { CdkStepperModule } from '@angular/cdk/stepper';
import { CdkTableModule } from '@angular/cdk/table';
import { CdkTreeModule } from '@angular/cdk/tree';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatBadgeModule } from '@angular/material/badge';
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatStepperModule } from '@angular/material/stepper';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatNativeDateModule, MatRippleModule } from '@angular/material/core';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatSliderModule } from '@angular/material/slider';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatTreeModule } from '@angular/material/tree';
import { OverlayModule } from '@angular/cdk/overlay';
import { CdkMenuModule } from '@angular/cdk/menu';
import { DialogModule } from '@angular/cdk/dialog';
import { HttpClientInterceptor } from './interceptors/http.interceptor';
import { AuthService } from './services/auth.service';
import { RoutingService } from './services/routing.service';
import { RentalCreateComponent } from './pages/rentals/rental-create/rental-create.component';
import { ShowForAuthenticatedUserDirective } from './common/directives/show-for-authenticated-user.directive';
import { ShowForGuestUserDirective } from './common/directives/show-for-guest-user.directive';
import { ProfileComponent } from './pages/profile/profile.component';
import { ImageComponent } from './common/image/image.component';
import { CommonModule } from '@angular/common';
import { TournamentCreateComponent } from './pages/tournaments/tournament-create/tournament-create.component';
import { TournamentListComponent } from './pages/tournaments/tournament-list/tournament-list.component';
import { TournamentComponent } from './pages/tournaments/tournament/tournament.component';
import {
  NgxMatDatetimePickerModule,
  NgxMatNativeDateModule,
  NgxMatTimepickerModule,
} from '@angular-material-components/datetime-picker';
import {
  ConfirmDeleteDialogTemp,
  TournamentItemComponent,
} from './pages/tournaments/tournament-item/tournament-item.component';
import { ShowForAdminUserDirective } from './common/directives/show-for-admin-user.directive';
import { RentalComponent } from './pages/rentals/rental/rental.component';
import { RentalDetailsComponent } from './pages/rentals/rental-details/rental-details.component';
import { RentalEditComponent } from './pages/rentals/rental-edit/rental-edit.component';
import { TeamComponent } from './pages/teams/team/team.component';
import { TeamCreateComponent } from './pages/teams/team-create/team-create.component';
import { TeamListComponent } from './pages/teams/team-list/team-list.component';
import { TeamItemComponent } from './pages/teams/team-item/team-item.component';
import { PayProcessingDialog } from './common/dialogs/pay-processing/pay-processing.dialog';
import { ChooseTeamDialog } from './common/dialogs/choose-team/choose-team.dialog';
import { LeaveTournamentDialog } from './common/dialogs/leave-tournament/leave-tournament.dialog';
import { TournamentUpdateComponent } from './pages/tournaments/tournament-update/tournament-update.component';
import { SelectPrizeDialog } from './common/dialogs/select-prize/select-prize.dialog';
import { ChooseWinnerDialog } from './common/dialogs/choose-winner/choose-winner.dialog';
import { RentalCartComponent } from './common/rental-cart/rental-cart.component';
import { RentalCartAddButtonComponent } from './common/rental-cart/rental-cart-add-button/rental-cart-add-button.component';
import { AvailableDaysComponent } from './common/dialogs/available-days/available-days.component';
import { EditProfileComponent } from './common/dialogs/edit-profile/edit-profile.component';
import { RentalCartItemComponent } from './common/rental-cart/rental-item/rental-cart-item/rental-cart-item.component';

export const API_URL = 'http://localhost:5155';

@NgModule({
  declarations: [
    TeamCreateComponent,
    AppComponent,
    LoginComponent,
    RegisterComponent,
    NavbarComponent,
    HomeComponent,
    RentalCreateComponent,
    ShowForAuthenticatedUserDirective,
    ShowForGuestUserDirective,
    ShowForAdminUserDirective,
    ProfileComponent,
    ImageComponent,
    TournamentCreateComponent,
    TournamentListComponent,
    TournamentComponent,
    TournamentItemComponent,
    ConfirmDeleteDialogTemp,
    RentalComponent,
    RentalDetailsComponent,
    RentalEditComponent,
    TeamComponent,
    TeamListComponent,
    TeamItemComponent,
    PayProcessingDialog,
    ChooseTeamDialog,
    LeaveTournamentDialog,
    TournamentUpdateComponent,
    SelectPrizeDialog,
    ChooseWinnerDialog,
    RentalCartComponent,
    RentalCartAddButtonComponent,
    AvailableDaysComponent,
    EditProfileComponent,
    RentalCartItemComponent,
  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatCardModule,
    MatIconModule,
    MatFormFieldModule,
    A11yModule,
    CdkAccordionModule,
    ClipboardModule,
    CdkListboxModule,
    CdkMenuModule,
    CdkStepperModule,
    CdkTableModule,
    CdkTreeModule,
    DragDropModule,
    MatAutocompleteModule,
    MatBadgeModule,
    MatBottomSheetModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatStepperModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    MatTreeModule,
    OverlayModule,
    PortalModule,
    ScrollingModule,
    DialogModule,
    NgxMatDatetimePickerModule,
    NgxMatTimepickerModule,
    NgxMatNativeDateModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useFactory: function (
        authService: AuthService,
        router: RoutingService,
        snack: MatSnackBar
      ) {
        return new HttpClientInterceptor(authService, router, snack);
      },
      multi: true,
      deps: [AuthService, RoutingService, MatSnackBar],
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
