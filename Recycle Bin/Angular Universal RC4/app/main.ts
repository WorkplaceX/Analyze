import * as router from '@angular/router';

import 'reflect-metadata';
import 'zone.js/dist/zone';

import { Component } from '@angular/core';
import { bootstrap } from '@angular/platform-browser-dynamic';

@Component({
  selector: 'my-app',
  template: '<h1>App {{ text }}</h1>',
	 directives: [router.ROUTER_DIRECTIVES]
})
export class AppComponent { 
	text: string;

	constructor() {
		this.text = 'Hello Data'
	}
}


bootstrap(AppComponent);