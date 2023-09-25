import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {
  registerMode = false;
  users: any;

  constructor(private http: HttpClient) {
    this.getUsers();
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  getUsers() {
    this.http.get('https://localhost:7210/api/Users').subscribe({
      next: (response) => (this.users = response),
      error: (error) => console.log(error),
      complete: () => console.log('success'),
    });
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}
