import { Comp } from "./data.service";

export class Data {
  public static comp: Comp = {
    requestCount: 0,
    list: [
      {
        type: "nav",
        list: [
          {
            cssClass: "flex",
            list: [
              {
                name: "Logo",
                cssClass: "flex-item",
                text: "Logo",
                type: "html",
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
                    activePath: "home/",
                    cssClassActive: "active",
                    cssClassActiveParent: "active-parent",
                    href: "/",
                    type: "anchor"
                  },
                  {
                    cssClass: "flex-item dropdown-hover",
                    list: [
                      {
                        cssClass: "flex-item",
                        text: "Product",
                        activePath: "product/",
                        isActiveDisable: true,
                        cssClassActive: "active",
                        cssClassActiveParent: "active-parent",
                        href: "/product",
                        type: "anchor",
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
                            type: "anchor",
                            activePath: "product/version/",
                            cssClassActive: "active",
                            cssClassActiveParent: "active-parent"
                          },
                          {
                            cssClass: "flex-item",
                            text: "Contact",
                            href: "/contact",
                            type: "anchor",
                            activePath: "product/contact/",
                            cssClassActive: "active",
                            cssClassActiveParent: "active-parent"
                          }
                        ]
                      }
                    ]
                  },
                  {
                    cssClass: "flex-item",
                    text: "About",
                    activePath: "about/",
                    cssClassActive: "active",
                    cssClassActiveParent: "active-parent",
                    href: "/about",
                    type: "anchor"
                  },
                  {
                    cssClass: "flex-item",
                    cssStyle: "margin-left: auto; background-color: green",
                    cssStyleSmall: "",
                    cssStyleHover: "background-color: lightgreen",
                    text: "Sign In",
                    href: "/product",
                    type: "anchor"
                  },
                  {
                    cssClass: "flex-item",
                    text: "Sign Out",
                    href: "/product",
                    type: "anchor"
                  },
                ]
              },
              {
                cssClass: "flex-item",
                cssStyle: "margin-left: auto; display: none",
                cssStyleSmall: "margin-left: auto; display: block",
                text: "Hamburger",
                type: "html",
                switchNames: ["Logo", "Dropdown"]
              },
            ]
          },
        ]
      },
      {
        cssClassActive: "grid",
        cssClassActiveParent: "grid",
        cssClass: "hide",
        cssStyle: "grid-template-columns: 1fr 2fr",
        cssStyleMedium: "grid-template-columns: 1fr",
        activePath: "product/contact/",
        list: [
          {
            cssClass: "grid-item",
            type: "anchor",
            text: "Email",
            activePath: "product/contact/install",
            cssClassActive: "active"
          },
          {
            cssClass: "grid-item",
            type: "html",
            text: "<h1>Contact</h1><p>Street</p>"
          },
        ]
      },
      {
        cssClassActive: "grid",
        cssClassActiveParent: "grid",
        cssClass: "hide",
        cssStyle: "grid-template-columns: 1fr 2fr",
        cssStyleMedium: "grid-template-columns: 1fr",
        activePath: "product/version/",
        list: [
          {
            cssClass: "grid-item",
            type: "anchor",
            text: "Install",
            activePath: "product/version/install",
            cssClassActive: "active"
          },
          {
            cssClass: "grid-item",
            type: "html",
            text: "<h1>Version</h1><p>Install the application <b>on a pc</b> to edit content and publish it.</p>"
          },
        ]
      },
      {
        cssClassActive: "block",
        cssClass: "hide",
        text: "<h1>About</h1><p>About this site</p>",
        type: "html",
        activePath: "about/",
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
            type: "html"
          }, {
            text: "Item2",
            cssClass: "grid-item",
            type: "html"
          }, {
            text: "Item3",
            cssClass: "grid-item",
            type: "html"
          }, {
            cssClass: "grid",
            cssStyle: "grid-template-columns: 1fr 1fr",
            cssStyleSmall: "grid-template-columns: 1fr",
            list: [
              {
                text: "ItemA",
                cssClass: "grid-item",
                type: "html"
              }, {
                text: "ItemB",
                cssClass: "grid-item",
                type: "html"
              }, {
                text: "ItemC",
                href: "/abc/",
                cssClass: "grid-item",
                type: "anchor"
              }
            ]
          }
        ]
      },
      {
        type: "footer",
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
                    type: "html"
                  },
                  {
                    cssClass: "grid-item",
                    text: "WorkplaceX is an initiative to simplify and standardize the development of business applications. It is based on ASP.NET Core, Angular, Bootstrap, Bulma and SQL Server.",
                    type: "html"
                  },
                ]
              },
              {
                cssClass: "grid-item",
                list: [
                  {
                    text: "QUICK LINKS",
                    cssClass: "grid-item",
                    type: "html"
                  },
                  {
                    cssClass: "grid-item",
                    text: "<a href='/'>Home</a>",
                    type: "html"
                  },
                ]
              },
              {
                cssClass: "grid-item",
                text: "Copyright © 2022 All Rights Reserved by WorkplaceX.org",
                type: "html"
              },
              {
                cssClass: "grid-item",
                text: "<a href='/'>Twitter</a><a href='/'>Youtube</a>",
                type: "html"
              },
            ]
          }
        ]
      },
    ]
  }
}