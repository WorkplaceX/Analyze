import { Component, HostListener, Input } from '@angular/core';
import { DataService, Json } from '../data.service';

@Component({
  selector: '[app-div]',
  templateUrl: './div.component.html',
  styleUrls: ['./div.component.scss']
})
export class DivComponent {
  constructor(private dataService: DataService) {

  }

  @Input()
  json!: Json

  @HostListener('mouseenter', ['$event'])
  handleMouseEnter(event: any) {
    this.json.isHover = true
    this.dataService.cssUpdate(this.json)
  }

  @HostListener('mouseleave', ['$event'])
  handleMouseleave(event: any) {
    this.json.isHover = undefined
    if (this.json.isSwitchLeave) {
      this.json.isSwitch = undefined
    }
    this.dataService.cssUpdate(this.json)
  }

  @HostListener('click', ['$event'])
  onClick(event: any) {
    event.stopPropagation()
    event.stopImmediatePropagation()
    if (this.json.isSwitch == undefined) {
      this.json.isSwitch = true
    } else {
      this.json.isSwitch = undefined
    }
    if (this.json.switchNames) {
      this.switchName(this.dataService.json, this.json.switchNames)
    }
    this.dataService.cssUpdate(this.json)
  }

  switchName(json: Json, switchNames?: string[]) {
    if (switchNames?.includes(json.name!)) {
      if (json.isSwitch == undefined) {
        json.isSwitch = true
      } else {
        json.isSwitch = undefined
      }
      this.dataService.cssUpdate(json)
    }
    json.list?.forEach((item) => this.switchName(item, switchNames))
  }
}
