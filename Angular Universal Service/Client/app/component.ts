import { Component, Input } from '@angular/core';
import { DataService, Data } from './dataService';
import  * as util from './util';

@Component({
  selector: 'app',
  template: `
  <Selector *ngIf="dataService.data.Component!=null" [componentData]="dataService.data.Component"></Selector>
  <div class="container">
    <div class="row">
      <div class="col-sm-12">
        <h1>Application</h1>
      </div>
    </div>    
    <div class="row">
      <div class="col-sm-4">
        <p>data.Name=({{ dataService.data.Name }})</p>
        <p>data.Session=({{ dataService.data.Session }})</p>
        <p>data.IsBrowser=({{ dataService.data.IsBrowser }})</p>
        <p>Version=({{ dataService.data.VersionClient + '; ' + dataService.data.VersionServer }})</p>
        <p>data=({{ dataJson() }})</p>
      </div>
      <div class="col-sm-4">
        Second of three columns
        <button class="btn btn-primary" (click)="clickClient()">Client Update</button>
        <button class="btn btn-primary" (click)="clickServer()">Server Update</button>
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
  dataService: DataService;

  constructor(dataService: DataService){
    this.dataService = dataService;
  } 

  clickClient(){
    this.dataService.data.Name += " " + util.currentTime() + ";" 
  } 

  clickServer(){
    this.dataService.update();
  } 

  dataJson(){
    return JSON.stringify(this.dataService.data);
  }
}

@Component({
  selector: 'Selector',
  template: `
  <LayoutContainer *ngIf="componentData.Type=='LayoutContainer'" [componentData]=componentData></LayoutContainer>
  <LayoutRow *ngIf="componentData.Type=='LayoutRow'" [componentData]=componentData></LayoutRow>
  <LayoutCell *ngIf="componentData.Type=='LayoutCell'" [componentData]=componentData></LayoutCell>
  <!-- <LayoutDebug [componentData]=componentData></LayoutDebug> -->
`
})
export class Selector {
  @Input() componentData: any
}

@Component({
  selector: 'LayoutContainer',
  template: `
  <div style='border:1px solid; padding:2px; margin:2px; background-color:yellow;'>
    Text={{ componentData.Text }}
    <Selector [componentData]=item *ngFor="let item of componentData.List"></Selector>
  </div>  
`
})
export class LayoutContainer {
  @Input() componentData: any
}

@Component({
  selector: 'LayoutRow',
  template: `
  <div style='border:1px solid; padding:2px; margin:2px; background-color:red;'>
    Text={{ componentData.Text }}
    <Selector [componentData]=item *ngFor="let item of componentData.List"></Selector>
  </div>  
`
})
export class LayoutRow {
  @Input() componentData: any
}

@Component({
  selector: 'LayoutCell',
  template: `
  <div style='border:1px solid; padding:2px; margin:2px; background-color:green;'>
    Text={{ componentData.Text }}
    <Selector [componentData]=item *ngFor="let item of componentData.List"></Selector>
  </div>  
`
})
export class LayoutCell {
  @Input() componentData: any
}

@Component({
  selector: 'LayoutDebug',
  template: `
  <div style='border:1px solid; padding:2px; margin:2px; background-color:yellow;'>
    Text={{ componentData.Text }}
    <Selector [componentData]=item *ngFor="let item of componentData.List"></Selector>
  </div>  
`
})
export class LayoutDebug {
  @Input() componentData: any
}
