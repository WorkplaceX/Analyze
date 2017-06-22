import { Component } from '@angular/core';

@Component({
  selector: '[button]',
  template: '<button (click)="click()">{{t}}</button>'
})
export class Button {
  t: string = "Text";

  click(){
	this.t = this.t + ".";
  } 
}
