function fillInputs(form) {
    let url = new URL(window.location.href);
    if (!url.search) return;
    for (let [name, value] of url.searchParams) {
        form.elements[name].value = value;
    }
}
fillInputs(document.forms.test);

let worker_add_new = document.querySelector("#worker_add_new");
let worker_add_new_popup = document.querySelector("#worker_add_new_popup");
worker_add_new.addEventListener("click", () => {
    worker_add_new_popup.classList.toggle("showPopup");
});

let closepopup = document.querySelector(".closepopup");
closepopup.addEventListener("click", () => {
    worker_add_new_popup.classList.toggle("showPopup");
});

