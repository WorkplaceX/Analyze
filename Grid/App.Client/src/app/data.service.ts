import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor() { }

  public json: Json = {
    text: "App",
    requestCount: 0,
    list: [{
      text: "Div1",
      list: [{
        text: "Div11",
      },{
        text: "Div12",
      }]
    }, {
      text: "Div2",
    }, {
      text: "Row",
      cssClass: "my-container",
      cssStyle: "grid-template-columns: auto auto",
      list: [
        {
          text: "Item1",
          cssClass: "my-item",
          contentType: "Html"
        }, {
          text: "Item2",
          cssClass: "my-item",
          contentType: "Html"
        }, {
          text: "Item3",
          cssClass: "my-item",
          contentType: "Html"
        }
      ]
    }, {
      text: "<h1>Hello World</h1>",
      contentType: "Html"
    }, {
      text: "Click",
      contentType: "Button"
    }]
  }

  public update() {
    this.json.requestCount! += 1
  }
}

export interface Json {
  text?: string
  list?: Json[]
  contentType?: string
  cssClass?: string
  cssStyle?: string
  requestCount?: number
}
