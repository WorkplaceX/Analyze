var fs = require('fs');

fs.createReadStream('../Angular Universal/dist/server/index.js').pipe(fs.createWriteStream('Application/Node.js/Server/AngularUniversalServer.js'));