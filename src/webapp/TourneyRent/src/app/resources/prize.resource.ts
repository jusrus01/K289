import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../app.module';

@Injectable({
  providedIn: 'root',
})
export class PrizeResource {
  constructor(private httpClient: HttpClient) {}

  public getAvailablePrizes(): Observable<any> {
    return this.httpClient.get(`${API_URL}/prize`);
  }
}
