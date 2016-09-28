module.exports = function (params) {
    return new Promise(function (resolve, reject) {
        var result = '<h1>Hello world from Server Side JavaScript!</h1>'
           + '<p>Current time in Node is: ' + new Date() + '</p>'
           + '<p>Request path is: ' + params.location.path + '</p>'
           + '<p>Absolute URL is: ' + params.absoluteUrl + '</p>';
        resolve({ html: result });
    });
};