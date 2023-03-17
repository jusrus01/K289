import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { TeamResource } from 'src/app/resources/team.resource';
import { RoutingService } from 'src/app/services/routing.service';

@Component({
  selector: 'app-team-create',
  templateUrl: './team-create.component.html',
  styleUrls: ['./team-create.component.scss']
})
export class TeamCreateComponent {

  public createForm: FormGroup;
  public members: string[] = ['Titas Bartulis', 'Orestas Jonusas', 'Matas Onaitis', 'Justinas Ruslys', 'Tadas Jutkus']; // Add mocked data here

  constructor(
    private formBuilder: FormBuilder,
    private teamResource: TeamResource,
    private routing: RoutingService
  ) {
    this.createForm = this.formBuilder.group({
      Name: ['', Validators.required],
      Description: ['', Validators.required],
      Members: [[]]
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
}