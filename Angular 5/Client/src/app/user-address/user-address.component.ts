import { Component, OnInit, Input } from '@angular/core';
import { Typicode } from '../user.service';

@Component({
  selector: 'app-user-address',
  template: `
  <p>
    {{ userAddress.street }}<br/>
    {{ userAddress.city }} {{ userAddress.zipcode }}
  </p>
  `,
  styleUrls: ['./user-address.component.css']
})
export class UserAddressComponent implements OnInit {
  @Input() userAddress: Typicode.Address;

  constructor() { }

  ngOnInit() {
  }

}
