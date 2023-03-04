import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-team-create',
  templateUrl: './team-create.component.html',
  styleUrls: ['./team-create.component.scss']
})
export class TeamCreateComponent {
  teamName: string = '';
  teamDescription: string = '';

  constructor(private http: HttpClient) {}

  createTeam() {
    const team = {
      teamName: this.teamName,
      teamDescription: this.teamDescription
    };

    this.http.post('http://localhost:5155/Team', team).subscribe(response => {
      console.log('Team created:', response);
    }, error => {
      console.error('Error creating team:', error);
    });
  }
}
