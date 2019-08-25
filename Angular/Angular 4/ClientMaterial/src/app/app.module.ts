import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

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
    BrowserModule,
    ApplicationMaterialModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
