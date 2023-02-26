import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { API_URL } from '../app.module';
import { Profile } from '../models/profiles/profile.model';

@Injectable({
  providedIn: 'root',
})
export class ProfileResource {
  constructor(private httpClient: HttpClient) {}

  getProfile(userId: string): Observable<Profile> {
    return this.httpClient.get<Profile>(
      `${API_URL}/account/${userId}`
    );
  }
}
