"use strict";
require('reflect-metadata');
require('angular2-universal/polyfills');
var main2_1 = require('./main2');
var Boot = (function () {
    function Boot() {
        /*
        
                var config: ngUniversal.BootloaderConfig = {
                directives: [AppComponent2],
                providers: [
                    ngCore.provide(BASE_URL, { useValue: '/' }),
                    ngCore.provide(ORIGIN_URL, { useValue: "/index2.html" }),
                    ngCore.provide(REQUEST_URL, { useValue: "/index.html" }),
                    // provideRouter(routes), // Out of Stack Space
                    ngUniversal.NODE_HTTP_PROVIDERS,
                    ngUniversal.NODE_ROUTER_PROVIDERS,
                ],
                // TODO: Render just the <app> component instead of wrapping it inside an extra HTML document
                // Waiting on https://github.com/angular/universal/issues/347
                template: '<!DOCTYPE html>\n<html><head></head><body><my-app2></my-app2></body></html>'
                }
        */
        // alert("Hello");
        // alert(bootloader); 
        // bootloader(config); 
        /*
        
                renderToString(AppComponent2).then(html => {
        
                    })
                    */
    }
    return Boot;
}());
var boot = new Boot();
var platform_browser_dynamic_1 = require('@angular/platform-browser-dynamic');
platform_browser_dynamic_1.bootstrap(main2_1.AppComponent2);
var main2_2 = require('./main2');
var platform_server_1 = require('@angular/platform-server');
var platform_browser_1 = require('@angular/platform-browser');
var platform_browser_dynamic_2 = require('@angular/platform-browser-dynamic');
var stringify_element_1 = require('angular2-universal/dist/node/stringify_element');
var core_1 = require('@angular/core');
/*
import {BROWSER_PROVIDERS } from "@angular/platform-browser";
disposePlatform();
createPlatform(ReflectiveInjector.resolveAndCreate(BROWSER_PROVIDERS));
alert("getPlatform=" + getPlatform());
*/
core_1.disposePlatform();
platform_server_1.serverBootstrap(main2_2.AppComponent3, [platform_browser_1.BROWSER_APP_PROVIDERS, platform_browser_dynamic_2.BROWSER_APP_COMPILER_PROVIDERS]).then(function (value) {
    var appComponent = value.instance;
    var elementRef = appComponent.MyElementRef;
    alert(stringify_element_1.stringifyElement(elementRef.nativeElement));
});
//# sourceMappingURL=boot-server.js.map