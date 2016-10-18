"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
require('jQuery');
var Tether = require('tether');
window.Tether = Tether;
require('bootstrap');
var core_1 = require('@angular/core');
var core_2 = require('@angular/core');
var ng_bootstrap_1 = require('@ng-bootstrap/ng-bootstrap');
var platform_browser_1 = require('@angular/platform-browser');
var AppComponent = (function () {
    function AppComponent() {
        this.model = {
            left: true,
            middle: false,
            right: false
        };
    }
    AppComponent = __decorate([
        core_1.Component({
            selector: 'my-app',
            template: "\n<div class=\"container\">\n  <div class=\"row\">\n    <div class=\"col-sm-4\">\n      <h1>My First Angular App</h1>\n    </div>\n    <div class=\"col-sm-4\">\n      One of three columns (2)\n    </div>\n    <div class=\"col-sm-4\">\n      One of three columns (3)\n  <label class=\"btn btn-primary\" [class.active]=\"model.left\">\n    <input type=\"checkbox\" /> Button\n  </label>    </div>\n  </div>\n</div>\n\n"
        })
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
var AppModule = (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_2.NgModule({
            declarations: [AppComponent],
            imports: [platform_browser_1.BrowserModule, ng_bootstrap_1.NgbModule.forRoot()],
            bootstrap: [AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
var platform_browser_dynamic_1 = require('@angular/platform-browser-dynamic');
var platform = platform_browser_dynamic_1.platformBrowserDynamic();
platform.bootstrapModule(AppModule);
