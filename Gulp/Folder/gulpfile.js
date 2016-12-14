var gulp = require('gulp');
var fs = require('fs');
var path = require('path');

function getFolders(dir) {
    return fs.readdirSync(dir)
      .filter(function (file) {
          return fs.statSync(path.join(dir, file)).isDirectory();
      });
}

gulp.task('default', function () {
    var folderList = getFolders('./');
    folderList.forEach(function (item) {
        console.log(item);
    });
});

