// the polyfills must be the first thing imported in node.js
import 'angular2-universal-polyfills';

// Angular 2
import { enableProdMode } from '@angular/core';
import { platformNodeDynamic } from "angular2-platform-node";
import { platformDynamicServer } from '@angular/platform-server';
import { NgModule } from '@angular/core';
import { Component } from '@angular/core';
import { BrowserModule, platformBrowser } from '@angular/platform-browser';
import { UniversalModule } from 'angular2-universal';

import { AppComponent } from '../../../Angular/app/app.component'
import { DataService, Data } from '../../../Angular/app/dataService'

@NgModule({
  imports: [UniversalModule],
  declarations: [AppComponent],
  bootstrap: [AppComponent],

})
export class MyModule {
}


// ### Start server side rendering in node

import 'zone.js';

enableProdMode();

import { Promise } from 'es6-promise';

declare var module: any;

(module).exports = function (params) {
  return new Promise(function (resolve, reject) {

    // See also: https://github.com/aspnet/JavaScriptServices/commit/5214a553a7d98bc532b956af7ad14b8905302878?w=1
    var requestZone = Zone.current.fork({
      name: 'angular-universal request',
      properties: {
        document: '<!DOCTYPE html><html><head></head><body><my-app></my-app></body></html>'
      }
    });

    if (params.data == null) {
      params.data = JSON.stringify({ Name: "Data from server2.ts" })
    }

    requestZone.run(() => {
      platformNodeDynamic([{ provide: 'paramsData', useValue: params.data }]).serializeModule(MyModule).then(html => {
        // console.log("Html=" + html); // Debug Angular Universal only!
        resolve({ html: html })
      });
    });

  });
}

// (module).then(); // Debug Angular Universal only! (Call default function)
