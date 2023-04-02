import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { ButtonComponent, DivComponent, HtmlComponent } from './framework/framework.component';

@NgModule({
  declarations: [
    AppComponent,
    DivComponent,
    HtmlComponent,
    ButtonComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
