import { Component, OnInit } from '@angular/core';
import { UserService, Typicode } from '../user.service';
import { summaryForJitName } from '@angular/compiler/src/aot/util';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  constructor(private userService: UserService) {
    this.sortIsDesc = false;
  }

  userList: Typicode.User[];

  isLoad = 0; // Show spinner icon while loading.

  sortIsDesc = false; // Ascending or descending order of json column "name".

  sortIsClient = true; // Json-Server supports server side sorting. (Switch between server side or client side sorting)

  ngOnInit() {
    this.getUserList(); // Load UserList.
  }

  getUserList() {
    // Client sort
    if (this.sortIsClient === true && this.userList != null) {
      this.userList = this.userList.sort((n1, n2) => this.sortName(n1.name, n2.name));
    }

    // Server sort
    if (this.sortIsClient === false || this.userList == null) {
      this.isLoad += 1;
      this.userService.getUserList('name', this.sortIsDesc).subscribe(
        data => { this.userList = data; this.isLoad -= 1; },
        err => console.error(err),
        () => console.log('UserList loaded')
      );
    }
  }

  // Switch sort direction
  columnNameClick() {
    this.sortIsDesc = !this.sortIsDesc; // Change sort order of json column "name".
      this.getUserList();
  }

  // Starting from Angular 5, do not use or implement pipes for sorting. https://angular.io/guide/pipes quote: "The Angular team and
  // many experienced Angular developers strongly recommend moving filtering and sorting logic into the component itself."
  sortName(name1, name2) {
    let result = 0;
    if (name1 < name2) {
      result = 1;
    }
    if (name1 > name2) {
      result = -1;
    }
    if (result !== 0 && !this.sortIsDesc) {
      result = result * -1;
    }
    return result;
  }
}
