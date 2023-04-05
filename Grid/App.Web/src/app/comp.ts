import { Comp } from "./data.service";

export class Data {
  public static comp: Comp = {
    requestCount: 0,
    list: [
      {
        type: "Nav",
        list: [
          {
            cssClass: "flex",
            list: [
              {
                name: "Logo",
                cssClass: "flex-item",
                text: "Logo",
                type: "Html",
                //isSwitchLeave: true,
                cssStyleSwitch: "background-color: green"
              },
              {
                name: "Dropdown",
                cssClass: "flex",
                cssClassSmall: "flex dropdown",
                cssStyle: "flex: 1;", // stretch
                cssStyleSmall: "flex-direction: column; width: 100%; display: none;", // flex vertical; dropdown stretch;
                cssStyleSwitch: "display: block",
                list: [
                  {
                    cssClass: "flex-item",
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
                        cssClassSmall: "flex-item",
                        cssStyleSmall: "",
                        list: [
                          {
                            cssClass: "flex-item",
                            text: "Version",
                            href: "/version",
                            type: "Anchor",
                            activePath: "about/",
                            cssClassActive: "active",
                            cssClassActiveParent: "active-parent"
                          },
                          {
                            cssClass: "flex-item",
                            text: "Contact",
                            href: "/contact",
                            type: "Anchor",
                            activePath: "about/contact",
                            cssClassActive: "active",
                            cssClassActiveParent: "active-parent"
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
                ]
              },
              {
                cssClass: "flex-item",
                cssStyle: "margin-left: auto; display: none",
                cssStyleSmall: "margin-left: auto; display: block",
                text: "Hamburger",
                type: "Html",
                switchNames: ["Logo", "Dropdown"]
              },
            ]
          },
        ]
      },
      {
        cssClass: "grid",
        cssStyle: "grid-template-columns: 1fr 2fr",
        cssStyleMedium: "grid-template-columns: 1fr",
        list: [
          {
            cssClass: "grid-item",
            type: "Anchor",
            text: "Install",
            activePath: "about/contact/install",
            cssClassActive: "active"
          },
          {
            cssClass: "grid-item",
            type: "Html",
            text: "<h1>Hello World</h1><p>Install the application <b>on a pc</b> to edit content and publish it.</p>"
          },
        ]
      },
      {
        cssClass: "grid",
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