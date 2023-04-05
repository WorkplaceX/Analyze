import { Component, Input } from '@angular/core';
import { Comp } from '../data.service';

@Component({
  selector: '[app-footer]',
  templateUrl: './footer.component.html',
  styleUrls: ['../div/div.component.scss', './footer.component.scss']
})
export class FooterComponent {
  @Input()
  comp!: Comp
}
