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

  ngOnInit() {
    this.responsive(this.json, window.innerWidth)
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    this.responsive(this.json, event.target.innerWidth)
  }

  responsive(json: Json, screenWidth: number) {
    json.cssStyleCurrent = json.cssStyle
    if (screenWidth < 1024) {
      json.cssStyleCurrent = json.cssStyleMedium
    }
    if (screenWidth < 768) {
      json.cssStyleCurrent = json.cssStyleSmall
    }
    if (json.list) {
      json.list.forEach((item) => this.responsive(item, screenWidth))
    }
  }

  json: Json

  onClick() {
    this.dataService.update()
  }
}
