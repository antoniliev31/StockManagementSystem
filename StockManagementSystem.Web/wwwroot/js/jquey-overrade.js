$.validator.methods.range = function (value, element, param) {
    var globalizedValue = parseFloat(value.replace(",", ".")).toFixed(2);
    return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
}

$.validator.methods.number = function (value, element) {
    var globalizedValue = parseFloat(value.replace(",", ".")).toFixed(2);
    return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(globalizedValue);
}

