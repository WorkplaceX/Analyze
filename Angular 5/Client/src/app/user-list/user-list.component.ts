import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  constructor(private userService: UserService) {
    this.SortIsDesc = false;
  }

  public UserList;

  public IsLoad = 0; // Show spinner icon while loading.

  public SortIsDesc: boolean; // Ascending or descending order of json column "name".

  ngOnInit() {
    this.getUserList();
  }

  getUserList() {
    this.IsLoad += 1;
    this.userService.getUserList('name', this.SortIsDesc).subscribe(
      data => { this.UserList = data; this.IsLoad -= 1; },
      err => console.error(err),
      () => console.log('UserList loaded')
    );
  }

  columnNameClick() {
    this.SortIsDesc = !this.SortIsDesc; // Change sort order of json column "name".
    this.getUserList();
  }
}
