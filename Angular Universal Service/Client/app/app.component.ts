import { Component } from '@angular/core';
import { DataService, Data } from './dataService';

@Component({
  selector: 'my-app',
  template: `
  <div class="container">
    <div class="row">
      <div class="col-sm-12">
        <h1>Angular2 App with Bootstrap</h1>
      </div>
    </div>    
    <div class="row">
      <div class="col-sm-4">
        <p>Data=({{ data.Name }})</p>
      </div>
      <div class="col-sm-4">
        Second of three columns
        <button class="btn btn-primary" (click)="click($event)">Click</button>
      </div>
      <div class="col-sm-4">
        Third of three columns
        <label class="btn btn-primary" >
          <input type="checkbox" /> Button
        </label>
      </div>
    </div>
  </div>  
`,
  providers: [DataService]  
})
export class AppComponent { 
  data: Data;

  constructor(dataService: DataService){
    this.data = dataService.data;
  } 

  click(){
    this.data.Name += "."; 
  } 
}

