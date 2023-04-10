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

  @HostListener('click', ['$event']) // See also dropdown:active to hide after click. Use mousedown event. Event click is too slow!
  click(event: any) {
    event.stopPropagation()
    event.stopImmediatePropagation()
    DataService.clickIsSwitch(this.comp, this.dataService.compRoot)
    DataService.clickIsActive(this.comp, this.dataService.compRoot)
  }

  @HostListener('mouseenter')
  mouseenter() {
    DataService.mouseenter(this.comp, this.dataService.compRoot)
  }

  @HostListener('mouseleave')
  mouseleave() {
    DataService.mouseleave(this.comp, this.dataService.compRoot)
  }
}
