$(function () {
    $('#editClickbodyAvailablePayment').on('click', '.rowEditStart', function () {
        var id = parseInt($(this).find('.editId').html());
        document.location.href = '/AvailablePayment/Edit?id=' + id;
        //alert($(this).find('.editId').html());
    });
});