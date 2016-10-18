import 'jQuery';

let Tether = require('tether');
(<any>window).Tether = Tether;

import 'bootstrap';

import { Component } from '@angular/core';
import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserModule } from '@angular/platform-browser';

@Component({
    selector: 'my-app',
    template: `
<div class="container">
  <div class="row">
    <div class="col-sm-4">
      <h1>My First Angular App</h1>
    </div>
    <div class="col-sm-4">
      One of three columns (2)
    </div>
    <div class="col-sm-4">
      One of three columns (3)
  <label class="btn btn-primary" [class.active]="model.left">
    <input type="checkbox" /> Button
  </label>    </div>
  </div>
</div>

`
})
export class AppComponent {
    model = {
        left: true,
        middle: false,
        right: false
    };
}

@NgModule({
    declarations: [AppComponent],
    imports: [BrowserModule, NgbModule.forRoot()],
    bootstrap: [AppComponent]
})
export class AppModule {
}

import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
const platform = platformBrowserDynamic();
platform.bootstrapModule(AppModule);