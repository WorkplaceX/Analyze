import { Component, HostListener, Input } from '@angular/core';
import { DataService, Comp } from '../data.service';

@Component({
  selector: '[app-div]',
  templateUrl: './div.component.html',
  styleUrls: ['./div.component.scss']
})
export class DivComponent {
  constructor(private dataService: DataService) {

  }

  @Input()
  comp!: Comp

  @HostListener('mousedown', ['$event']) // See also dropdown:active to hide after click. Use mousedown event. Event click is too slow!
  mousedown(event: any) {
    event.stopPropagation()
    event.stopImmediatePropagation()
    DataService.click(this.comp, this.dataService.compRoot)
  }

  @HostListener('mouseenter', ['$event'])
  mouseenter(event: any) {
    this.comp.isHover = true
    DataService.cssUpdate(this.comp, this.dataService.compRoot)
  }

  @HostListener('mouseleave', ['$event'])
  mouseleave(event: any) {
    this.comp.isHover = undefined
    DataService.cssUpdate(this.comp, this.dataService.compRoot)
  }
}
