$(function () {
    $('#editClickbodyPaymentMethod').on('click', '.rowEditStart', function () {
        var id = $(this).find('.editId').html();
        document.location.href = '/PaymentMethod/DeleteInOperation?id=' + id;
        //alert($(this).find('.editId').html());
    });
});