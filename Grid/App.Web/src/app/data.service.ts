import { Injectable } from '@angular/core';
import { Data } from './comp';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor() {
    DataService.transform(this.compRoot, this.compRoot)
  }

  compRoot: Comp = Data.comp

  static transform(comp: Comp, compRoot: Comp) {
    if (!comp.list) {
      if (comp.switchGroup) {
        comp.switchCssClass ??= "switch"
        comp.switchHoverCssClass ??= "switch-hover"
        comp.switchOnCssClass ??= "switch-on"
        comp.switchOnHoverCssClass ??= "switch-on-hover"
      }
      if (comp.activeGroup) {
        comp.activeCssClass ??= "active"
        comp.activeHoverCssClass ??= "active-hover"
        comp.activeOnCssClass ??= "active-on"
        comp.activeOnHoverCssClass ??= "active-on-hover"
        comp.activeAncestorCssClass ??= "active-ancestor"
        comp.activeAncestorHoverCssClass ??= "active-ancestor-hover"
      }
    }
    comp.list?.forEach((item) => this.transform(item, compRoot))
  }

  static click(comp: Comp, compRoot: Comp) {
    DataService.clickIsSwitch(comp, compRoot)
    DataService.clickIsActive(comp, compRoot)
    this.cssUpdateAll(compRoot, compRoot)
  }

  static clickIsSwitch(comp: Comp, compRoot: Comp) {
    if (comp.switchGroup && !comp.isSwitchDisable) {
      this.isSwitchToggle(comp)
      if (comp.switchNames) {
        this.isSwitchToggleNamesAll(compRoot, compRoot, comp)
      }
      if (!comp.isActiveDisable) {
        this.isSwitchResetAll(compRoot, compRoot, comp)
      }
      this.isActiveUpdateAll(compRoot, compRoot, comp)
    }
  }

  static clickIsActive(comp: Comp, compRoot: Comp) {
    // Active
    if (comp.activeGroup && !comp.isActiveDisable) {
      // Close dropdown
      this.isSwitchResetAll(compRoot, compRoot, comp)
      if (compRoot.rootIsEnableHoverSwitch) {
        this.isSwitchResetHoverAll(compRoot, compRoot, comp)
      }
      this.isActiveUpdateAll(compRoot, compRoot, comp)
    }
  }

  static mouseenter(comp: Comp, compRoot: Comp) {
    // Hover
    if (compRoot.rootIsEnableHover) {
      comp.isHover = true
    }
    // HoverSwitch
    if (compRoot.rootIsEnableHoverSwitch) {
      let isActiveSwitchHover = (compRoot.rootBreakpoint == null && comp.isActiveSwitchHover) || (compRoot.rootBreakpoint == "medium" && comp.isActiveSwitchHoverMedium) || (compRoot.rootBreakpoint == "small" && comp.isActiveSwitchHoverSmall)
      if (comp.activeGroup && isActiveSwitchHover && comp.isActiveDisable) {
        if (!comp.isSwitch) {
          this.clickIsSwitch(comp, compRoot)
        }
      }
      if (!comp.isActiveDescendent) {
        this.isSwitchResetHoverAll(compRoot, compRoot, comp)
      }
    }
    this.cssUpdateAll(compRoot, compRoot)
  }

  static mouseleave(comp: Comp, compRoot: Comp) {
    // Hover
    if (compRoot.rootIsEnableHover) {
      comp.isHover = undefined
      DataService.cssUpdate(comp, compRoot)
    }
  }

  static resize(screenWidth: number, compRoot: Comp) {
    let breakpoint = compRoot.rootBreakpoint
    compRoot.rootBreakpoint = undefined
    if (screenWidth < 1024) {
      compRoot.rootBreakpoint = "medium"
    }
    if (screenWidth < 768) {
      compRoot.rootBreakpoint = "small"
    }
    if (breakpoint != compRoot.rootBreakpoint) { // Breakpoint changed
      this.isSwitchResetBreakpointAll(compRoot, compRoot)
    }
    this.cssUpdateAll(compRoot, compRoot)
  }

  static cssUpdate(comp: Comp, compRoot: Comp) {
    // CssClass
    comp.calculatedCssClass = comp.cssClass
    if (compRoot.rootBreakpoint == "medium" && comp.cssClassMedium != null) {
      comp.calculatedCssClass = comp.cssClassMedium
    }
    if (compRoot.rootBreakpoint == "small" && comp.cssClassSmall != null) {
      comp.calculatedCssClass = comp.cssClassMedium ? comp.cssClassMedium : comp.cssClassSmall ? comp.cssClassSmall : comp.calculatedCssClass
    }
    // CssStyle
    comp.calculatedCssStyle = comp.cssStyle
    if (compRoot.rootBreakpoint == "medium" && comp.cssStyleMedium != null) {
      comp.calculatedCssStyle = comp.cssStyleMedium
    }
    if (compRoot.rootBreakpoint == "small") {
      comp.calculatedCssStyle = comp.cssStyleMedium ? comp.cssStyleMedium : comp.cssStyleSmall ? comp.cssStyleSmall : comp.calculatedCssStyle
    }

    // Switch
    if (comp.switchGroup) {
      this.cssUpdateAppend(comp, true, comp.switchCssClass, comp.switchCssStyle)
      this.cssUpdateAppend(comp, comp.isSwitch, comp.switchOnCssClass, comp.switchOnCssStyle)
    }
    // Active
    if (comp.activeGroup) {
      this.cssUpdateAppend(comp, true, comp.activeCssClass, comp.activeCssStyle)
      this.cssUpdateAppend(comp, comp.isActive, comp.activeOnCssClass, comp.activeOnCssStyle)
      this.cssUpdateAppend(comp, comp.isActiveAncestor, comp.activeAncestorCssClass, comp.activeAncestorCssStyle)
    }

    // Hover
    if (comp.isHover) {
      this.cssUpdateAppend(comp, true, comp.hoverCssClass, comp.hoverCssStyle)
      // Active
      if (comp.activeGroup) {
        this.cssUpdateAppend(comp, true, comp.activeHoverCssClass, comp.activeHoverCssStyle)
        this.cssUpdateAppend(comp, comp.isActive, comp.activeOnHoverCssClass, comp.activeOnHoverCssStyle)
        this.cssUpdateAppend(comp, comp.isActiveAncestor, comp.activeAncestorHoverCssClass, comp.activeOnHoverCssStyle)
      } else {
        // Switch
        if (comp.switchGroup) {
          this.cssUpdateAppend(comp, true, comp.switchHoverCssClass, comp.switchHoverCssStyle)
          this.cssUpdateAppend(comp, comp.isSwitch, comp.switchOnHoverCssClass, comp.switchOnHoverCssStyle)
        }
      }
    }
  }

  static cssUpdateAppend(comp: Comp, value?: boolean, cssClass?: string, cssStyle?: string) {
    // CssClass
    if (value) {
      if (cssClass != null) {
        if (comp.calculatedCssClass) {
          comp.calculatedCssClass += " "
        }
        comp.calculatedCssClass = (comp.calculatedCssClass || "") + cssClass
      }
      // CssStyle
      if (cssStyle != null) {
        if (comp.calculatedCssStyle && !comp.calculatedCssStyle.endsWith(";")) {
          comp.calculatedCssStyle += "; "
        }
        comp.calculatedCssStyle = (comp.calculatedCssStyle || "") + cssStyle
      }
    }
  }

  static cssUpdateAll(comp: Comp, compRoot: Comp) {
    this.cssUpdate(comp, compRoot)
    comp.list?.forEach((item) => this.cssUpdateAll(item, compRoot))
  }

  static isSwitchReset(comp: Comp) {
    if (comp.switchGroup && comp.isSwitch) {
      this.isSwitchToggle(comp)
    }
  }

  static isSwitchResetBreakpointAll(comp: Comp, compRoot: Comp) {
    if (comp.isSwitchResetBreakpoint) {
      if (comp.isSwitch) {
        this.isSwitchReset(comp)
      }
    }
    comp.list?.forEach((item) => this.isSwitchResetBreakpointAll(item, compRoot))
  }

  static isSwitchResetAll(comp: Comp, compRoot: Comp, compClick: Comp) {
    if (comp.isSwitchReset && comp != compClick && !compClick.switchNames?.includes(comp.name!)) {
      this.isSwitchReset(comp)
    }
    comp.list?.forEach((item) => this.isSwitchResetAll(item, compRoot, compClick))
  }

  static isSwitchResetHoverAll(comp: Comp, compRoot: Comp, compClick: Comp) {
    let isSwitchResetHover = (compRoot.rootBreakpoint == null && comp.isSwitchResetHover) || (compRoot.rootBreakpoint == "medium" && comp.isSwitchResetHoverMedium) || (compRoot.rootBreakpoint == "small" && comp.isSwitchResetHoverSmall)
    if (isSwitchResetHover && comp != compClick) {
      let isAncestor = compClick && comp.activeGroup == compClick.activeGroup && compClick.activePath?.startsWith(comp.activePath!)
      if (!isAncestor) {
        this.isSwitchReset(comp)
      }
    }
    comp.list?.forEach((item) => this.isSwitchResetHoverAll(item, compRoot, compClick))
  }

  static isSwitchToggle(comp: Comp) {
    if (!comp.isSwitch) {
      comp.isSwitch = true
    } else {
      comp.isSwitch = undefined
    }
  }

  /** Switch through names related switches */
  static isSwitchToggleNamesAll(comp: Comp, compRoot: Comp, compClick: Comp) {
    if (compClick.switchNames?.includes(comp.name!)) {
      this.isSwitchToggle(comp)
    }
    comp.list?.forEach((item) => this.isSwitchToggleNamesAll(item, compRoot, compClick))
  }

  static isActiveUpdateAll(comp: Comp, compRoot: Comp, compClick: Comp) {
    if (comp.activeGroup == compClick.activeGroup && comp.activePath) {
      // IsActive, IsActiveAncestor
      if (compClick.isActiveDisable != true) {
        comp.isActive = undefined
        comp.isActiveAncestor = undefined
        if (comp.activePath == compClick.activePath) {
          comp.isActive = true
        } else {
          if (compClick.activePath?.startsWith(comp.activePath)) {
            comp.isActiveAncestor = true
          }
        }
      }
      // IsActiveDescendent
      comp.isActiveDescendent = undefined
      if (comp.activePath != compClick.activePath && comp.activePath?.startsWith(compClick.activePath!)) {
        comp.isActiveDescendent = true
      }
    }
    comp.list?.forEach((item) => this.isActiveUpdateAll(item, compRoot, compClick))
  }

  public update() {
    this.compRoot.rootRequestCount! += 1
  }
}

