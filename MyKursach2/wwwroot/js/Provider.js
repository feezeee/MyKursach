﻿function fillInputs(form) {
    let url = new URL(window.location.href);
    if (!url.search) return;
    for (let [name, value] of url.searchParams) {
        form.elements[name].value = value;
    }
}
fillInputs(document.forms.test);
$(function () {
    $('#editClickbodyProvider').on('click', '.rowEditStart', function () {
        var id = parseInt($(this).find('.editId').html());
        document.location.href = '/Provider/Edit?id=' + id;
        //alert($(this).find('.editId').html());
    });
});