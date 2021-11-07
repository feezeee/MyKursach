$(function () {
    $('#editClickbodyGoodForSale').on('click', '.rowEditStart', function () {
        var id = parseInt($(this).find('.editId').html());
        document.location.href = '/GoodForSale/Edit?id=' + id;
        //alert($(this).find('.editId').html());
    });
});