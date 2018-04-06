import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class UserService {

  constructor(private http:HttpClient) {

  }

  getUserList() {
    return this.http.get('http://jsonplaceholder.typicode.com/users');
  }
}
