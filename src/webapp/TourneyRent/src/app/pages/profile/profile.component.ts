import { ViewChild } from '@angular/core';
import { Component } from '@angular/core';
import { ImageComponent } from 'src/app/common/image/image.component';
import { Profile } from 'src/app/models/profiles/profile.model';
import { ProfileResource } from 'src/app/resources/profile.resource';
import { TournamentResource } from 'src/app/resources/tournament.resource';
import { AuthService } from 'src/app/services/auth.service';
import { TournamentService } from 'src/app/services/tournament.service';
import { TeamResource } from 'src/app/resources/team.resource';
import { MatDialog } from '@angular/material/dialog';
import { EditProfileComponent } from 'src/app/common/dialogs/edit-profile/edit-profile.component';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
// TODO: Change to take into account other people's profile
// if the need arrises to allow viewing user details
export class ProfileComponent {
  @ViewChild('profileImage') profileImage!: ImageComponent;

  public uploadConfig: any;

  public profile!: Profile;
  public isLoading: boolean;
  public isUser: boolean;
  public isEditing: boolean;

  public pictureSource!: string | ArrayBuffer | null;
  public pictureFile!: any;

  public tournaments: any;
  public teams: any;

  public joinedTournaments: any;

  constructor(
    private profileResource: ProfileResource,
    private authService: AuthService,
    private tournamentResource: TournamentResource,
    private tournamentService: TournamentService,
    private teamResource: TeamResource,
    public dialog: MatDialog
  ) {
    this.isLoading = true;
    this.isUser = true; // TODO: Same
    this.isEditing = false;
  }

  ngOnInit() {
    this.profileResource
      .getProfile(this.authService.getAuthUserId())
      .subscribe((profile: Profile) => {
        this.profile = profile;
        this.isLoading = false;
      });

    this.tournamentResource
      .getTournaments(this.authService.getAuthUserId())
      .subscribe((tournaments) => {
        this.tournaments = tournaments.map((t: any) => ({
          ...t,
          status: this.tournamentService.getTournamentStatus(t),
        }));
      });

    this.tournamentResource
      .getJoinedTournaments(this.authService.getAuthUserId())
      .subscribe((tournaments) => {
        this.joinedTournaments = tournaments;
      });

    this.teamResource
      .getUserTeams(this.authService.getAuthUserId())
      .subscribe((teams) => {
        this.teams = teams;
      });
  }

  public onEdit(): void {
    const dialogRef = this.dialog.open(EditProfileComponent, {
      width: '400px',
    });
  }

  onFileUpload(event: any, upload: any): void {
    this.pictureFile = event.target.files[0];

    const reader = new FileReader();
    reader.onload = () => (this.pictureSource = reader.result);
    reader.readAsDataURL(this.pictureFile);
    upload.value = null;

    const formData = new FormData();
    formData.append('imageFile', this.pictureFile);

    this.profileResource
      .changeMyProfileImage(formData)
      .subscribe((resp: any) => {
        this.profile.imageId = resp.imageId;
        setTimeout(() => {
          this.profileImage.reload();
        });
      });
  }
}
