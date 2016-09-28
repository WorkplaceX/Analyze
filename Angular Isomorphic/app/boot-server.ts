import 'reflect-metadata';
import 'angular2-universal/polyfills';
import * as ngCore from '@angular/core';
import { APP_BASE_HREF } from '@angular/common';
import { provideRouter } from '@angular/router';
import * as ngUniversal from 'angular2-universal';
import { BASE_URL, ORIGIN_URL, REQUEST_URL } from 'angular2-universal/common';
import { routes } from './routes';
import { AppComponent2 } from './main2';
import { renderToString } from 'angular2-universal';

class Boot { 
	constructor() {
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
}

const boot = new Boot();


import { bootstrap } from '@angular/platform-browser-dynamic';
bootstrap(AppComponent2);


import { AppComponent3 } from './main2';
import { serverBootstrap } from '@angular/platform-server';
import {BROWSER_APP_PROVIDERS} from '@angular/platform-browser';
import {BROWSER_APP_COMPILER_PROVIDERS} from '@angular/platform-browser-dynamic';
import { stringifyElement } from 'angular2-universal/dist/node/stringify_element'
import { disposePlatform, getPlatform, createPlatform, ElementRef, ReflectiveInjector } from '@angular/core';

/*
import {BROWSER_PROVIDERS } from "@angular/platform-browser";
disposePlatform();
createPlatform(ReflectiveInjector.resolveAndCreate(BROWSER_PROVIDERS));
alert("getPlatform=" + getPlatform());
*/
disposePlatform();

serverBootstrap(AppComponent3, [BROWSER_APP_PROVIDERS, BROWSER_APP_COMPILER_PROVIDERS]).then(value =>{
    var appComponent: AppComponent3 = <AppComponent3> value.instance;
    var elementRef: ElementRef = appComponent.MyElementRef;
    alert(stringifyElement(elementRef.nativeElement));
})
