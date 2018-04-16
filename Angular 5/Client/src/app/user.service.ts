import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class UserService {

  constructor(private http: HttpClient) { }

  getUserList(sortColumnName: string, sortIsDesc: boolean): Observable<Object> {
    /*

    http://jsonplaceholder.typicode.com/users uses Json-Server as backend (https://github.com/typicode/json-server).
    Json-Server supports server side paging, sorting and filtering.

    */
    let sortParam = '_sort=' + sortColumnName;
    let sortAscDesc = 'asc';
    if (sortIsDesc === true) {
      sortAscDesc = 'desc';
    }
    sortParam += '&_order=' + sortAscDesc;
    return this.http.get('http://jsonplaceholder.typicode.com/users' + '?' + sortParam);
  }

  getUser(userId: number) {
    return this.http.get('http://jsonplaceholder.typicode.com/users/' + userId);
  }
}
