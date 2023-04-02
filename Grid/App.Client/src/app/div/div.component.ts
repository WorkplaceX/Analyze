import { Component, Input } from '@angular/core';
import { Json } from '../data.service';

@Component({
  selector: '[app-div]',
  templateUrl: './div.component.html',
  styleUrls: ['./div.component.scss']
})
export class DivComponent {
  @Input() 
  json!: Json
}
