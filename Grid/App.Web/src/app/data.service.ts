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
    if (comp.list == undefined) {
      comp.cssClassHover = "hover"
    }
    comp.list?.forEach((item) => this.transform(item, compRoot))
  }

  static click(comp: Comp, compRoot: Comp) {
    this.isSwitchUpdate(comp)
    if (comp.switchNames) {
      this.isSwitchUpdateNamesAll(compRoot, compRoot, comp)
    }
    if (comp.activePath && comp.isActiveDisable != true) {
      this.isActiveUpdateAll(compRoot, compRoot, comp)
    }
    this.isSwitchResetSwitchAll(compRoot, compRoot, comp)
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
    comp.cssClassCurrent = comp.cssClass
    if (compRoot.rootBreakpoint == "Medium" && comp.cssClassMedium != null) {
      comp.cssClassCurrent = comp.cssClassMedium
    }
    if (compRoot.rootBreakpoint == "Small" && comp.cssClassSmall != null) {
      comp.cssClassCurrent = comp.cssClassMedium ? comp.cssClassMedium : comp.cssClassSmall ? comp.cssClassSmall : comp.cssClassCurrent
    }
    // CssStyle
    comp.cssStyleCurrent = comp.cssStyle
    if (compRoot.rootBreakpoint == "Medium" && comp.cssStyleMedium != null) {
      comp.cssStyleCurrent = comp.cssStyleMedium
    }
    if (compRoot.rootBreakpoint == "Small") {
      comp.cssStyleCurrent = comp.cssStyleMedium ? comp.cssStyleMedium : comp.cssStyleSmall ? comp.cssStyleSmall : comp.cssStyleCurrent
    }
    // CssSwitch
    this.cssUpdateAppend(comp, comp.isSwitch, comp.cssClassSwitch, comp.cssStyleSwitch)
    // CssActive
    this.cssUpdateAppend(comp, comp.isActive, comp.cssClassActive, comp.cssStyleActive)
    // CssActiveParent
    this.cssUpdateAppend(comp, comp.isActiveParent, comp.cssClassActiveParent, comp.cssStyleActiveParent)
    // CssHover
    this.cssUpdateAppend(comp, comp.isHover, comp.cssClassHover, comp.cssStyleHover)
  }

  static cssUpdateAppend(comp: Comp, value?: boolean, cssClass?: string, cssStyle?: string) {
    // CssClass
    if (value) {
      if (cssClass != null) {
        if (comp.cssClassCurrent) {
          comp.cssClassCurrent += " "
        }
        comp.cssClassCurrent = (comp.cssClassCurrent || "") + cssClass
      }
      // CssStyle
      if (cssStyle != null) {
        if (comp.cssStyleCurrent && !comp.cssStyleCurrent.endsWith(";")) {
          comp.cssStyleCurrent += "; "
        }
        comp.cssStyleCurrent = (comp.cssStyleCurrent || "") + cssStyle
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
    if (comp.isSwitch == undefined) {
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
    if (comp.activePath && comp.activePathGroup == compClick.activePathGroup) {
      comp.isActive = undefined
      comp.isActiveParent = undefined
      if (comp.activePath == compClick.activePath) {
        comp.isActive = true
      } else {
        if (compClick.activePath?.startsWith(comp.activePath)) {
          comp.isActiveParent = true
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

  /** Append if isHover */
  cssClassHover?: string

  /** Append if isSwitch */
  cssClassSwitch?: string

  /** Calculated */
  cssClassCurrent?: string

  /** Css style of DivComponent. Used for example for grid-template-columns. Not applicable to root element. */
  cssStyle?: string

  /** Replaces cssStyle for medium screen. */
  cssStyleMedium?: string

  /** Replaces cssStyle for small screen. */
  cssStyleSmall?: string

  /** Append if isHover */
  cssStyleHover?: string

  /** Append if isSwitch */
  cssStyleSwitch?: string

  /** Calculated */
  cssStyleCurrent?: string

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

  isActive?: boolean

  /** If true, user can not click and activate component. */
  isActiveDisable?: boolean

  /** If true, comp is an active parent in activePath. */
  isActiveParent?: boolean

  cssClassActive?: string

  cssStyleActive?: string

  cssClassActiveParent?: string

  cssStyleActiveParent?: string

  activePath?: string

  activePathGroup?: string

  rootRequestCount?: number

  /** Breakpoint (null, Medium, Small). Root comp only. */
  rootBreakpoint?: string
}
