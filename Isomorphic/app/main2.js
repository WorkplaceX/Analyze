"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var AppComponent2 = (function () {
    function AppComponent2() {
        this.text = 'Hello Data';
    }
    AppComponent2 = __decorate([
        core_1.Component({
            selector: 'my-app2',
            template: '<h1>App {{ text }}</h1>'
        }), 
        __metadata('design:paramtypes', [])
    ], AppComponent2);
    return AppComponent2;
}());
exports.AppComponent2 = AppComponent2;
var AppComponent3 = (function () {
    function AppComponent3(elementRef) {
        this.MyElementRef = elementRef;
        this.text = 'Hello Data3';
    }
    AppComponent3 = __decorate([
        core_1.Component({
            template: '<h1>App {{ text }}</h1>'
        }), 
        __metadata('design:paramtypes', [core_1.ElementRef])
    ], AppComponent3);
    return AppComponent3;
}());
exports.AppComponent3 = AppComponent3;
//# sourceMappingURL=main2.js.map