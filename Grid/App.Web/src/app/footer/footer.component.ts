import { Component, Input } from '@angular/core';
import { Json } from '../data.service';

@Component({
  selector: '[app-footer]',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent {
  @Input()
  json!: Json
}
