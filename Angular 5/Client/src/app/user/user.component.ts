import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {Location} from '@angular/common';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  constructor(private route: ActivatedRoute, private location: Location) {
    this.route.params.subscribe( params => {
      if (params['userId']) {
        this.getUser(params['userId']);
      }
    });
  }

  public UserId: number;

  ngOnInit() {

  }

  backClick() {
    this.location.back();
  }

  getUser(userId: number) {
    this.UserId = userId;
  }

}