/** Json component is either a DivComponent or a content component like AnchorComponent. */
export interface Comp {
  /** Type of content component. If null, DivComponent is rendered. */
  type?: string

  name?: string

  /** Text for content component. */
  text?: string

  /** Href for AnchorComponent. */
  href?: string

  /** List of child components. If null use field type to select content component. */
  list?: Comp[]

  /** CssClass of DivComponent. Not applicable to root element. */
  cssClass?: string

  /** Replaces cssClass for medium screen */
  cssClassMedium?: string

  /** Replaces cssClass for small screen */
  cssClassSmall?: string

  /** Append if switchGroup */
  switchCssClass?: string

  /** Append if isSwitch */
  switchOnCssClass?: string

  /** Calculated */
  calculatedCssClass?: string

  /** Css style of DivComponent. Used for example for grid-template-columns. Not applicable to root element. */
  cssStyle?: string

  /** Replaces cssStyle for medium screen. */
  cssStyleMedium?: string

  /** Replaces cssStyle for small screen. */
  cssStyleSmall?: string

  /** Append if switchGroup */
  switchCssStyle?: string

  /** Append if isSwitch */
  switchOnCssStyle?: string

  /** Calculated */
  calculatedCssStyle?: string

  /** True if user mouse hovers over. */
  isHover?: boolean

