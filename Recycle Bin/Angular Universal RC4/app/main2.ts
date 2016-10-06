import { Component, ElementRef } from '@angular/core';
import * as router from '@angular/router';

@Component({
  selector: 'my-app2',
  template: '<h1>App {{ text }}</h1>'
})
export class AppComponent2 { 
	text: string;

	constructor() {
		this.text = 'Hello Data'
	}
}

@Component({
  template: '<h1>App {{ text }}</h1>'
})
export class AppComponent3 { 
	text: string;
	MyElementRef: ElementRef;

	constructor(elementRef: ElementRef) {
		this.MyElementRef = elementRef;
		this.text = 'Hello Data3'
	}
}
