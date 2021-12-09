

$(function () {
    $('#editClickbodyPosition').on('click', '.rowEditStart', function () {
        var id = parseInt($(this).find('.editId').html());
        document.location.href = '/Position/Edit?id=' + id;
        //alert($(this).find('.editId').html());
    });
});



