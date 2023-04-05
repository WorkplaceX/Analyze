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

  @HostListener('mouseenter', ['$event'])
  handleMouseEnter(event: any) {
    this.comp.isHover = true
    this.dataService.cssUpdate(this.comp)
  }

  @HostListener('mouseleave', ['$event'])
  handleMouseleave(event: any) {
    this.comp.isHover = undefined
    if (this.comp.isSwitchLeave) {
      this.comp.isSwitch = undefined
    }
    this.dataService.cssUpdate(this.comp)
  }

  @HostListener('click', ['$event'])
  onClick(event: any) {
    event.stopPropagation()
    event.stopImmediatePropagation()
    if (this.comp.isSwitch == undefined) {
      this.comp.isSwitch = true
    } else {
      this.comp.isSwitch = undefined
    }
    if (this.comp.switchNames) {
      this.switchName(this.dataService.comp, this.comp.switchNames)
    }
    if (this.comp.activePath) {
      this.active(this.dataService.comp, this.comp.activePath)
    }
    this.dataService.cssUpdate(this.comp)
  }

  active(comp: Comp, activePath: string) {
    if (comp.activePath) {
      comp.isActive = undefined
      comp.isActiveParent = undefined
      if (comp.activePath == activePath) {
        comp.isActive = true
      } else {
        if (activePath.startsWith(comp.activePath)) {
          comp.isActiveParent = true
        }
      }
      this.dataService.cssUpdate(comp)
    }
    comp.list?.forEach((item) => this.active(item, activePath))
  }

  switchName(comp: Comp, switchNames?: string[]) {
    if (switchNames?.includes(comp.name!)) {
      if (comp.isSwitch == undefined) {
        comp.isSwitch = true
      } else {
        comp.isSwitch = undefined
      }
      this.dataService.cssUpdate(comp)
    }
    comp.list?.forEach((item) => this.switchName(item, switchNames))
  }
}
