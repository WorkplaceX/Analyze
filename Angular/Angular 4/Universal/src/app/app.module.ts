import { NgModule } from '@angular/core';

import { BrowserModule } from '@angular/platform-browser';


import { AppComponent } from './app.component';
import { Button } from './Component/Button';
import { MdButtonModule } from '@angular/material';

@NgModule({
  exports: [
    MdButtonModule,
  ]
})
export class ApplicationMaterialModule {}

@NgModule({
  declarations: [
    AppComponent, Button
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'Universal'}),

    MdButtonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
