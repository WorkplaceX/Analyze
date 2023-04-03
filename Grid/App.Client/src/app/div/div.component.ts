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
  handleMousemove(event: any) {
    this.json.isHover = undefined
    this.dataService.cssUpdate(this.json)
  }  
}
