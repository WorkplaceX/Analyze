import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {Location} from '@angular/common';
import { UserService, Typicode } from '../user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  constructor(private route: ActivatedRoute, private location: Location, private userService: UserService) {
    this.route.params.subscribe( params => {
      if (params['userId']) {
        this.getUser(params['userId']);
      }
    });
  }

  userId: number;

  user: Typicode.User;

  ngOnInit() {

  }

  backClick() {
    this.location.back();
  }

  getUser(userId: number) {
    this.userId = userId;
    this.userService.getUser(userId).subscribe(
      data => { this.user = data; },
      err => console.error(err),
      () => console.log('User loaded')
    );
  }
}