import { Component } from '@angular/core';

@Component({
  selector: '[button]',
  template: '<p>Press the button:</p><button class="btn btn-primary" (click)="click()">{{t}}</button> <input type="text" />'
})
export class Button {
  t: string = "Button";

  click(){
	this.t = this.t + ".";
  } 
}
