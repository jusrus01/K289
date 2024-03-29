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

  public updateTournament(data: any, id: any): Observable<any> {
    return this.httpClient.put(`${API_URL}/tournament/${id}`, data);
  }

  public joinTournament(id: any, data: any): Observable<any> {
    return this.httpClient.post(`${API_URL}/tournament/${id}/join`, data);
  }

  public leaveTournament(id: any): Observable<any> {
    return this.httpClient.post(`${API_URL}/tournament/${id}/Leave`, {});
  }

  public getTournaments(ownerId: any): Observable<any> {
    return this.httpClient.get(`${API_URL}/tournament/owner/${ownerId}`);
  }

  public getJoinedTournaments(userId: any): Observable<any> {
    return this.httpClient.get(`${API_URL}/tournament/user/${userId}`);
  }

  public selectWinner(tournamentId: any, winnerId: any) {
    return this.httpClient.post(
      `${API_URL}/tournament/${tournamentId}/winner/${winnerId}`,
      {}
    );
  }
}
