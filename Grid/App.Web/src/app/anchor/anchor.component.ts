import { Component, Input } from '@angular/core';
import { Comp } from '../data.service';

@Component({
  selector: '[app-anchor]',
  templateUrl: './anchor.component.html',
  styleUrls: ['./anchor.component.scss']
})
export class AnchorComponent {
  @Input() 
  comp!: Comp
}
