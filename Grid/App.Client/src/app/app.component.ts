import { Component, HostListener } from '@angular/core';
import { DataService, Json } from './data.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private dataService: DataService) {
    this.json = dataService.json
  }

  json: Json

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
