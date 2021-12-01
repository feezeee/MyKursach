$(function () {
    $('#editClickbodyDeliveryGood').on('click', '.rowEditStart', function () {
        var id = parseInt($(this).find('.editId').html());
        document.location.href = '/DeliveryGood/Edit?id=' + id;
        //alert($(this).find('.editId').html());
    });
});