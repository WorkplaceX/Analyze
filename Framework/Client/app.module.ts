import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
/* GulpFind01 */ import { AppComponent, Selector, LayoutContainer, LayoutRow, LayoutCell, LayoutDebug, Button, InputX, Label } from './app/component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import * as util from './app/util';
import { HttpModule } from '@angular/http';

@NgModule({
  imports: [NgbModule.forRoot(), BrowserModule, HttpModule],
/* GulpFind02 */ declarations: [AppComponent, Selector, LayoutContainer, LayoutRow, LayoutCell, LayoutDebug, Button, InputX, Label],
  bootstrap: [AppComponent],
  providers: [
    { provide: 'angularData', useValue: JSON.stringify({ Name: "app.module.ts=" + util.currentTime() }) },
  ]
})
export class AppModule { }
