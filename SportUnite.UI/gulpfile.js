var gulp = require("gulp"),
    sass = require("gulp-sass"),
    prefix = require("gulp-autoprefixer"),
    clean = require("gulp-clean-css"),
    jshint = require("gulp-jshint"),
    uglify = require("gulp-uglify"),
    babel = require("gulp-babel"),
    concat = require("gulp-concat"),
    rename = require("gulp-rename");

gulp.task("sass", function() {
  return gulp
    .src('wwwroot/css/main.sass')
    .pipe(sass())
    .pipe(prefix(['last 15 versions', '> 1%', 'ie 8', 'ie 7'], { cascade: true }))
    .pipe(rename({suffix: ".min"}))
    .pipe(clean({keepSpecialComments: 0}))
    .pipe(gulp.dest('wwwroot/css'));
});

gulp.task("custom-js", function() {
  return gulp
    .src('wwwroot/js/src/*.js')
    .pipe(jshint())
    .pipe(jshint.reporter('default'))
    .pipe(babel({presets: ['env']}))
    .pipe(concat('custom.js'))
    .pipe(gulp.dest('wwwroot/js'));
})

gulp.task("js", ["custom-js"] , function() {
  return gulp
    .src([
      'wwwroot/bower/jquery/dist/jquery.js',
      'wwwroot/bower/materialize/dist/js/materialize.js',
      'wwwroot/js/custom.js'
    ])
    .pipe(concat('common.js'))
    .pipe(rename({suffix: ".min"}))
    .pipe(uglify())
    .pipe(gulp.dest('wwwroot/js'));
});

gulp.task("watch", function() {
  gulp.watch("wwwroot/js/src/*.js", ["js"]);
  gulp.watch(["wwwroot/css/**/*.sass", "wwwroot/css/main.sass"], ["sass"]);
});

gulp.task("default", ["watch", "sass", "js"]);
