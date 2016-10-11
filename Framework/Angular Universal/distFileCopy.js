var fs = require('fs');

fs.createReadStream('dist/server/index.js').pipe(fs.createWriteStream('../ASP.NET Core/WebApplication/AngularUniversalServer.js'));