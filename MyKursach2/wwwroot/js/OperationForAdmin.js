
$(function () {
    $('#editClickbodyOperation').on('click', '.rowEditStart', function () {
        var id = parseInt($(this).find('.editId').html());
        document.location.href = '/Operation/Create?operationId=' + id;
        //alert($(this).find('.editId').html());
    });
});