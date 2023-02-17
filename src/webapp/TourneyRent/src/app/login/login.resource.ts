import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../app.module';

@Injectable({
  providedIn: 'root',
})
export class LoginResource {
  constructor(private httpClient: HttpClient) {}

  login(data: any): Observable<any> {
    return this.httpClient.post(
      `${API_URL}/account/login`,
      {
        email: data.email,
        password: data.password,
      },
      { observe: 'response', withCredentials: true }, // withCredentials is required to send cookie
    );
  }
}
