var gulp = require('gulp');
var clean = require('gulp-clean');
var rename = require("gulp-rename");
var shell = require('gulp-shell')
var runSequence = require('run-sequence');

// Copy folder
gulp.task('t1', function() {
    return gulp.src('../Client/app/**/*.*')
        .pipe(gulp.dest('./src/+app/'))
})

// Rename file
gulp.task('t2', function() {
    return gulp.src('./src/+app/component.ts')
        .pipe(rename({ basename: "app.component"}))
        .pipe(gulp.dest('./src/+app/'))
})

// Delete file
gulp.task('t3', function() {
    return gulp.src('./src/+app/component.ts')
        .pipe(clean())
})

// npm run build
gulp.task('t4', shell.task([
  'npm run build'
]))

// Clean folder
gulp.task('t5', function() {
    return gulp.src('./publish/')
        .pipe(clean())
})

// Copy folder
gulp.task('t6', function() {
    return gulp.src('./dist/server/**/*.*')
        .pipe(gulp.dest('./publish/'))
})

// Copy file
gulp.task('t7', function() {
    return gulp.src('../Client/index.html')
        .pipe(gulp.dest('./publish/src/'))
})

// Copy folder
gulp.task('publish', function() {
    return gulp.src('./publish/**/*.*')
        .pipe(gulp.dest('C:/Temp/Publish/'))
})

gulp.task('default', function(){
    return runSequence('t1', 't2', 't3', 't4', 't5', 't6', 't7', 'publish');
});