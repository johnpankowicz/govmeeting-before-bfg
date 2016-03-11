module.exports = function (config) {
    config.set({
        browsers: ['Chrome'],
        frameworks: ['jasmine'],
        files: [
            'SingleMeeting_tests/**/*.js',
            'Utilities_tests/**/*.js',
            '../BrowserApp/SingleMeeting/**/*.js',
            '../BrowserApp/Utilities/**/*.js'
    ]
    });
};
