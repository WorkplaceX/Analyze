import { Json } from "./data.service";

export class Data {
  public static json: Json = {
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
                text: "Logo",
                type: "Html"
              },
              {
                cssClass: "flex-item nav-item",
                text: "Home",
                href: "/",
                type: "Anchor"
              },
              {
                cssClass: "flex-item dropdown-hover",
                list: [
                  {
                    cssClass: "flex-item",
                    text: "About",
                    href: "/about",
                    type: "Anchor",
                  },
                  {
                    cssClass: "flex-item dropdown",
                    cssStyleSmall: "",
                    list: [
                      {
                        cssClass: "flex-item",
                        text: "Version",
                        href: "/version",
                        type: "Anchor",
                      },
                      {
                        cssClass: "flex-item",
                        text: "Contact",
                        href: "/contact",
                        type: "Anchor",
                      }
                    ]
                  }
                ]
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
              {
                cssClass: "flex-item",
                text: "Hamburger",
                type: "Html"
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
}