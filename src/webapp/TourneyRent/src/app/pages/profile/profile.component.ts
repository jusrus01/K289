import { ViewChild } from '@angular/core';
import { Component } from '@angular/core';
import { ImageComponent } from 'src/app/common/image/image.component';
import { Profile } from 'src/app/models/profiles/profile.model';
import { ProfileResource } from 'src/app/resources/profile.resource';
import { AuthService } from 'src/app/services/auth.service';

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

  constructor(
    private profileResource: ProfileResource,
    private authService: AuthService
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
        this.profileImage.reload();
      });
  }
}
