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
  }

  /** UserList */
  userList: Typicode.User[];

  /** Show spinner icon while loading UserList. */
  isLoad = 0;

  /** Ascending or descending order of json column "name". */
  sortIsDesc = false;

  ngOnInit() {
    this.getUserList(); // Load UserList.
  }

  getUserList() {
    // Client sort
    if (this.userService.sortIsClient === true && this.userList != null) {
      this.userList = this.userList.sort((n1, n2) => this.userService.sortName(n1.name, n2.name, this.sortIsDesc));
    }

    // Server sort
    if (this.userService.sortIsClient === false || this.userList == null) {
      this.isLoad += 1;
      this.userService.getUserList('name', this.sortIsDesc).subscribe(
        data => { this.userList = data; this.isLoad -= 1; },
        err => console.error(err),
        () => console.log('UserList loaded')
      );
    }
  }

  /** Switch sort direction */
  columnNameClick() {
    this.sortIsDesc = !this.sortIsDesc; // Change sort order of json column "name".
      this.getUserList(); // Update UserList.
  }
}
