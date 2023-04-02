import { Component } from '@angular/core';
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

  onClick() {
    this.dataService.update()
  }
}
