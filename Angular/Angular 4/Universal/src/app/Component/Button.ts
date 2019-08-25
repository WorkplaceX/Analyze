import { Component } from '@angular/core';

@Component({
  selector: '[button]',
  template: 'Click button: <button md-button (click)="click()">{{t}}</button>'
})
export class Button {
  t: string = "Button";

  click(){
	this.t = this.t + ".";
  } 
}
