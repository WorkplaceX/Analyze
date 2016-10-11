import { Component } from '@angular/core';
import { DataService, Data } from './dataService';
import { Inject } from '@angular/core';

@Component({
  selector: 'my-app',
  template: '<h1>My First Angular App ({{ data.Name }})</h1>',
  providers: [DataService]  
})
export class AppComponent { 
  data: Data;

  constructor(@Inject('paramsData') paramsData: string, dataService: DataService){
    this.data = dataService.data;
    if (paramsData != null){
      this.data.Name = paramsData;
    }
  }
}
