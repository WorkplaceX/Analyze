var gulp = require('gulp');
var runSequence = require('run-sequence');

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

gulp.task('task1', function (cb) {
    sleep(2000).then(() => { 
        cb(); // Task finished. // See also https://github.com/gulpjs/gulp/blob/master/docs/API.md#async-task-support (Tasks can be made asynchronous if its fn does one of the following:)
	});
});

gulp.task('task2', function (cb) {
    sleep(1000).then(() => { 
        cb();
	});
});

gulp.task('default', function (cb) {
    runSequence('task1', 'task2', () => cb());
});
