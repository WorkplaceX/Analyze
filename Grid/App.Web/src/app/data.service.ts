import { Injectable } from '@angular/core';
import { Data } from './json';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor() { }

  public json: Json = Data.json

  public resize(screenWidth: number) {
    this.json.breakPoint = undefined;
    if (screenWidth < 1024) {
      this.json.breakPoint = "Medium"
    }
    if (screenWidth < 768) {
      this.json.breakPoint = "Small"
    }
    this.cssUpdateRecursive(this.json)
  }

  public cssUpdate(json: Json) {
    // CssClass
    json.cssClassCurrent = json.cssClass
    if (this.json.breakPoint == "Medium" && json.cssClassMedium != null) {
      json.cssClassCurrent = json.cssClassMedium
    }
    if (this.json.breakPoint == "Small" && json.cssClassSmall != null) {
      json.cssClassCurrent = json.cssClassSmall
    }
    // CssStyle
    json.cssStyleCurrent = json.cssStyle
    if (this.json.breakPoint == "Medium" && json.cssStyleMedium != null) {
      json.cssStyleCurrent = json.cssStyleMedium
    }
    if (this.json.breakPoint == "Small" && json.cssStyleSmall != null) {
      json.cssStyleCurrent = json.cssStyleSmall
    }
    // CssSwitch
    this.cssUpdateAppend(json, json.isSwitch, json.cssClassSwitch, json.cssStyleSwitch)
    // CssHover
    this.cssUpdateAppend(json, json.isHover, json.cssClassHover, json.cssStyleHover)
  }

  cssUpdateAppend(json: Json, value?: boolean, cssClass?: string, cssStyle?: string) {
    // CssClass
    if (value && cssClass != null) {
      if (json.cssClassCurrent) {
        json.cssClassCurrent += " "
      }
      json.cssClassCurrent = (json.cssClassCurrent || "") + cssClass
    }
    // CssStyle
    if (value && cssStyle != null) {
      if (json.cssStyleCurrent && !json.cssStyleCurrent.endsWith(";")) {
        json.cssStyleCurrent += "; "
      }
      json.cssStyleCurrent = (json.cssStyleCurrent || "") + cssStyle
    }
  }

  cssUpdateRecursive(json: Json) {
    this.cssUpdate(json)
    json.list?.forEach((item) => this.cssUpdateRecursive(item))
  }

  public update() {
    this.json.requestCount! += 1
  }
}

/** Component is either a DivComponent or a content component like AnchorComponent. */
export interface Json {
  name?: string

  /** Text for content component. */
  text?: string

  /** List of child components. If null use field type to select content component. */
  list?: Json[]

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
