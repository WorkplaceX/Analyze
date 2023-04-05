import { Component, HostListener } from '@angular/core';
import { DataService, Comp } from './data.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private dataService: DataService) {
    this.comp = dataService.comp
  }

  comp: Comp

  ngOnInit() {
    this.dataService.resize(window.innerWidth)
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    this.dataService.resize(event.target.innerWidth)
  }

  onClick() {
    this.dataService.update()
  }
}
