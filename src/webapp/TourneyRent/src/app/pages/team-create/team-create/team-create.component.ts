import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'app-team-create',
  templateUrl: './team-create.component.html',
  styleUrls: ['./team-create.component.scss']
})
export class TeamCreateComponent {
  Name!: string;
  Description!: string;

  constructor(private http: HttpClient) {}

  createTeam() {
    const team = {
      Name: this.Name,
      Description: this.Description
    };

    this.http.post('http://localhost:5155/Team', team).subscribe(response => {
      console.log('Team created:', response);
    }, error => {
      console.error('Error creating team:', error);
    });
  }
}
