import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../app.module';

@Injectable({
  providedIn: 'root',
})
export class TournamentResource {
  constructor(private httpClient: HttpClient) {}

  public createTournament(data: any): Observable<any> {
    return this.httpClient.post(`${API_URL}/tournament/create`, data);
  }
}
