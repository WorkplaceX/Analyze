import { Comp } from "./data.service";

export class Data {
  public static comp: Comp = {
    rootRequestCount: 0,
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
                switchGroup: "main"
              },
              {
                name: "Dropdown",
                cssClass: "flex",
                cssClassSmall: "flex dropdown",
                cssStyle: "flex: 1;", // stretch
                cssStyleSmall: "flex-direction: column; width: 100%; display: none;", // flex vertical; dropdown stretch;
                switchCssStyle: "display: block",
                list: [
                  {
                    cssClass: "flex-item",
                    text: "Home",
                    activePath: "home/",
                    activePathGroup: "nav",
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
                        activePathGroup: "nav",
                        isActiveDisable: true,
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
                            activePathGroup: "nav",
                          },
                          {
                            cssClass: "flex-item",
                            text: "Contact",
                            href: "/contact",
                            type: "anchor",
                            activePath: "product/contact/",
                            activePathGroup: "nav",
                          }
                        ]
                      }
                    ]
                  },
                  {
                    cssClass: "flex-item",
                    text: "About",
                    activePath: "about/",
                    activePathGroup: "nav",
                    href: "/about",
                    type: "anchor"
                  },
                  {
                    cssClass: "flex-item",
                    cssStyle: "margin-left: auto; background: green",
                    cssStyleSmall: "",
                    hoverCssClass: "hover",
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
                switchGroup: "nav",
                isSwitchResetBreakpoint: true,
                isSwitchResetSwitch: true,
                switchNames: ["Logo", "Dropdown"]
              },
            ]
          },
        ]
      },
      {
        activeCssClass: "grid",
        activeAncestorCssClass: "grid",
        cssClass: "hide",
        cssStyle: "grid-template-columns: 1fr 2fr",
        cssStyleMedium: "grid-template-columns: 1fr",
        activePath: "product/contact/",
        activePathGroup: "nav",
        list: [
          {
            cssClass: "grid-item",
            type: "anchor",
            text: "Email",
            activePath: "product/contact/install",
            activePathGroup: "nav",
          },
          {
            cssClass: "grid-item",
            type: "html",
            text: "<h1>Contact</h1><p>Street</p>"
          },
        ]
      },
      {
        activeCssClass: "grid",
        activeAncestorCssClass: "grid",
        cssClass: "hide",
        cssStyle: "grid-template-columns: 1fr 2fr",
        cssStyleMedium: "grid-template-columns: 1fr",
        activePath: "product/version/",
        activePathGroup: "nav",
        list: [
          {
            cssClass: "grid-item",
            type: "anchor",
            text: "Install",
            activePath: "product/version/install",
            activePathGroup: "nav",
          },
          {
            cssClass: "grid-item",
            type: "html",
            text: "<h1>Version</h1><p>Install the application <b>on a pc</b> to edit content and publish it.</p>"
          },
        ]
      },
      {
        activeCssClass: "block",
        cssClass: "hide",
        text: "<h1>About</h1><p>About this site</p>",
        type: "html",
        activePath: "about/",
        activePathGroup: "nav",
      },
      {
        cssClass: "grid",
        cssStyle: "grid-template-columns: 1fr 1fr 1fr",
        cssStyleMedium: "grid-template-columns: 1fr 1fr",
        cssStyleSmall: "grid-template-columns: 1fr",
        list: [
          {
            type: "html",
            text: "Item1",
            cssClass: "grid-item",
            hoverCssClass: "hover",
            activePath: "a/",
            activePathGroup: "content",
            activeCssClass: "active"
          }, {
            type: "html",
            text: "Item2",
            cssClass: "grid-item",
            hoverCssClass: "hover",
            activePath: "b/",
            activePathGroup: "content",
          }, {
            type: "html",
            text: "Item3",
            cssClass: "grid-item",
            hoverCssClass: "hover",
            activePath: "c/",
            activePathGroup: "content",
          }, {
            cssClass: "grid",
            cssStyle: "grid-template-columns: 1fr 1fr",
            cssStyleSmall: "grid-template-columns: 1fr",
            list: [
              {
                text: "ItemA",
                cssClass: "grid-item",
                type: "html",
                hoverCssClass: "hover",
                isSwitchResetSwitch: true,
                switchGroup: "x"
              }, {
                text: "ItemB",
                cssClass: "grid-item",
                type: "html",
                switchCssClass: "switch",
                hoverCssClass: "hover",
                switchGroup: "x"
              }, {
                text: "ItemC",
                href: "/abc/",
                cssClass: "grid-item",
                hoverCssClass: "hover",
                type: "anchor",
                switchGroup: "x"
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