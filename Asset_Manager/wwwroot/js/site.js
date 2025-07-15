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

//Toast
document.addEventListener('DOMContentLoaded', () => {
    const toastEl = document.querySelector('.toast');
    if (toastEl) {
        const toast = new bootstrap.Toast(toastEl, { delay: 5000, autohide: true });
        toast.show();
    }

    window.addEventListener('load', () => {
        const timing = window.performance?.timing;
        if (timing) {
            document.getElementById('pageLoadTime').textContent = `Loaded in ${timing.loadEventEnd - timing.navigationStart}ms`;
        }
        document.getElementById('currentDateTime').textContent = `| ${new Date().toLocaleDateString()} ${new Date().toLocaleTimeString()}`;
    });
});