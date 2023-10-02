import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';

@Injectable({
  providedIn: 'root',
})
export class MemberService {
  baseUrl = environment.apiUrl;

  constructor(private httpClient: HttpClient) {}

  getMembers() {
    return this.httpClient.get<Member[]>(
      this.baseUrl + 'users',
      this.getHttpOptions()
    );
  }

  getMember(username: string) {
    return this.httpClient.get<Member>(
      this.baseUrl + 'users/' + username,
      this.getHttpOptions()
    );
  }

  getHttpOptions() {
    const userString = localStorage.getItem('user');

    if (!userString) return;

    const user = JSON.parse(userString);

    return {
      headers: new HttpHeaders({
        Authorization: 'Bearer ' + user.token,
      }),
    };
  }
}
