"use strict";
var main_1 = require('./main');
exports.routes = [
    { path: '', redirectTo: 'home' },
    { path: 'home', component: main_1.AppComponent },
    { path: '**', redirectTo: 'home' }
];
//# sourceMappingURL=routes.js.map