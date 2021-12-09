$(function () {
    $('#editClickbodyWorker').on('click', '.rowEditStart', function () {
        var id = parseInt($(this).find('.editId').html());
        document.location.href = '/Worker/Edit?id=' + id;
        //alert($(this).find('.editId').html());
    });
});