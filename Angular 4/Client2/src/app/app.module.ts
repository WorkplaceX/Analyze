import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';

import { Button } from './Component/Button'

@NgModule({
  declarations: [
    AppComponent, Button
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'client'})
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
