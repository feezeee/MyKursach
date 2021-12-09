
$(function () {
    $('#editClickbodyProvider').on('click', '.rowEditStart', function () {
        var id = parseInt($(this).find('.editId').html());
        document.location.href = '/Provider/Edit?id=' + id;
        //alert($(this).find('.editId').html());
    });
});