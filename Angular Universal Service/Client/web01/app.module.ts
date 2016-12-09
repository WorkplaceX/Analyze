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
    { provide: 'paramsData', useValue: JSON.stringify({ Name: "app.module.ts Web01=" + util.currentTime() }) },
  ]
})
export class AppModule { }
