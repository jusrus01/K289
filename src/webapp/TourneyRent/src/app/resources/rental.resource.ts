import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../app.module';

@Injectable({
  providedIn: 'root',
})
export class RentalResource {
  constructor(private httpClient: HttpClient) {}

  public getAvailableDays(rentalId: any): Observable<Date[]> {
    return this.httpClient.get<Date[]>(`${API_URL}/rentalitem/${rentalId}/availableDays`);
  }
}
