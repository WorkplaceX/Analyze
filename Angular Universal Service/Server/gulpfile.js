/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var shell = require('gulp-shell')
var runSequence = require('run-sequence');

// npm run publish
gulp.task('publish', shell.task([
  'cd.. & cd Render & npm run publish'
]))

// Copy file
gulp.task('copy', function () {
    return gulp.src('../Render/publish/**/*.*')
        .pipe(gulp.dest('./Render/'))
})

gulp.task('default', function () {
    return runSequence('publish', 'copy');
});