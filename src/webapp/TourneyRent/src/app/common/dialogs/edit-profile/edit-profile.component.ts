import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ProfileResource } from 'src/app/resources/profile.resource';
import { MatDialogRef } from '@angular/material/dialog';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss']
})
export class EditProfileComponent implements OnInit {
  editProfileForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar,
    private dialogRef: MatDialogRef<EditProfileComponent>,
    private profileResource: ProfileResource,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    this.editProfileForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });
  }

  onSave(): void {
  
    if (this.editProfileForm.valid) {
      const userId = this.authService.getAuthUserId();
      
      const formData = this.editProfileForm.value;
      formData.Id = userId;
  
      this.profileResource.updateProfile(formData).subscribe(
        () => {
          this.snackBar.open('Profile updated successfully', 'Close', {
            duration: 2000,
          });
          
          this.dialogRef.close(); // Close the dialog
        },
        (error) => {
          if (error && error.error) {
            this.snackBar.open(`Error updating profile: ${error.error}`, 'Close', {
              duration: 2000,              
            });
            console.log('Error type 1:', error.error);
          } else if (error) {
            this.snackBar.open(`Error updating profile: ${error}`, 'Close', {
              duration: 2000,
            });
            console.log('Error type 2:', error);
          } else {
            this.snackBar.open('Unknown error occurred while updating profile', 'Close', {
              duration: 2000,
            });
            console.log('Error type 3');
          }
        }
      );
    } else {
      this.snackBar.open('Please fill in all required fields', 'Close', {
        duration: 2000,
      });
    }
  }
  

  onCancel(): void {
    this.dialogRef.close(); // Close the dialog
  } 
}
