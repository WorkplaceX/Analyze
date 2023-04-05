import { Component, Input } from '@angular/core';
import { Json } from '../data.service';

@Component({
  selector: '[app-button]',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss']
})
export class ButtonComponent {
  @Input() 
  json!: Json
}
