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
      if (comp.activeGroup) {
        comp.hoverCssClass ??= "hover"
        comp.activeCssClass ??= "active"
        comp.activeAncestorCssClass ??= "active-ancestor"
        comp.hoverActiveCssClass ??= "hover-active"
        comp.hoverActiveAncestorCssClass ??= "hover-active-ancestor"
      }
      if (comp.switchGroup) {
        comp.switchCssClass ??= "switch"
        comp.hoverCssClass ??= "hover"
        comp.hoverSwitchCssClass ??= "hover-switch"
      }
    }
    comp.list?.forEach((item) => this.transform(item, compRoot))
  }

  static clickIsSwitch(comp: Comp, compRoot: Comp) {
    // Switch
    if (comp.switchGroup) {
      if (!comp.isSwitchDisable) {
        this.isSwitchUpdate(comp)
        if (comp.switchNames) {
          this.isSwitchUpdateNamesAll(compRoot, compRoot, comp)
        }
      }
    }
    DataService.cssUpdate(comp, compRoot)
  }

  static clickIsActive(comp: Comp, compRoot: Comp) {
    // Active
    if (comp.activeGroup) {
      if (!comp.isActiveDisable) {
        // Close dropdown
        this.isSwitchResetAll(compRoot, compRoot, comp)
      }
      this.isActiveUpdateAll(compRoot, compRoot, comp)
    }
    DataService.cssUpdate(comp, compRoot)
  }

  static mouseenter(comp: Comp, compRoot: Comp) {
    if (!compRoot.debugIsDisableHover) {
      comp.isHover = true
      DataService.cssUpdate(comp, compRoot)
      let isActiveSwitchHover = (compRoot.rootBreakpoint == null && comp.isActiveSwitchHover) || (compRoot.rootBreakpoint == "medium" && comp.isActiveSwitchHoverMedium) || (compRoot.rootBreakpoint == "small" && comp.isActiveSwitchHoverSmall)
      if (comp.activeGroup && comp.isActiveDisable && isActiveSwitchHover) {
        if (!comp.isSwitch) {
          this.clickIsSwitch(comp, compRoot)
        }
      } else {
        if (!comp.list && !comp.isActiveDescendent) { // TODO comp.list because of parent
          this.isSwitchResetHoverAll(compRoot, compRoot, comp)
        }
      }
    }
  }

  static mouseleave(comp: Comp, compRoot: Comp) {
    if (!compRoot.debugIsDisableHover) {
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
    this.cssUpdateAll(compRoot, compRoot)
    if (breakpoint != compRoot.rootBreakpoint) { // Breakpoint changed
      this.isSwitchResetBreakpointAll(compRoot, compRoot)
    }
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

    // Active
    if (comp.activeGroup) {
      this.cssUpdateAppend(comp, comp.isActive, comp.activeCssClass, comp.activeCssStyle)
      this.cssUpdateAppend(comp, comp.isActiveAncestor, comp.activeAncestorCssClass, comp.activeAncestorCssStyle)
    } else {
      // Switch
      if (comp.switchGroup) {
        this.cssUpdateAppend(comp, comp.isSwitch, comp.switchCssClass, comp.switchCssStyle)
      }
    }

    // Hover
    if (comp.isHover) {
      this.cssUpdateAppend(comp, true, comp.hoverCssClass, comp.hoverCssStyle)
      // Active
      if (comp.activeGroup) {
        this.cssUpdateAppend(comp, comp.isActive, comp.hoverActiveCssClass, comp.hoverActiveCssStyle)
        this.cssUpdateAppend(comp, comp.isActiveAncestor, comp.hoverActiveAncestorCssClass, comp.hoverActiveCssStyle)
      } else {
        // Switch
        if (comp.switchGroup) {
          this.cssUpdateAppend(comp, comp.isSwitch, comp.hoverSwitchCssClass, comp.hoverSwitchCssStyle)
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

  static isSwitchResetBreakpointAll(comp: Comp, compRoot: Comp) {
    if (comp.isSwitchResetBreakpoint) {
      if (comp.isSwitch) {
        this.clickIsSwitch(comp, compRoot)
      }
    }
    comp.list?.forEach((item) => this.isSwitchResetBreakpointAll(item, compRoot))
  }

  static isSwitchResetAll(comp: Comp, compRoot: Comp, compClick: Comp) {
    if (comp.isSwitchReset && comp != compClick && !compClick.switchNames?.includes(comp.name!)) {
      if (comp.isSwitch) {
        this.clickIsSwitch(comp, compRoot)
      }
    }
    comp.list?.forEach((item) => this.isSwitchResetAll(item, compRoot, compClick))
  }

  static isSwitchResetHoverAll(comp: Comp, compRoot: Comp, compClick: Comp) {
    if (comp.activeGroup) {
      let isActiveSwitchHover = (compRoot.rootBreakpoint == null && comp.isActiveSwitchHover) || (compRoot.rootBreakpoint == "medium" && comp.isActiveSwitchHoverMedium) || (compRoot.rootBreakpoint == "small" && comp.isActiveSwitchHoverSmall)
      if (isActiveSwitchHover) {
        if (comp.isSwitch) {
          this.clickIsSwitch(comp, compRoot)
        }
      }
    }
    comp.list?.forEach((item) => this.isSwitchResetHoverAll(item, compRoot, compClick))
  }

  static isSwitchUpdate(comp: Comp) {
    if (!comp.isSwitch) {
      comp.isSwitch = true
    } else {
      comp.isSwitch = undefined
    }
  }

  /** Switch through names related switches */
  static isSwitchUpdateNamesAll(comp: Comp, compRoot: Comp, compClick: Comp) {
    if (compClick.switchNames?.includes(comp.name!)) {
      this.isSwitchUpdate(comp)
      DataService.cssUpdate(comp, compRoot)
    }
    comp.list?.forEach((item) => this.isSwitchUpdateNamesAll(item, compRoot, compClick))
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
      DataService.cssUpdate(comp, compRoot)
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

  /** Append if isSwitch */
  switchCssClass?: string

  /** Calculated */
  calculatedCssClass?: string

  /** Css style of DivComponent. Used for example for grid-template-columns. Not applicable to root element. */
  cssStyle?: string

  /** Replaces cssStyle for medium screen. */
  cssStyleMedium?: string

  /** Replaces cssStyle for small screen. */
  cssStyleSmall?: string

  /** Append if isSwitch */
  switchCssStyle?: string

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

  activeAncestorCssClass?: string

  activeAncestorCssStyle?: string

  hoverCssClass?: string

  hoverCssStyle?: string

  hoverSwitchCssClass?: string

  hoverSwitchCssStyle?: string

  hoverActiveCssClass?: string

  hoverActiveCssStyle?: string

  hoverActiveAncestorCssClass?: string

  hoverActiveAncestorCssStyle?: string

  rootRequestCount?: number

  /** Breakpoint (null, Medium, Small). Root comp only. */
  rootBreakpoint?: string

  debugIsDisableHover?: boolean

  debug?: string
}