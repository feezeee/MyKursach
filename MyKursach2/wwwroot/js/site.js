// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function fillInputs(form) {
    let url = new URL(window.location.href);
    if (!url.search) return;
    for (let [name, value] of url.searchParams) {
        form.elements[name].value = value;
    }
}
fillInputs(document.forms.test);