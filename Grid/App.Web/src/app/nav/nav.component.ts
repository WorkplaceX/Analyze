import { Component, Input } from '@angular/core';
import { Comp } from '../data.service';

@Component({
  selector: '[app-nav]',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent {
  @Input()
  comp!: Comp
}
