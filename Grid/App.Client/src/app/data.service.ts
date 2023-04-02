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
        contentType: "Nav",
        list: [
          {
            cssClass: "my-container",
            list: [
              {
                cssClass: "my-item",
                text: "Home",
                href: "/",
                contentType: "Anchor"
              },
              {
                cssClass: "my-item",
                text: "About",
                href: "/about",
                contentType: "Anchor"
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
        cssClass: "my-container",
        cssStyle: "grid-template-columns: 1fr 1fr 1fr",
        cssStyleMedium: "grid-template-columns: 1fr 1fr",
        cssStyleSmall: "grid-template-columns: 1fr",
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
          }, {
            cssClass: "my-container",
            cssStyle: "grid-template-columns: 1fr 1fr 1fr",
            list: [
              {
                text: "ItemA",
                cssClass: "my-item",
                contentType: "Html"
              }, {
                text: "ItemB",
                cssClass: "my-item",
                contentType: "Html"
              }, {
                text: "ItemC",
                href: "/abc/",
                cssClass: "my-item",
                contentType: "Anchor"
              }
            ]
          }
        ]
      }, {
        text: "<h1>Hello World</h1>",
        contentType: "Html"
      }, {
        text: "Click",
        contentType: "Button"
      }, {
        text: "Footer",
        contentType: "Footer",
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
  cssStyleSmall?: string
  cssStyleMedium?: string
  cssStyleCurrent?: string
  href?: string
  isActive?: boolean
  requestCount?: number
}
