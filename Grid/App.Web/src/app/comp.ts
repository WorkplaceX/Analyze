import { Comp } from "./data.service";

export class Data {
  public static comp: Comp = {
    rootRequestCount: 0,
    rootIsDisableHoverSwitch: false,
    list: [
      {
        type: "nav",
        list: [
          {
            cssClass: "flex",
            list: [
              {
                cssClass: "flex-item",
                name: "logo",
                text: "Logo",
                type: "html",
                switchGroup: "main"
              },
              {
                name: "dropdownMain",
                switchGroup: "nav",
                isSwitchReset: true,
                isSwitchDisable: true,
                isSwitchResetBreakpoint: true,
                cssClass: "flex",
                cssClassSmall: "hide",
                cssStyle: "flex: 1;", // stretch
                cssStyleSmall: "flex-direction: column; width: 100%;", // flex vertical; dropdown stretch;
                switchOnCssClass: "flex dropdown block",
                list: [
                  {
                    cssClass: "flex-item",
                    text: "Home",
                    activePath: "home/",
                    activeGroup: "nav",
                    href: "/",
                    type: "anchor"
                  },
                  {
                    cssClass: "flex-item",
                    list: [
                      {
                        debug: "my",
                        cssClass: "flex-item",
                        text: "Product",
                        activePath: "product/",
                        activeGroup: "nav",
                        isActiveDisable: true,
                        isActiveSwitchHover: true,
                        isActiveSwitchHoverMedium: true,
                        isSwitchResetHover: true,
                        isSwitchResetHoverMedium: true,
                        switchNames: ["dropdownProduct"],
                        switchGroup: "nav",
                        href: "/product",
                        type: "anchor",
                      },
                      {
                        name: "dropdownProduct",
                        switchGroup: "nav",
                        isSwitchResetHover: true,
                        isSwitchResetBreakpoint: true,
                        cssClassSmall: "flex-item hide",
                        cssClass: "flex-item dropdown hide",
                        switchOnCssClass: "block",
                        list: [
                          {
                            cssClass: "flex-item",
                            text: "Version",
                            href: "/version",
                            type: "anchor",
                            activePath: "product/version/",
                            activeGroup: "nav",
                          },
                          {
                            cssClass: "flex-item",
                            text: "Contact",
                            href: "/contact",
                            type: "anchor",
                            activePath: "product/contact/",
                            activeGroup: "nav",
                          }
                        ]
                      }
                    ]
                  },
                  {
                    cssClass: "flex-item",
                    text: "About",
                    activePath: "about/",
                    activeGroup: "nav",
                    href: "/about",
                    type: "anchor",
                  },
                  {
                    cssClass: "flex-item",
                    cssStyle: "margin-left: auto; background: green",
                    cssStyleSmall: "margin-left: initial;",
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
                switchNames: ["logo", "dropdownMain"]
              },
            ]
          },
        ]
      },
      {
        activeOnCssClass: "grid",
        activeAncestorCssClass: "grid",
        cssClass: "hide",
        cssStyle: "grid-template-columns: 1fr 2fr",
        cssStyleMedium: "grid-template-columns: 1fr",
        activePath: "product/contact/",
        activeGroup: "nav",
        list: [
          {
            cssClass: "grid-item",
            type: "anchor",
            text: "Email",
            activePath: "product/contact/install",
            activeGroup: "nav",
          },
          {
            cssClass: "grid-item",
            type: "html",
            text: "<h1>Contact</h1><p>Street</p>"
          },
        ]
      },
      {
        activeOnCssClass: "grid",
        activeAncestorCssClass: "grid",
        cssClass: "hide",
        cssStyle: "grid-template-columns: 1fr 2fr",
        cssStyleMedium: "grid-template-columns: 1fr",
        activePath: "product/version/",
        activeGroup: "nav",
        list: [
          {
            cssClass: "grid-item",
            type: "anchor",
            text: "Install",
            activePath: "product/version/install",
            activeGroup: "nav",
          },
          {
            cssClass: "grid-item",
            type: "html",
            text: "<h1>Version</h1><p>Install the application <b>on a pc</b> to edit content and publish it.</p>"
          },
        ]
      },
      {
        cssClass: "hide",
        activeOnCssClass: "block",
        text: "<h1>About</h1><p>About this site</p>",
        type: "html",
        activePath: "about/",
        activeGroup: "nav",
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
            activeCssClass: "",
            activePath: "a/",
            activeGroup: "content",
          }, {
            type: "html",
            text: "Item2",
            cssClass: "grid-item",
            activeCssClass: "",
            activePath: "b/",
            activeGroup: "content",
          }, {
            type: "html",
            text: "Item3",
            cssClass: "grid-item",
            activeCssClass: "",
            activePath: "c/",
            activeGroup: "content",
          }, {
            cssClass: "grid",
            cssStyle: "grid-template-columns: 1fr 1fr",
            cssStyleSmall: "grid-template-columns: 1fr",
            list: [
              {
                text: "ItemA",
                cssClass: "grid-item",
                type: "html",
                isSwitchReset: true,
                switchGroup: "x"
              }, {
                text: "ItemB",
                cssClass: "grid-item",
                type: "html",
                switchGroup: "x"
              }, {
                text: "ItemC",
                href: "/abc/",
                cssClass: "grid-item",
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
                    type: "html",
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