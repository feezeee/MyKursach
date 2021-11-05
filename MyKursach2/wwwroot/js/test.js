function fillInputs(form) {
    let url = new URL(window.location.href);
    if (!url.search) return;
    for (let [name, value] of url.searchParams) {
        form.elements[name].value = value;
    }
}
fillInputs(document.forms.test);

//$(function () {
//    $('#editClickbody').on('click', '.rowEditStart', function () {
//        var id = $(this).find('.editId').val();
//        document.location.href = '/Task/Edit/' + id;
//    });
//});

$(function () {
    $('#editClickbody').on('click', '.rowEditStart', function () {
        var id = parseInt($(this).find('.editId').html());
        document.location.href = '/Worker/Edit?id=' + id;
        //alert($(this).find('.editId').html());
    });
});

$(function () {
    $('#editClickbodyPosition').on('click', '.rowEditStart', function () {
        var id = parseInt($(this).find('.editId').html());
        document.location.href = '/Position/Edit?id=' + id;
        //alert($(this).find('.editId').html());
    });
});


$(function () {
    $('#editClickbodyGender').on('click', '.rowEditStart', function () {
        var id = parseInt($(this).find('.editId').html());
        document.location.href = '/Gender/Edit?id=' + id;
        //alert($(this).find('.editId').html());
    });
});


//let worker_add_new = document.querySelector("#worker_add_new");
//let worker_add_new_popup = document.querySelector("#worker_add_new_popup");
//worker_add_new.addEventListener("click", () => {
//    worker_add_new_popup.classList.toggle("showPopup");
//});

//let closepopup = document.querySelector(".closepopup");
//closepopup.addEventListener("click", () => {
//    worker_add_new_popup.classList.toggle("showPopup");
//});

