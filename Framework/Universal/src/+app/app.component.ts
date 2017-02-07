import { Component, Input } from '@angular/core';
import { DataService } from './dataService';
import  * as util from './util';

/* AppComponent */
@Component({
  selector: 'app',
  template: `
  <p>json.IsBrowser=({{ dataService.json.IsBrowser }})</p>
  <p>RequestCount=({{ dataService.RequestCount }})</p>
  <p>json.ResponseCount=({{ dataService.json.ResponseCount }})</p>
  <p>log=({{ dataService.log }})</p>
  <Selector [json]=item *ngFor="let item of dataService.json.List; trackBy:fn"></Selector>
  <div class="container">
    <div class="row">
      <div class="col-sm-12">
        <h1>Application</h1>
      </div>
    </div>    
    <div class="row">
      <div class="col-sm-4">
        <p>json.Name=({{ dataService.json.Name }})</p>
        <p>json.Session=({{ dataService.json.Session }})</p>
        <p>json.IsBrowser=({{ dataService.json.IsBrowser }})</p>
        <p>Version=({{ dataService.json.VersionClient + '; ' + dataService.json.VersionServer }})</p>
        <p>json=({{ jsonText }})</p>
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
    this.dataService.json.Name += " " + util.currentTime() + ";" 
  } 

  clickServer(){
    this.dataService.update();
  } 

  clickJson() {
    this.jsonText = JSON.stringify(this.dataService.json);
  }
}

/* Selector */
@Component({
  selector: 'Selector',
  template: `
  <LayoutContainer *ngIf="json.Type=='LayoutContainer'" [json]=json></LayoutContainer>
  <LayoutRow *ngIf="json.Type=='LayoutRow'" [json]=json></LayoutRow>
  <LayoutCell *ngIf="json.Type=='LayoutCell'" [json]=json></LayoutCell>
  <ButtonX *ngIf="json.Type=='Button'" [json]=json></ButtonX>
  <InputX *ngIf="json.Type=='Input'" [json]=json></InputX>
  <Label *ngIf="json.Type=='Label'" [json]=json></Label>
  <Grid *ngIf="json.Type=='Grid'" [json]=json></Grid>
  <!-- <LayoutDebug [json]=json></LayoutDebug> -->
`
})
export class Selector {
  @Input() json: any
}

/* LayoutContainer */
@Component({
  selector: 'LayoutContainer',
  template: `
  <div style='border:1px solid; padding:2px; margin:2px; background-color:yellow;'>
    Text={{ json.Text }}
    <Selector [json]=item *ngFor="let item of json.List; trackBy trackBy"></Selector>
  </div>  
`
})
export class LayoutContainer {
  @Input() json: any

  trackBy(index: any, item: any) {
    return item.Type;
  }
}

/* LayoutRow */
@Component({
  selector: 'LayoutRow',
  template: `
  <div style='border:1px solid; padding:2px; margin:2px; background-color:red;'>
    Text={{ json.Text }}
    <Selector [json]=item *ngFor="let item of json.List; trackBy trackBy"></Selector>
  </div>  
`
})
export class LayoutRow {
  @Input() json: any

  trackBy(index: any, item: any) {
    return item.Type;
  }
}

/* LayoutCell */
@Component({
  selector: 'LayoutCell',
  template: `
  <div style='border:1px solid; padding:2px; margin:2px; background-color:green;'>
    Text={{ json.Text }}
    <Selector [json]=item *ngFor="let item of json.List; trackBy trackBy"></Selector>
  </div>  
`
})
export class LayoutCell {
  @Input() json: any

  trackBy(index: any, item: any): any {
    return item.Type;
  }
}

/* LayoutDebug */
@Component({
  selector: 'LayoutDebug',
  template: `
  <div style='border:1px solid; padding:2px; margin:2px; background-color:yellow;'>
    Text={{ json.Text }}
    <Selector [json]=item *ngFor="let item of json.List"></Selector>
  </div>  
`
})
export class LayoutDebug {
  @Input() json: any
}

/* Button */
@Component({
  selector: 'ButtonX',
  template: `<button type="text" class="btn btn-primary" (click)="click()">{{ json.Text }}</button>`
})
export class Button {
  constructor(dataService: DataService){
    this.dataService = dataService;
  }

  @Input() json: any
  dataService: DataService;

  click(){
    this.json.IsClick = true;
    this.dataService.update();
  } 
}

/* InputX */
@Component({
  selector: 'InputX',
  template: `
  <input value="{{text}}" (keyup)="onKey($event)" (focus)="focus(true)" (focusout)="focus(false)" placeholder="Empty" />
  <p>
    Text={{ json.Text }}<br/>
    TextNew={{ json.TextNew}}<br/>
    Focus={{json.IsFocus}}<br/>
    AutoComplete={{json.AutoComplete}}
  </p>`
})
export class InputX {
  @Input() json: any
  dataService: DataService;
  text: string;
  inputFocused: any;

  constructor( dataService: DataService){
    this.dataService = dataService;
  }

  ngOnInit() {
    this.text = this.json.Text;
  }  

  onKey(event:any) {
    this.text = event.target.value;
    this.json.TextNew = this.text;
    this.dataService.update();
  }

  focus(isFocus: boolean) {
    this.json.IsFocus = isFocus;
  }  
}

/* Label */
@Component({
  selector: 'Label',
  template: `{{ json.Text }}`
})
export class Label {
  @Input() json: any
}

/* Grid */
@Component({
  selector: 'Grid',
  template: `
  <div style="white-space: nowrap;">
  <GridHeader [grid]=json [json]=item *ngFor="let item of json.ColumnList; trackBy trackBy"></GridHeader>
  </div>
  <GridRow [grid]=json [json]=item *ngFor="let item of json.RowList; trackBy trackBy"></GridRow>
  `
})
export class Grid {
  @Input() json: any

  trackBy(index: any, item: any) {
    return item.Type;
  }
}

/* GridRow */
@Component({
  selector: 'GridRow',
  template: `
  <div (mouseover)="json.IsSelect=true" (mouseout)="json.IsSelect=false" [ngClass]="{'select-class':json.IsSelect}" style="white-space: nowrap;">
  <GridCell [grid]=grid [row]=json [json]=item *ngFor="let item of grid.ColumnList; trackBy trackBy"></GridCell>
  </div>
  `,
  styles: [`
  .select-class {
    background-color: lightgray;
  }
  `]
})
export class GridRow {
  @Input() json: any;
  @Input() grid: any;

  trackBy(index: any, item: any) {
    return item.Type;
  }

  click(){
    this.json.IsSelect = !this.json.IsSelect;
  }
}

/* GridCell */
@Component({
  selector: 'GridCell',
  template: `
  <div (click)="click()" [ngClass]="{'select-class':json.IsSelect}" style="display:inline-block; overflow: hidden;" [style.width.%]=json.WidthPercent>{{ grid.CellList[json.FieldName][row.Index].V }}</div>
  `,
  styles: [`
  .select-class {
    background-color: rgba(255, 255, 0, 0.7);
  }
  `]
})
export class GridCell {
  @Input() json: any; // Column
  @Input() grid: any;
  @Input() row: any;

  trackBy(index: any, item: any) {
    return item.Type;
  }

  click(){
    this.json.IsSelect = !this.json.IsSelect;
  }
}

/* GridHeader */
@Component({
  selector: 'GridHeader',
  template: `
  <div (click)="click()" [ngClass]="{'select-class':json.IsSelect}" style="display:inline-block; overflow: hidden;" [style.width.%]=json.WidthPercent><b>{{ json.Text }}</b></div>
  `,
  styles: [`
  .select-class {
    background-color: rgba(255, 255, 0, 0.7);
  }
  `]
})
export class GridHeader {
  @Input() json: any; // Column
  @Input() grid: any;
  @Input() row: any;

  trackBy(index: any, item: any) {
    return item.Type;
  }

  click(){
    this.json.IsSelect = !this.json.IsSelect;
  }
}
