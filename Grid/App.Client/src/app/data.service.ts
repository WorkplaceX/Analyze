import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor() { }

  public json: Json = {
    text: "App",
    requestCount: 0,
    list: [
      {
        type: "Nav",
        list: [
          {
            cssClass: "flex",
            list: [
              {
                cssClass: "flex-item",
                text: "Home",
                href: "/",
                type: "Anchor"
              },
              {
                cssClass: "flex-item",
                text: "About",
                href: "/about",
                type: "Anchor"
              },
            ]
          }
        ]
      }, {
        text: "Div1",
        list: [{
          text: "Div11",
        }, {
          text: "Div12",
        }]
      }, {
        text: "Div2",
      }, {
        cssClass: "grid",
        cssStyle: "grid-template-columns: 1fr 1fr 1fr",
        cssStyleMedium: "grid-template-columns: 1fr 1fr",
        cssStyleSmall: "grid-template-columns: 1fr",
        list: [
          {
            text: "Item1",
            cssClass: "grid-item",
            type: "Html"
          }, {
            text: "Item2",
            cssClass: "grid-item",
            type: "Html"
          }, {
            text: "Item3",
            cssClass: "grid-item",
            type: "Html"
          }, {
            cssClass: "grid",
            cssStyle: "grid-template-columns: 1fr 1fr",
            list: [
              {
                text: "ItemA",
                cssClass: "grid-item",
                type: "Html"
              }, {
                text: "ItemB",
                cssClass: "grid-item",
                type: "Html"
              }, {
                text: "ItemC",
                href: "/abc/",
                cssClass: "grid-item",
                type: "Anchor"
              }
            ]
          }
        ]
      }, {
        text: "<h1>Hello World</h1>",
        type: "Html"
      }, {
        text: "Click",
        type: "Button"
      }, {
        text: "Footer",
        type: "Footer",
      }]
  }

  public update() {
    this.json.requestCount! += 1
  }
}

export interface Json {
  text?: string
  list?: Json[]
  type?: string
  cssClass?: string
  cssStyle?: string
  cssStyleSmall?: string
  cssStyleMedium?: string
  cssStyleCurrent?: string
  href?: string
  isActive?: boolean
  requestCount?: number
}
