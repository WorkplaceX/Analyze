var gulp = require('gulp');
require('shelljs/global');

gulp.task('default', function () {
    exec('cd ../Parameter & npm run gulp -- -HelloFromExec');
    console.log('Done');
});
