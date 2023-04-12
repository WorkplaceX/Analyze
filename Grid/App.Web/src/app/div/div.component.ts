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

  compDebug() {
    return (this.comp.isSwitch ? "S " : "") + (this.comp.isActive ? "A " : "") + (this.comp.isActiveAncestor ? "P " : "") + (this.comp.isActiveDescendent ? "D " : "") + (this.comp.isHover ? "H" : "")
  }

  @HostListener('click', ['$event'])
  click(event: any) {
    event.stopPropagation()
    event.stopImmediatePropagation()
    DataService.click(this.comp, this.dataService.compRoot)
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
