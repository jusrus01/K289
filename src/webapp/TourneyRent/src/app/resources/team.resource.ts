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

  public getUserTeams(userId: any): Observable<any> {
    return this.httpClient.get(`${API_URL}/Team/Members/${userId}`);
  }
}
