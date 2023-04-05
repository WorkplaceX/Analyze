import { Component, Input } from '@angular/core';
import { Comp } from '../data.service';

@Component({
  selector: '[app-button]',
  templateUrl: './button.component.html',
  styleUrls: ['../div/div.component.scss', './button.component.scss']
})
export class ButtonComponent {
  @Input() 
  comp!: Comp
}
