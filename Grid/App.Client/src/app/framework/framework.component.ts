import { Component, Input } from '@angular/core';
import { Json } from '../data.service';

/* Div */
@Component({
  selector: '[app-div]',
  template: `
  <div app-div *ngFor="let item of json.list" [json]="item" [ngClass]="item.cssClass" [style]="item.cssStyleCurrent"></div>
  <div app-button *ngIf="json.contentType == 'Button'" [json]="json" style="display: inline;"></div>
  <div app-anchor *ngIf="json.contentType == 'Anchor'" [json]="json" style="display: inline;"></div>
  <div app-html *ngIf="json.contentType == 'Html'" [json]="json" style="display: inline;"></div>
  <div app-footer *ngIf="json.contentType == 'Footer'" [json]="json" style="display: inline;"></div>
  `,
})
export class DivComponent {
  @Input() 
  json!: Json
}

/* Html */
@Component({
  selector: '[app-html]',
  template: `
  <div [innerHtml]="json.text" [ngClass]="json.cssClass" style="display:inline"></div>
  `,
})
export class HtmlComponent {
  @Input()
  json!: Json
}

/* Button */
@Component({
  selector: '[app-button]',
  template: `<button>{{json.text}}</button>`,
})
export class ButtonComponent {
  @Input() 
  json!: Json
}

/* Anchor */
@Component({
  selector: '[app-anchor]',
  template: `<a href="{{json.href}}">{{json.text}}</a>`,
})
export class AnchorComponent {
  @Input() 
  json!: Json
}
