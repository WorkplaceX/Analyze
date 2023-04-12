import { Component, HostListener } from '@angular/core';
import { DataService, Comp } from './data.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./div/div.component.scss', './app.component.scss']
})
export class AppComponent {
  constructor(private dataService: DataService) {
    this.compRoot = dataService.compRoot
  }

  compRoot: Comp

  ngOnInit() {
    DataService.resize(window.innerWidth, this.dataService.compRoot)
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    DataService.resize(event.target.innerWidth, this.dataService.compRoot)
  }

  onClick() {
    this.dataService.update()
  }
}
