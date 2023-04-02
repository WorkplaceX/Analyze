import { Component, Input } from '@angular/core';
import { Json } from '../data.service';

@Component({
  selector: '[app-html]',
  templateUrl: './html.component.html',
  styleUrls: ['./html.component.scss']
})
export class HtmlComponent {
  @Input()
  json!: Json
}
