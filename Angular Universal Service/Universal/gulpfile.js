var gulp = require('gulp');
var clean = require('gulp-clean');
var rename = require("gulp-rename");
var shell = require('gulp-shell')
var runSequence = require('run-sequence');

folder = '../Client/' + process.argv[2].substring(1) + '/'; // For example '../Client/web01/''

// Copy folder
gulp.task('t1', function() {
    return gulp.src(folder + '/app/**/*.*')
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
    return gulp.src(folder + '/index.html')
        .pipe(gulp.dest('./publish/src/'))
})

// Copy folder
gulp.task('publishLocal', function() {
    return gulp.src('./publish/**/*.*')
        .pipe(gulp.dest('C:/Temp/Publish/'))
})

gulp.task('default', function(){
    console.log(folder);
    return runSequence('t1', 't2', 't3', 't4', 't5', 't6', 't7');
});