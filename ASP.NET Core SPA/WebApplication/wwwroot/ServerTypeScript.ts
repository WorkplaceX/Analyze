import { Promise } from 'es6-promise';

declare var module: any;

(module).exports = function (params) {
    return new Promise(function (resolve, reject) {
        var result = '<h1>Hello world from Server Side TypeScript!</h1>'
            + '<p>Current time in Node.js is: ' + new Date() + '</p>'
            + '<p>Request path is: ' + params.location.path + '</p>'
            + '<p>Absolute URL is: ' + params.absoluteUrl + '</p>'
            + '<p>Data is: ' + JSON.stringify(JSON.parse(params.data)) + '</p>';
        resolve({ html: result });
    });
}