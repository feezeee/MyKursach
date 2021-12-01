$(function () {
    $('#editClickbodySoldGood').on('click', '.rowEditStart', function () {
        var id = parseInt($(this).find('.editId').html());
        document.location.href = '/SoldGood/Edit?id=' + id;
        //alert($(this).find('.editId').html());
    });
});