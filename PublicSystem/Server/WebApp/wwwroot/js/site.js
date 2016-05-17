// Write your Javascript code.

// function.name (all IE)
// Remove once https://github.com/angular/angular/issues/6501 is fixed.
/*! @source http://stackoverflow.com/questions/6903762/function-name-not-supported-in-ie*/
if (!Object.hasOwnProperty('name')) {
    Object.defineProperty(Function.prototype, 'name', {
        get: function() {
            var matches = this.toString().match(/^\s*function\s*((?![0-9])[a-zA-Z0-9_$]*)\s*\(/);
            var name = matches && matches.length > 1 ? matches[1] : "";
            // For better performance only parse once, and then cache the
            // result through a new accessor for repeated access.
            Object.defineProperty(this, 'name', {value: name});
            return name;
        }
    });
}

// Fixes undefined module function in SystemJS bundle
function module() {}
