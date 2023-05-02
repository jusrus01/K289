import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { API_URL } from '../app.module';

@Injectable({
  providedIn: 'root'
})
export class RentalResource {
  constructor(private http: HttpClient) { }

  getItems(): Observable<any[]> {
    return this.http.get<any[]>(`${API_URL}/rentalItem`);
  }

  getItemById(itemId: number): Observable<any> {
    const url = `${API_URL}/rentalItem/${itemId}`;
    return this.http.get<any>(url);
  }

  createItem(item: any): Observable<any> {
    return this.http.post<any>(API_URL, item);
  }

  updateItem(itemId: number, item: any): Observable<any> {
    const url = `${API_URL}/rentalItem/${itemId}`;
    return this.http.put<any>(url, item);
  }

  deleteItem(itemId: number): Observable<any> {
    const url = `${API_URL}/rentalItem/${itemId}`;
    return this.http.delete(url);
  }

  public getAvailableDays(rentalId: any): Observable<Date[]> {
    const url = `${API_URL}/rentalItem/${rentalId}/availableDays`;
    return this.http.get<Date[]>(url);
  }
}
