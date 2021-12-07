


$(function () {
    $('#editClickbodyPosition').on('click', '.rowEditStart', function () {
        var id = parseInt($(this).find('.editId').html());
        document.location.href = '/Position/Edit?id=' + id;
        //alert($(this).find('.editId').html());
    });
});


//$(function () {
//    $('#editClickbodyGender').on('click', '.rowEditStart', function () {
//        var id = parseInt($(this).find('.editId').html());
//        document.location.href = '/Gender/Edit?id=' + id;
//        //alert($(this).find('.editId').html());
//    });
//});

$(function () {
    $('#editClickbodyPaymentMethod').on('click', '.rowEditStart', function () {
        var id = parseInt($(this).find('.editId').html());
        document.location.href = '/PaymentMethod/Edit?id=' + id;
        //alert($(this).find('.editId').html());
    });
});

