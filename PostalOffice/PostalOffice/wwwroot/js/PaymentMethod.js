$(function () {
    $('#editClickbodyPaymentMethod').on('click', '.rowEditStart', function () {
        var id = parseInt($(this).find('.editId').html());
        document.location.href = '/PaymentMethod/Edit?id=' + id;
        //alert($(this).find('.editId').html());
    });
});