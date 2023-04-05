import { Injectable } from '@angular/core';
import { Data } from './comp';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor() { }

  public comp: Comp = Data.comp

  public resize(screenWidth: number) {
    this.comp.breakPoint = undefined;
    if (screenWidth < 1024) {
      this.comp.breakPoint = "Medium"
    }
    if (screenWidth < 768) {
      this.comp.breakPoint = "Small"
    }
    this.cssUpdateRecursive(this.comp)
  }

  public cssUpdate(comp: Comp) {
    // CssClass
    comp.cssClassCurrent = comp.cssClass
    if (this.comp.breakPoint == "Medium" && comp.cssClassMedium != null) {
      comp.cssClassCurrent = comp.cssClassMedium
    }
    if (this.comp.breakPoint == "Small" && comp.cssClassSmall != null) {
      comp.cssClassCurrent = comp.cssClassSmall
    }
    // CssStyle
    comp.cssStyleCurrent = comp.cssStyle
    if (this.comp.breakPoint == "Medium" && comp.cssStyleMedium != null) {
      comp.cssStyleCurrent = comp.cssStyleMedium
    }
    if (this.comp.breakPoint == "Small" && comp.cssStyleSmall != null) {
      comp.cssStyleCurrent = comp.cssStyleSmall
    }
    // CssSwitch
    this.cssUpdateAppend(comp, comp.isSwitch, comp.cssClassSwitch, comp.cssStyleSwitch)
    // CssHover
    this.cssUpdateAppend(comp, comp.isHover, comp.cssClassHover, comp.cssStyleHover)
  }

  cssUpdateAppend(comp: Comp, value?: boolean, cssClass?: string, cssStyle?: string) {
    // CssClass
    if (value && cssClass != null) {
      if (comp.cssClassCurrent) {
        comp.cssClassCurrent += " "
      }
      comp.cssClassCurrent = (comp.cssClassCurrent || "") + cssClass
    }
    // CssStyle
    if (value && cssStyle != null) {
      if (comp.cssStyleCurrent && !comp.cssStyleCurrent.endsWith(";")) {
        comp.cssStyleCurrent += "; "
      }
      comp.cssStyleCurrent = (comp.cssStyleCurrent || "") + cssStyle
    }
  }

  cssUpdateRecursive(comp: Comp) {
    this.cssUpdate(comp)
    comp.list?.forEach((item) => this.cssUpdateRecursive(item))
  }

  public update() {
    this.comp.requestCount! += 1
  }
}

/** Json component is either a DivComponent or a content component like AnchorComponent. */
export interface Comp {
  name?: string

  /** Text for content component. */
  text?: string

  /** List of child components. If null use field type to select content component. */
  list?: Comp[]

  /** Type of content component. If null, DivComponent is rendered. */
  type?: string

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

  /** If true, isSwitch is set to unassigned when mouse leaves. */
  isSwitchLeave?: boolean

  switchNames?: string[]

  /** Breakpoint (null, Medium, Small). Root element only. */
  breakPoint?: string

  /** Href for AnchorComponent. */
  href?: string

  isActive?: boolean

  requestCount?: number
}
