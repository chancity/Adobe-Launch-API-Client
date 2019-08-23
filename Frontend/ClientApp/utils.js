export function clone(object) {
    return JSON.parse(JSON.stringify(object));
}

export function isNode() {
    return typeof process === 'object' && process.versions && !!process.versions.node;
}

export function isObjectEmpty(obj) {
    for (var key in obj) {
        if (obj.hasOwnProperty(key))
            return false;
    }
    return true;
}

export function emptyForm(form) {
    var inputs = Array.from(form.querySelectorAll("input, select, textarea"));
    inputs.forEach(x => {
        var inputType = x.getAttribute("type");
        if (inputType === "checkbox" || inputType === "radio") {
            x.checked = false;
        } else {
            x.value = "";
        }
    });
}