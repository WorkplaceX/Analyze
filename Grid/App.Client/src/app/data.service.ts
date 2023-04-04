import { Injectable } from '@angular/core';
import { Data } from './json';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor() { }

  public json2: Json = {
    text: "App",
    cssClass: "MyApp",
    list: [
      {
        type: "Nav",
        cssClass: "MyNav",
        list: [
          {
            cssClass: "flex MyFlex",
            list: [
              {
                cssClass: "flex-item MyFlexItem",
                text: "Home",
                href: "/",
                type: "Anchor"
              },
              {
                cssClass: "flex-item MyFlexItem",
                text: "About",
                href: "/about",
                type: "Anchor"
              },
              {
                cssClass: "MyNavClass",
                type: "Nav",
                list: [

                ]
              }
            ]
          }
        ]
      },
    ]
  }

  public json: Json = Data.json

  public resize(screenWidth: number) {
    this.json.breakPoint = undefined;
    if (screenWidth < 1024) {
      this.json.breakPoint = "Medium"
    }
    if (screenWidth < 768) {
      this.json.breakPoint = "Small"
    }
    this.cssUpdateRecursive(this.json)
  }

  public cssUpdate(json: Json) {
    json.cssStyleCurrent = json.cssStyle
    if (this.json.breakPoint == "Medium" && json.cssStyleMedium != null) {
      json.cssStyleCurrent = json.cssStyleMedium
    }
    if (this.json.breakPoint == "Small" && json.cssStyleSmall != null) {
      json.cssStyleCurrent = json.cssStyleSmall
    }
    if (json.isHover && json.cssStyleHover != null) {
      if (json.cssStyleCurrent && !json.cssStyleCurrent.endsWith(";")) {
        json.cssStyleCurrent += "; "
      }
      json.cssStyleCurrent += json.cssStyleHover
    }
  }

  cssUpdateRecursive(json: Json) {
    this.cssUpdate(json)
    if (json.list) {
      json.list.forEach((item) => this.cssUpdateRecursive(item))
    }
  }

  public update() {
    this.json.requestCount! += 1
  }
}

/** Component is either a DivComponent or a content component like AnchorComponent. */
export interface Json {
  /** Text for content component. */
  text?: string

  /** List of child components. If null use field type to select content component. */
  list?: Json[]

  /** Type of content component. If null, DivComponent is rendered. */
  type?: string

  /** CssClass of DivComponent. Not applicable to root element. */
  cssClass?: string

  /** CssStyle of DivComponent. Not applicable to root element. */
  cssStyle?: string

  cssStyleSmall?: string

  cssStyleMedium?: string

  cssStyleHover?: string

  cssStyleCurrent?: string

  isHover?: boolean

  /** BreakPoint (null, Medium, Small). Root element only. */
  breakPoint?: string

  /** Href for AnchorComponent. */
  href?: string

  isActive?: boolean

  requestCount?: number
}
