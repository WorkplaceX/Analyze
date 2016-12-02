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

function Run (params) {
  return new Promise(function (resolve, reject) {

    // See also: https://github.com/aspnet/JavaScriptServices/commit/5214a553a7d98bc532b956af7ad14b8905302878?w=1
    var requestZone = Zone.current.fork({
      name: 'angular-universal request',
      properties: {
        document: '<my-app></my-app>'
      }
    });

    if (params.data == null) {
      params.data = JSON.stringify({ Name: "Data from server2.ts" })
    }

    requestZone.run(() => {
      platformNodeDynamic([{ provide: 'paramsData', useValue: params.data }]).serializeModule(MyModule).then(html => {
        html = (<string>html).substring(48); // Remove <html><head><title></title></head><body><my-app>
        html = (<string>html).substring(0, (<string>html).length - 234); // Remove </my-app></body></html><universal-script><script> try {window.UNIVERSAL_CACHE = ({"APP_ID":"c04f"}) ...
        // console.log("Html=" + html); // Debug Angular Universal only!
        resolve(html)
      });
    });

  });
}

(module).exports = function (params) {
  return Run(params);
}

// (module).then(); // Debug Angular Universal only! (Call default function)


/* 
-Uncomment following code block.
-npm run build:prod
-Copy manually file dist/index.js to Application/Nodejs/Server/AngularUniversalServerNoSpa.js
*/
/*
var http = require('http');
http.createServer(function (req, res) {
  var htmlBegin = 
    `
    <html>
    <head>
        <title>Angular QuickStart</title>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <link rel="stylesheet" href="styles.css">
        <link rel="stylesheet" href="node_modules/bootstrap/dist/css/bootstrap.min.css">
    </head>
    <!-- 3. Display the application -->
    <body>
        <script>
            params = { data: JSON.stringify({ Name: "Data from index.html" }) };
        </script>
        <my-app>
    `;
  var htmlEnd =
    `
    </my-app>

        <script type="text/javascript">
            function defer() {
                var element = document.createElement("script");
                element.src = "defer.js";
                document.head.appendChild(element);
            }

            if (window.addEventListener)
                window.addEventListener("load", defer, false);
            else if (window.attachEvent)
                window.attachEvent("onload", defer);
            else window.onload = defer;
        </script>

    </body>
    </html>
    `;
  
  Run({}).then((html) => { 
    res.writeHead(200, { 'Content-Type': 'text/html' });
    res.end(htmlBegin + html + htmlEnd);
  })
}).listen(process.env.PORT); // Debug: listen(process.env.PORT); listen(1337, '127.0.0.1');
*/