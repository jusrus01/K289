import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../app.module';

@Injectable({
  providedIn: 'root',
})
export class TournamentResource {
  constructor(private httpClient: HttpClient) {}

  public deleteTournament(id: any): Observable<any> {
    return this.httpClient.delete(`${API_URL}/tournament/${id}`);
  }

  public getTournament(id: any): Observable<any> {
    return this.httpClient.get(`${API_URL}/tournament/${id}`);
  }

  public getAllTournaments(): Observable<any> {
    return this.httpClient.get(`${API_URL}/tournament`);
  }

  public createTournament(data: any): Observable<any> {
    return this.httpClient.post(`${API_URL}/tournament`, data);
  }

  public joinTournament(id: any, data: any): Observable<any> {
    return this.httpClient.post(`${API_URL}/tournament/${id}/join`, data);
  }

  public leaveTournament(id: any): Observable<any> {
    return this.httpClient.post(`${API_URL}/tournament/${id}/Leave`, {});
  }
}
