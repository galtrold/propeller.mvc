var gulp = require("gulp");
var config = require("./gulp-config.js");
var msbuild = require("gulp-msbuild");
var foreach = require("gulp-foreach");
var debug = require("gulp-debug");
var path = require("path");
var args = require('yargs').argv;

gulp.task('push', function() {

    publishProjects(config.solutionPath + "\\src", config.websitePath)

});

gulp.task('push-domain', function() {

    publishProjects(config.solutionPath + "\\src\\Domain", config.websitePath)

});

var publishProjects = function(location, dest) {
    var targets = ["Build"];

    console.log("publish to " + dest + " folder");
    return gulp.src([location + "/**/*.csproj"])
        .pipe(foreach(function(stream, file) {
            return stream
                .pipe(debug({ title: "Building project:" }))
                .pipe(msbuild({
                    targets: targets,
                    configuration: config.buildConfiguration,
                    logCommand: false,
                    verbosity: "minimal",
                    stdout: true,
                    errorOnFail: true,
                    maxcpucount: 0,
                    toolsVersion: 14.0,
                    properties: {
                        DeployOnBuild: "true",
                        DeployDefaultTarget: "WebPublish",
                        WebPublishMethod: "FileSystem",
                        DeleteExistingFiles: "false",
                        publishUrl: dest,
                        _FindDependencies: "false"
                    }
                }));
        }));
};