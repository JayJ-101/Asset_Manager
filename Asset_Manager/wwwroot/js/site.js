//Date Validation
$.validator.addMethod("purchasedatenotgreaterthantoday", function (value, element) {
    var purchaseDate = new Date(value);
    var today = new Date();
    return purchaseDate <= today;
});

$.validator.unobtrusive.adapters.add("purchasedatenotgreaterthantoday", [], function (options) {
    options.rules["purchasedatenotgreaterthantoday"] = true;
    options.messages["purchasedatenotgreaterthantoday"] = options.message;
});



$.validator.addMethod("warrantydatenotlessthantoday", function (value, element) {
    var warrantyDate = new Date(value);
    var today = new Date();
    return warrantyDate >= today;
});

$.validator.unobtrusive.adapters.add("warrantydatenotlessthantoday", [], function (options) {
    options.rules["warrantydatenotlessthantoday"] = true;
    options.messages["warrantydatenotlessthantoday"] = options.message;
});

