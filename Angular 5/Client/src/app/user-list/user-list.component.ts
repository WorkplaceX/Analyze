import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  constructor(private userService: UserService) {
  }

  public UserList;

  public IsLoad = 0; // Show spinner icon while loading.

  ngOnInit() {
    this.getUserList();
  }

  getUserList() {
    this.IsLoad += 1;
    this.userService.getUserList().subscribe(
      data => { this.UserList = data; this.IsLoad -= 1; },
      err => console.error(err),
      () => console.log('UserList loaded')
    );
  }
}
