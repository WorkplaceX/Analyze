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
    text: "App",
    requestCount: 0,
    type: "Button",
    list: [
      {
        text: "NavX8",
        type: "Nav",
        list: [
          {
            text: "Nav1",
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
  
  cssStyleCurrent?: string
  
  /** Href for AnchorComponent. */
  href?: string
  
  isActive?: boolean
  
  requestCount?: number
}
