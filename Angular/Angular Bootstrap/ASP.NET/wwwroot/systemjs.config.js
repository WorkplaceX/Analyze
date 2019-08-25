/**
 * System configuration for Angular samples
 * Adjust as necessary for your application needs.
 */
(function (global) {
  System.config({
    paths: {
      // paths serve as alias
      'npm:': 'node_modules/'
    },
    // map tells the System loader where to look for things
    map: {
      // our app is within the app folder
      app: 'app',
      // angular bundles
      '@angular/core': 'npm:@angular/core/bundles/core.umd.js',
      '@angular/common': 'npm:@angular/common/bundles/common.umd.js',
      '@angular/compiler': 'npm:@angular/compiler/bundles/compiler.umd.js',
      '@angular/platform-browser': 'npm:@angular/platform-browser/bundles/platform-browser.umd.js',
      '@angular/platform-browser-dynamic': 'npm:@angular/platform-browser-dynamic/bundles/platform-browser-dynamic.umd.js',
      '@angular/forms': 'npm:@angular/forms/bundles/forms.umd.js',
      '@ng-bootstrap/ng-bootstrap': 'node_modules/@ng-bootstrap/ng-bootstrap/bundles/ng-bootstrap.js',
        // other libraries
      'rxjs': 'npm:rxjs',
      'jQuery': 'node_modules/jquery/dist/jquery.js',
      'tether': 'node_modules/tether/dist/js/tether.js',
      'bootstrap': 'node_modules/bootstrap/dist/js/bootstrap.js'
  },
    // packages tells the System loader how to load when no filename and/or no extension
    packages: {
      app: {
        main: './main.js',
        defaultExtension: 'js',
      },
      rxjs: {
          defaultExtension: 'js'
      }
    }
  });
})(this);
