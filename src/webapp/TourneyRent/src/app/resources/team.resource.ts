import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../app.module';

@Injectable({
  providedIn: 'root',
})
export class TeamResource {
  constructor(private httpClient: HttpClient) {}

  public createTeam(data: any): Observable<any> {
    return this.httpClient.post(`${API_URL}/Team`, data);
  }

  public getAllTeams(): Observable<any> {
    return this.httpClient.get(`${API_URL}/Team`);
  }

  public getTeamMembers(teamId: any): Observable<any> {
    return this.httpClient.get(`${API_URL}/Team/${teamId}/members`);
  }

  public getUserTeams(userId: any): Observable<any> {
    return this.httpClient.get(`${API_URL}/Team/Members/${userId}`);
  }

  public addTeamMember(teamId: any, teamMemberCreate: any): Observable<any> {
    return this.httpClient.post(`${API_URL}/Team/${teamId}/members`, teamMemberCreate);
  }
}
