import { Component, Input } from '@angular/core';
import { DataService, Data } from './dataService';
import  * as util from './util';

/* AppComponent */
@Component({
  selector: 'app',
  template: `
  <p>data.IsBrowser=({{ dataService.data.IsBrowser }})</p>
  <p>RequestCount=({{ dataService.RequestCount }})</p>
  <p>data.ResponseCount=({{ dataService.data.ResponseCount }})</p>
  <p>log=({{ dataService.log }})</p>
  <Selector [data]=item *ngFor="let item of dataService.data.List; trackBy:fn"></Selector>
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
        <p>data=({{ jsonText }})</p>
      </div>
      <div class="col-sm-4">
        Second of three columns
        <button class="btn btn-primary" (click)="clickClient()">Client Update</button>
        <button class="btn btn-primary" (click)="clickServer()">Server Update</button>
        <button class="btn btn-primary" (click)="clickJson()">Json</button>
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
  jsonText: string;

  constructor(dataService: DataService){
    this.dataService = dataService;
  } 

  fn() {
    return 0;
  }

  clickClient(){
    this.dataService.data.Name += " " + util.currentTime() + ";" 
  } 

  clickServer(){
    this.dataService.update();
  } 

  clickJson() {
    this.jsonText = JSON.stringify(this.dataService.data);
  }
}

/* Selector */
@Component({
  selector: 'Selector',
  template: `
  <LayoutContainer *ngIf="data.Type=='LayoutContainer'" [data]=data></LayoutContainer>
  <LayoutRow *ngIf="data.Type=='LayoutRow'" [data]=data></LayoutRow>
  <LayoutCell *ngIf="data.Type=='LayoutCell'" [data]=data></LayoutCell>
  <ButtonX *ngIf="data.Type=='Button'" [data]=data></ButtonX>
  <InputX *ngIf="data.Type=='Input'" [data]=data></InputX>
  <Label *ngIf="data.Type=='Label'" [data]=data></Label>
  <Grid *ngIf="data.Type=='Grid'" [data]=data></Grid>
  <!-- <LayoutDebug [data]=data></LayoutDebug> -->
`
})
export class Selector {
  @Input() data: any
}

/* LayoutContainer */
@Component({
  selector: 'LayoutContainer',
  template: `
  <div style='border:1px solid; padding:2px; margin:2px; background-color:yellow;'>
    Text={{ data.Text }}
    <Selector [data]=item *ngFor="let item of data.List; trackBy trackBy"></Selector>
  </div>  
`
})
export class LayoutContainer {
  @Input() data: any

  trackBy(index: any, item: any) {
    return item.Type;
  }
}

/* LayoutRow */
@Component({
  selector: 'LayoutRow',
  template: `
  <div style='border:1px solid; padding:2px; margin:2px; background-color:red;'>
    Text={{ data.Text }}
    <Selector [data]=item *ngFor="let item of data.List; trackBy trackBy"></Selector>
  </div>  
`
})
export class LayoutRow {
  @Input() data: any

  trackBy(index: any, item: any) {
    return item.Type;
  }
}

@Component({
  selector: 'LayoutCell',
  template: `
  <div style='border:1px solid; padding:2px; margin:2px; background-color:green;'>
    Text={{ data.Text }}
    <Selector [data]=item *ngFor="let item of data.List; trackBy trackBy"></Selector>
  </div>  
`
})
export class LayoutCell {
  @Input() data: any

  trackBy(index: any, item: any): any {
    return item.Type;
  }
}

/* LayoutDebug */
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

/* Button */
@Component({
  selector: 'ButtonX',
  template: `<button type="text" class="btn btn-primary" (click)="click()">{{ data.Text }}</button>`
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

/* InputX */
@Component({
  selector: 'InputX',
  template: `
  <input value="{{text}}" (keyup)="onKey($event)" (focus)="focus(true)" (focusout)="focus(false)" placeholder="Empty" />
  <p>
    Text={{ data.Text }}<br/>
    TextNew={{ data.TextNew}}<br/>
    Focus={{data.IsFocus}}<br/>
    AutoComplete={{data.AutoComplete}}
  </p>`
})
export class InputX {
  @Input() data: any
  dataService: DataService;
  text: string;
  inputFocused: any;

  constructor( dataService: DataService){
    this.dataService = dataService;
  }

  ngOnInit() {
    this.text = this.data.Text;
  }  

  onKey(event:any) {
    this.text = event.target.value;
    this.data.TextNew = this.text;
    this.dataService.update();
  }

  focus(isFocus: boolean) {
    this.data.IsFocus = isFocus;
  }  
}

/* Label */
@Component({
  selector: 'Label',
  template: `{{ data.Text }}`
})
export class Label {
  @Input() data: any
}

/* Grid */
@Component({
  selector: 'Grid',
  template: `
  <GridRow [grid]=data [data]=item *ngFor="let item of data.GridCellList; trackBy trackBy"></GridRow>
  `
})
export class Grid {
  @Input() data: any

  trackBy(index: any, item: any) {
    return item.Type;
  }
}

/* GridRow */
@Component({
  selector: 'GridRow',
  template: `
  <div (mouseover)="data.IsSelect=true" (mouseout)="data.IsSelect=false" [ngClass]="{'select-class':data.IsSelect}">
  <GridCell [grid]=grid [data]=item *ngFor="let item of data; trackBy trackBy"></GridCell>
  </div>
  `,
  styles: [`
  .select-class {
    background-color: lightgray;
  }
  `]
})
export class GridRow {
  @Input() grid: any;
  @Input() data: any

  trackBy(index: any, item: any) {
    return item.Type;
  }

  click(){
    this.data.IsSelect = !this.data.IsSelect;
  }
}

/* GridCell */
@Component({
  selector: 'GridCell',
  template: `
  <div (click)="click()" [ngClass]="{'select-class':data.IsSelect}" style="display:inline">{{ data.Value }}</div>
  `,
  styles: [`
  .select-class {
    background-color: rgba(255, 255, 0, 0.7);
  }
  `]
})
export class GridCell {
  @Input() data: any
  @Input() grid: any;

  trackBy(index: any, item: any) {
    return item.Type;
  }

  click(){
    this.data.IsSelect = !this.data.IsSelect;
  }
}
