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
  <GridField *ngIf="json.Type=='GridField'" [json]=json></GridField>
  <GridKeyboard *ngIf="json.Type=='GridKeyboard'" [json]=json></GridKeyboard>
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
    <Selector [json]=item *ngFor="let item of json.List; trackBy trackBy"></Selector>
  </div>  
`
})
export class LayoutDebug {
  @Input() json: any

  trackBy(index: any, item: any): any {
    return item.Type;
  }
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
  <input type="text" class="form-control" [(ngModel)]="text" (ngModelChange)="onChange()" (focus)="focus(true)" (focusout)="focus(false)" placeholder="Empty"/>
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

  onChange() {
    this.json.TextNew = this.text;
    this.dataService.update();
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
  <GridHeader [json]=item *ngFor="let item of dataService.json.GridData.ColumnList[json.GridName]; trackBy trackBy"></GridHeader>
  </div>
  <GridRow [jsonGridData]=dataService.json.GridData [jsonGrid]=json [json]=item *ngFor="let item of dataService.json.GridData.RowList[json.GridName]; trackBy trackBy"></GridRow>
  `
})
export class Grid {
  constructor(dataService: DataService){
    this.dataService = dataService;
  }

  dataService: DataService;
  @Input() json: any

  trackBy(index: any, item: any) {
    return item.Type;
  }
}

/* GridRow */
@Component({
  selector: 'GridRow',
  template: `
  <div (click)="click()" (mouseover)="mouseOver()" (mouseout)="mouseOut()" [ngClass]="{'select-class1':json.IsSelect==1, 'select-class2':json.IsSelect==2, 'select-class3':json.IsSelect==3}" style="white-space: nowrap;">
  <GridCell [jsonGrid]=jsonGrid [jsonGridData]=jsonGridData [jsonRow]=json [json]=item *ngFor="let item of jsonGridData.ColumnList[jsonGrid.GridName]; trackBy trackBy"></GridCell>
  </div>
  `,
  styles: [`
  .select-class1 {
    background-color: rgba(255, 255, 0, 0.5);
  }
  .select-class2 {
    background-color: rgba(255, 255, 0, 0.2);
  }
  .select-class3 {
    background-color: rgba(255, 255, 0, 0.7);
  }
  `]
})
export class GridRow {
  @Input() json: any;
  @Input() jsonGrid: any;
  @Input() jsonGridData: any;
  dataService: DataService;

  constructor(dataService: DataService){
    this.dataService = dataService;
  }
  
  mouseOver(){
    this.json.IsSelect = this.json.IsSelect | 2;
  }

  mouseOut(){
    this.json.IsSelect = this.json.IsSelect & 1;
  }

  trackBy(index: any, item: any) {
    return item.Type;
  }

  click(){
    this.json.IsClick = true;
    this.dataService.update();
  }
}

/* GridCell */
@Component({
  selector: 'GridCell',
  template: `
  <div (click)="click()" [ngClass]="{'select-class':jsonGridData.CellList[jsonGrid.GridName][json.FieldName][jsonRow.Index].IsSelect}" style="display:inline-block; position:relative;" [style.width.%]=json.WidthPercent>
  <div style='margin-right:30px;text-overflow: ellipsis; overflow:hidden;'>
  {{ jsonGridData.CellList[jsonGrid.GridName][json.FieldName][jsonRow.Index].V }}
  <img src='ArrowDown.png' style="width:12px;height:12px;top:8px;position:absolute;right:7px;"/>
  </div>
  <GridFieldInstance [dataService]=dataService [gridName]=jsonGrid.GridName [fieldName]=json.FieldName [index]=jsonRow.Index></GridFieldInstance>
  `,
  styles: [`
  .select-class {
    border:solid 2px blue;
  }
  `]
})
export class GridCell {
  @Input() json: any; // Column // Used for FieldName
  @Input() jsonRow: any; // Used for Index
  @Input() jsonGrid: any; // Used for GridName
  @Input() jsonGridData: any; // Used for Value
  dataService: DataService;

  constructor(dataService: DataService){
    this.dataService = dataService;
  }

  trackBy(index: any, item: any) {
    return item.Type;
  }

