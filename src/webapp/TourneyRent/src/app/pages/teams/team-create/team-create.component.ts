import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { TeamResource } from 'src/app/resources/team.resource';
import { RoutingService } from 'src/app/services/routing.service';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '../../../app.module';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-team-create',
  templateUrl: './team-create.component.html',
  styleUrls: ['./team-create.component.scss']
})
export class TeamCreateComponent {

  public createForm: FormGroup;
  public members: string[] = [];
  public availableMembers: any[] = [];

  constructor(
    private formBuilder: FormBuilder,
    private teamResource: TeamResource,
    private routing: RoutingService,
    private authService: AuthService,
    private http: HttpClient
  ) {
    this.createForm = this.formBuilder.group({
      Name: ['', Validators.required],
      Description: ['', Validators.required],
      Members: [[]]
    });

    this.getUsers('');
  }

  public getUsers(search: string): void {
    const url = `${API_URL}/Account/Search`;
    this.http.get(url).subscribe((result: any) => {
      this.availableMembers = result
        .filter((user: any) => user.id !== this.authService.getAuthUserId())
        .map((user: any) => ({
          id: user.id,
          name: `${user.firstName} ${user.lastName}`
        }));
    });
  }  

  public createTeam(): void {
    if (!this.createForm.valid) {
      return;
    }
  
    let teamData = {
      Name: this.createForm.get('Name')?.value,
      Description: this.createForm.get('Description')?.value,
      ID: 0
    };

    let membersData = {
      Members: this.createForm.get('Members')?.value
    }

    this.teamResource.createTeam(teamData).subscribe(
      (response: any) => {
        teamData.ID = response.id;
        for (let i = 0; i < membersData.Members.length; i++) {         
           let teamMemberCreate = {
              TeamId: teamData.ID,
              UserId: membersData.Members[i].id,
              Role: 'Member'
            }
            this.teamResource.addTeamMember(teamData.ID, teamMemberCreate).subscribe(() => this.routing.goToTeams()); 
        }
      },
      (error: any) => {
        console.log(error);
      }
    ); 
  }

  public compareFn(a: any, b: any): boolean {
    return a && b && a === b;
  }
  
}
