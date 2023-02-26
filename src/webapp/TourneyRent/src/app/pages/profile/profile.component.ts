import { Component } from '@angular/core';
import { Profile } from 'src/app/models/profiles/profile.model';
import { ProfileResource } from 'src/app/resources/profile.resource';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent {
  public profile!: Profile;
  public isLoading: boolean;

  constructor(
    private profileResource: ProfileResource,
    private authService: AuthService
  ) {
    this.isLoading = true;
  }

  ngOnInit() {
    this.profileResource
      .getProfile(this.authService.getAuthUserId())
      .subscribe((profile: Profile) => {
        this.profile = profile;
        this.isLoading = false;
      });
  }
}
