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
    if (comp.switchGroup) {
      comp.switchCssClass ??= "switch"
      comp.hoverCssClass ??= "hover"
      comp.hoverSwitchCssClass ??= "hover-switch"
    } else {
      if (comp.activePath) {
        comp.hoverCssClass ??= "hover"
        comp.activeCssClass ??= "active"
        comp.activeAncestorCssClass ??= "active-ancestor"
        comp.hoverActiveCssClass ??= "hover-active"
        comp.hoverActiveAncestorCssClass ??= "hover-active-ancestor"
      }
    }
    comp.list?.forEach((item) => this.transform(item, compRoot))
  }

  static click(comp: Comp, compRoot: Comp) {
    // Switch
    if (comp.switchGroup) {
      this.isSwitchUpdate(comp)
      if (comp.switchNames) {
        this.isSwitchUpdateNamesAll(compRoot, compRoot, comp)
      }
      this.isSwitchResetSwitchAll(compRoot, compRoot, comp)
    }
    // Active
    if (comp.activePathGroup) {
      this.isActiveUpdateAll(compRoot, compRoot, comp)
    }
    DataService.cssUpdate(comp, compRoot)
  }

  static resize(screenWidth: number, compRoot: Comp) {
    let breakpoint = compRoot.rootBreakpoint
    compRoot.rootBreakpoint = undefined
    if (screenWidth < 1024) {
      compRoot.rootBreakpoint = "Medium"
    }
    if (screenWidth < 768) {
      compRoot.rootBreakpoint = "Small"
    }
    this.cssUpdateAll(compRoot, compRoot)
    if (breakpoint != compRoot.rootBreakpoint) { // Breakpoint changed
      this.isSwitchResetBreakpointAll(compRoot, compRoot)
    }
  }

  static cssUpdate(comp: Comp, compRoot: Comp) {
    // CssClass
    comp.calculatedCssClass = comp.cssClass
    if (compRoot.rootBreakpoint == "Medium" && comp.cssClassMedium != null) {
      comp.calculatedCssClass = comp.cssClassMedium
    }
    if (compRoot.rootBreakpoint == "Small" && comp.cssClassSmall != null) {
      comp.calculatedCssClass = comp.cssClassMedium ? comp.cssClassMedium : comp.cssClassSmall ? comp.cssClassSmall : comp.calculatedCssClass
    }
    // CssStyle
    comp.calculatedCssStyle = comp.cssStyle
    if (compRoot.rootBreakpoint == "Medium" && comp.cssStyleMedium != null) {
      comp.calculatedCssStyle = comp.cssStyleMedium
    }
    if (compRoot.rootBreakpoint == "Small") {
      comp.calculatedCssStyle = comp.cssStyleMedium ? comp.cssStyleMedium : comp.cssStyleSmall ? comp.cssStyleSmall : comp.calculatedCssStyle
    }
    // Switch
    this.cssUpdateAppend(comp, comp.isSwitch, comp.switchCssClass, comp.switchCssStyle)
    // Active
    this.cssUpdateAppend(comp, comp.isActive, comp.activeCssClass, comp.activeCssStyle)
    // ActiveAncestor
    this.cssUpdateAppend(comp, comp.isActiveAncestor, comp.activeAncestorCssClass, comp.activeAncestorCssStyle)
    // Hover
    if (comp.isHover) {
      if (comp.isSwitch) {
        this.cssUpdateAppend(comp, true, comp.hoverSwitchCssClass, comp.hoverSwitchCssStyle)
      } else {
        if (comp.isActive) {
          this.cssUpdateAppend(comp, true, comp.hoverActiveCssClass, comp.hoverActiveCssStyle)
        } else {
          if (comp.isActiveAncestor) {
            this.cssUpdateAppend(comp, true, comp.hoverActiveAncestorCssClass, comp.hoverActiveCssStyle)
          } else {
            this.cssUpdateAppend(comp, true, comp.hoverCssClass, comp.hoverCssStyle)
          }
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
        this.click(comp, compRoot)
      }
    }
    comp.list?.forEach((item) => this.isSwitchResetBreakpointAll(item, compRoot))
  }

  static isSwitchResetSwitchAll(comp: Comp, compRoot: Comp, compClick: Comp) {
    if (comp.isSwitchResetSwitch && comp != compClick && comp.switchGroup == compClick.switchGroup) {
      if (comp.isSwitch) {
        this.click(comp, compRoot)
      }
    }
    comp.list?.forEach((item) => this.isSwitchResetSwitchAll(item, compRoot, compClick))
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
    if (comp.activePath && comp.activePathGroup == compClick.activePathGroup && compClick.isActiveDisable != true) {
      comp.isActive = undefined
      comp.isActiveAncestor = undefined
      if (comp.activePath == compClick.activePath) {
        comp.isActive = true
      } else {
        if (compClick.activePath?.startsWith(comp.activePath)) {
          comp.isActiveAncestor = true
        }
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
  isSwitchResetSwitch?: boolean

  switchGroup?: string

  /** Related switches to also click. */
  switchNames?: string[]

  activePath?: string

  activePathGroup?: string

  isActive?: boolean

  /** If true, user can not click and activate component. */
  isActiveDisable?: boolean

  /** If true, comp is an ancestor of activePath. */
  isActiveAncestor?: boolean

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

  // Root

  rootRequestCount?: number

  /** Breakpoint (null, Medium, Small). Root comp only. */
  rootBreakpoint?: string
}
