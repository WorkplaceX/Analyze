import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app/component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import * as util from './app/util';

@NgModule({
  imports: [NgbModule.forRoot(), BrowserModule],
  declarations: [AppComponent],
  bootstrap: [AppComponent],
  providers: [
    { provide: 'angularData', useValue: JSON.stringify({ Name: "app.module.ts=" + util.currentTime() }) },
  ]
})
export class AppModule { }