  /** True if user clicks DivComponent and unassigned if user clicks again. */
  isSwitch?: boolean

  /** If true, isSwitch is set to undefined if screen resize hits breakpoint. */
  isSwitchResetBreakpoint?: boolean

  /** If true, isSwitch is set to undefined if any other switch is clicked. */
  isSwitchReset?: boolean

  /** If true, isSwitch is set to undefined if any other component is hovered. Except of isActiveDescendent components. */
  isSwitchResetHover?: boolean

  isSwitchResetHoverMedium?: boolean

  isSwitchResetHoverSmall?: boolean

  /** If true, user can not click this component. */
  isSwitchDisable?: boolean

  switchGroup?: string

  /** Related switches to also click. */
  switchNames?: string[]

  activePath?: string

  activeGroup?: string

  isActive?: boolean

  /** If true, user can not click and activate this component. */
  isActiveDisable?: boolean

  /** If true, comp is an ancestor of activePath. */
  isActiveAncestor?: boolean

  /** If true, comp is a descendent of activePath */
  isActiveDescendent?: boolean

  /** If true, switch is clicked when hover. */
  isActiveSwitchHover?: boolean

  /** If true, switch is clicked when hover. */
  isActiveSwitchHoverSmall?: boolean

  /** If true, switch is clicked when hover. */
  isActiveSwitchHoverMedium?: boolean

  activeCssClass?: string

  activeCssStyle?: string

  activeOnCssClass?: string

  activeOnCssStyle?: string

  activeAncestorCssClass?: string

  activeAncestorCssStyle?: string

  hoverCssClass?: string

  hoverCssStyle?: string

  switchOnHoverCssClass?: string

  switchOnHoverCssStyle?: string

  switchHoverCssClass?: string

  switchHoverCssStyle?: string

  activeHoverCssClass?: string

  activeHoverCssStyle?: string

  activeOnHoverCssClass?: string

  activeOnHoverCssStyle?: string

  activeAncestorHoverCssClass?: string

  activeAncestorHoverCssStyle?: string

  rootRequestCount?: number

  /** Breakpoint (null, Medium, Small). Root comp only. */
  rootBreakpoint?: string

  rootIsEnableHover?: boolean

  rootIsEnableHoverSwitch?: boolean

  debug?: string
}