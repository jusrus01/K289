import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../app.module';
import { LoginResponse } from '../models/login/login-response.model';

@Injectable({
  providedIn: 'root',
})
export class LoginResource {
  constructor(private httpClient: HttpClient) {}

  login(data: any): Observable<any> {
    return this.httpClient.post<LoginResponse>(`${API_URL}/account/login`, {
      email: data.email,
      password: data.password,
    });
  }
}
