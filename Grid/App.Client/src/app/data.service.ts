import { Injectable } from '@angular/core';

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

  public json: Json = {
    requestCount: 0,
    list: [
      {
        type: "Nav",
        list: [
          {
            cssClass: "flex",
            cssStyleSmall: "flex-direction: column",
            list: [
              {
                cssClass: "flex-item nav-item",
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
              {
                cssClass: "flex-item",
                cssStyle: "margin-left: auto; background-color: green",
                cssStyleSmall: "",
                cssStyleHover: "background-color: lightgreen",
                text: "Sign In",
                href: "/about",
                type: "Anchor"
              },
              {
                cssClass: "flex-item",
                text: "Sign Out",
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
          type: "Html"
        }, {
          text: "Div12",
          type: "Html"
        }]
      }, {
        text: "Div2",
        type: "Html"
      }, {
        cssClass: "grid mygrid",
        cssStyle: "grid-template-columns: 1fr 1fr 1fr",
        cssStyleMedium: "grid-template-columns: 1fr 1fr",
        cssStyleSmall: "grid-template-columns: 1fr",
        list: [
          {
            text: "Item1A",
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
            cssStyleSmall: "grid-template-columns: 1fr",
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
      },
      {
        text: "<h1>Hello World</h1>",
        type: "Html"
      },
      {
        text: "Click",
        type: "Button"
      },
      {
        type: "Footer",
        list: [
          {
            cssClass: "grid",
            cssStyle: "grid-template-columns: 1fr 1fr",
            cssStyleSmall: "grid-template-columns: 1fr",
            list: [
              {
                cssClass: "grid-item",
                list: [
                  {
                    text: "ABOUT",
                    cssClass: "grid-item",
                    type: "Html"
                  },
                  {
                    cssClass: "grid-item",
                    text: "WorkplaceX is an initiative to simplify and standardize the development of business applications. It is based on ASP.NET Core, Angular, Bootstrap, Bulma and SQL Server.",
                    type: "Html"
                  },
                ]
              },
              {
                cssClass: "grid-item",
                list: [
                  {
                    text: "QUICK LINKS",
                    cssClass: "grid-item",
                    type: "Html"
                  },
                  {
                    cssClass: "grid-item",
                    text: "<a href='/'>Home</a>",
                    type: "Html"
                  },
                ]
              },
              {
                cssClass: "grid-item",
                text: "Copyright © 2022 All Rights Reserved by WorkplaceX.org",
                type: "Html"
              },
              {
                cssClass: "grid-item",
                text: "<a href='/'>Twitter</a><a href='/'>Youtube</a>",
                type: "Html"
              },
            ]
          }
        ]
      },
    ]
  }

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