  click(){
    this.jsonGridData.CellList[this.jsonGrid.GridName][this.json.FieldName][this.jsonRow.Index].IsClick = true;
    this.dataService.update();
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

  trackBy(index: any, item: any) {
    return item.Type;
  }

  click(){
    this.json.IsSelect = !this.json.IsSelect;
  }
}

/* GridField */
@Component({
  selector: 'GridField',
  template: `
  HELLO FIELD {{gridData()?.CellList[gridData().FocusGridName][gridData().FocusFieldName][gridData().FocusIndex].V}}
  <input type="text" class="form-control" [(ngModel)]="gridData()?.CellList[gridData().FocusGridName][gridData().FocusFieldName][gridData().FocusIndex].V" placeholder="Empty"/>
  <GridFieldInstance [dataService]=dataService [gridName]=dataService.json.GridData.FocusGridName [fieldName]="fieldName" [index]=dataService.json.GridData.FocusIndex></GridFieldInstance>
  `
})
export class GridField {
  constructor(dataService: DataService){
    this.dataService = dataService;
    this.fieldName = "AirportText";
  }

  dataService: DataService;
  @Input() json: any;
  fieldName: any;

  gridData(){
    if (this.dataService.json.GridData.FocusGridName != null){
      return this.dataService.json.GridData;
    } else {
      return null;
    }
  }

  trackBy(index: any, item: any) {
    return item.Type;
  }
}

/* GridFieldInstance */
@Component({
  selector: 'GridFieldInstance',
  template: `
  {{x}}
  <input type="text" class="form-control" [(ngModel)]="gridData()?.CellList[gridName][fieldName][index].V" placeholder="Empty"/>
  `
})
export class GridFieldInstance {
  @Input() dataService: DataService;
  @Input() gridName: any;
  @Input() fieldName: any;
  @Input() index: any;
  x: any;

  gridData(){
    if (this.gridName != null){
      return this.dataService.json.GridData;
    } else {
      return null;
    }
  }
}

/* GridKeyboard */
@Component({
  selector: 'GridKeyboard',
  template: `
  {{x}}
  `,
  host: {
    '(document:keydown)': '_keydown($event)',
  }
})
export class GridKeyboard {
  constructor(dataService: DataService){
    this.dataService = dataService;
    this.x = "";
  }
  @Input() json: any;
  dataService: DataService;
  x: string;

  public _keydown(event: KeyboardEvent) {
    this.x = this.x + event.keyCode;
    var gridData: any = this.dataService.json.GridData;
    if (gridData.FocusGridName != null){
      if (event.keyCode == 40 || event.keyCode == 38){
        var rowList = gridData.RowList[gridData.FocusGridName];
        var rowCurrent: string = gridData.FocusIndex;
        var rowPrevious: string;
        var rowNext: string;
        if (rowCurrent != null){
          /* RowPrevious */
          for (let index in rowList){
            if (index == rowCurrent){
              if (rowPrevious == null){
                rowPrevious = rowCurrent;
              }
              break;
            }
            rowPrevious = index;
          }
          /* RowNext */
          for (let index in rowList){
            if (rowNext != null){
              rowNext = index;
              break;
            }
            if (index == rowCurrent){
              rowNext = index;
            }
          }
          if (event.keyCode == 38){
            rowCurrent = rowPrevious;
          }
          if (event.keyCode == 40){
            rowCurrent = rowNext;
          }
          /* Update Row.IsSelect */
          for (let index in rowList){
            if (rowList[index].Index == rowCurrent){
              rowList[index].IsSelect = 1;
            } else {
              rowList[index].IsSelect = 0;
            }
          }
          /* Update Cell.IsSelect */
          var columList = gridData.ColumnList[gridData.FocusGridName];
          var cellList = gridData.CellList[gridData.FocusGridName];
          for (let indexColumn in columList){
            let fieldName: string = columList[indexColumn].FieldName;
            cellList[fieldName][gridData.FocusIndex].IsSelect = false;
          }
          for (let indexColumn in columList){
            let fieldName: string = columList[indexColumn].FieldName;
            if (fieldName == gridData.FocusFieldName){
              cellList[fieldName][rowCurrent].IsSelect = true;
            }
          }
          gridData.FocusIndex = rowCurrent;
        }
        event.preventDefault();
      }
    }
  }
}
