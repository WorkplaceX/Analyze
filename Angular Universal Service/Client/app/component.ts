import { Component, Input } from '@angular/core';
import { DataService, Data } from './dataService';
import  * as util from './util';

@Component({
  selector: 'app',
  template: `
  <Selector [data]=item *ngFor="let item of dataService.data.List"></Selector>
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
  <LayoutContainer *ngIf="data.Type=='LayoutContainer'" [data]=data></LayoutContainer>
  <LayoutRow *ngIf="data.Type=='LayoutRow'" [data]=data></LayoutRow>
  <LayoutCell *ngIf="data.Type=='LayoutCell'" [data]=data></LayoutCell>
  <ButtonX *ngIf="data.Type=='Button'" [data]=data></ButtonX>
  <InputX *ngIf="data.Type=='Input'" [data]=data></InputX>
  <Label *ngIf="data.Type=='Label'" [data]=data></Label>
  <!-- <LayoutDebug [data]=data></LayoutDebug> -->
`
})
export class Selector {
  @Input() data: any
}

@Component({
  selector: 'LayoutContainer',
  template: `
  <div style='border:1px solid; padding:2px; margin:2px; background-color:yellow;'>
    Text={{ data.Text }}
    <Selector [data]=item *ngFor="let item of data.List"></Selector>
  </div>  
`
})
export class LayoutContainer {
  @Input() data: any
}

@Component({
  selector: 'LayoutRow',
  template: `
  <div style='border:1px solid; padding:2px; margin:2px; background-color:red;'>
    Text={{ data.Text }}
    <Selector [data]=item *ngFor="let item of data.List"></Selector>
  </div>  
`
})
export class LayoutRow {
  @Input() data: any
}

@Component({
  selector: 'LayoutCell',
  template: `
  <div style='border:1px solid; padding:2px; margin:2px; background-color:green;'>
    Text={{ data.Text }}
    <Selector [data]=item *ngFor="let item of data.List"></Selector>
  </div>  
`
})
export class LayoutCell {
  @Input() data: any
}

@Component({
  selector: 'LayoutDebug',
  template: `
  <div style='border:1px solid; padding:2px; margin:2px; background-color:yellow;'>
    Text={{ data.Text }}
    <Selector [data]=item *ngFor="let item of data.List"></Selector>
  </div>  
`
})
export class LayoutDebug {
  @Input() data: any
}

@Component({
  selector: 'ButtonX',
  template: `<button class="btn btn-primary" (click)="click()">{{ data.Text }}</button>`
})
export class Button {
  @Input() data: any

  dataService: DataService;

  constructor(dataService: DataService){
    this.dataService = dataService;
  }

  click(){
    this.data.IsClick = true;
    this.dataService.update();
  } 
}

@Component({
  selector: 'InputX',
  template: `
  <input (keyup)="onKey($event)" (focus)="focus(true)" (focusout)="focus(false)" value="{{data.Text}}"/>
  <p>
    Text={{ data.Text }}<br/>
    TextNew={{ data.TextNew}}<br/>
    Focus={{data.IsFocus}}
  </p>`
})
export class InputX {
  @Input() data: any
  onKey(event:any) {
    this.data.TextNew = event.target.value;
  }

  focus(isFocus: boolean) {
    this.data.IsFocus = isFocus;
  }  
}

@Component({
  selector: 'Label',
  template: `{{ data.Text }}`
})
export class Label {
  @Input() data: any
}
