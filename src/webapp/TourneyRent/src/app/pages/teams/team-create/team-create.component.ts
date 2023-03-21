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
    const url = `${API_URL}/Account/Search/${search}`;
    this.http.get(url).subscribe((result: any) => {
      this.availableMembers = result.map((user: any) => ({
        id: user.id,
        name: `${user.firstName} ${user.lastName}`,
      }));
    });
  }

  public createTeam(): void {
    if (!this.createForm.valid) {
      return;
    }
  
    const teamData = {
      Name: this.createForm.get('Name')?.value,
      Description: this.createForm.get('Description')?.value,
      Members: this.createForm.get('Members')?.value
    };
  
    this.teamResource.createTeam(teamData)
      .subscribe(() => this.routing.goToTeams());
  }

  public compareFn(a: any, b: any): boolean {
    return a && b && a === b;
  }
  
}
