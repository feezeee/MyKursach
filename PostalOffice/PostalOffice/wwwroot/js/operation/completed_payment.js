$(function () {
    $('#editClickbodyCompletedPayment').on('click', '.rowEditStart', function () {
        var id = parseInt($(this).find('.editId').html());
        document.location.href = '/CompletedPayment/Edit?id=' + id;
        //alert($(this).find('.editId').html());
    });
});