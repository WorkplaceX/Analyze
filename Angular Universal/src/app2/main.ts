// the polyfills must be the first thing imported in node.js
import 'angular2-universal-polyfills';

// Angular 2
import { enableProdMode } from '@angular/core';
import { platformNodeDynamic } from "angular2-platform-node";
import { platformDynamicServer } from '@angular/platform-server';
import { NgModule }      from '@angular/core';
import { Component } from '@angular/core';
import { BrowserModule, platformBrowser } from '@angular/platform-browser';
import { UniversalModule } from 'angular2-universal';

@Component({
  selector: 'app',
  template: '<p>Hello world!</p>',
})
export class MyComponent {
    constructor()
    {
        console.log("Constructor MyComponent");
    }
}

@NgModule({
  imports: [ UniversalModule ], 
  declarations: [ MyComponent ],
  bootstrap: [ MyComponent ],
})
export class MyModule { 
  constructor() {
    console.log("Constructor MyModule");    
  }
}

enableProdMode();

import 'zone.js';

var requestZone = Zone.current.fork({
  name: 'angular-universal request',
  properties: {
    document: '<!DOCTYPE html><html><head></head><body><app></app></body></html>'
  }
});

requestZone.run(() => 
{  
  platformNodeDynamic().serializeModule(MyModule).then(html=>{ console.log("Html=" + html) });

  // platformDynamicServer().bootstrapModule(MyModule).then(obj=>{ console.log("Obj=" + obj) });
});

console.log("Hello World!");
