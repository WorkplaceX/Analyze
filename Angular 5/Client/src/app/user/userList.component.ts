import { Component } from '@angular/core';
import { UserService } from './user.service'

@Component({
  selector: 'app-userlist',
  templateUrl: './userList.component.html',
  styleUrls: ['./userList.component.css']
})
export class UserListComponent {
  title = 'userList';

  constructor(private userService:UserService) {
    
  }

  ngOnInit() {
    this.getUserList();
  }

  public UserList;

  getUserList() {
    this.userService.getUserList().subscribe(
      data => { this.UserList = data},
      err => console.error(err),
      () => console.log('UserList load done')
    );
  }
}
