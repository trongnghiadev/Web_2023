var size = parseInt($("#selection-datatable_length").val());

load(null, 1, size);

//click event pagination
$("body").on("click", "#load-pagination li a", function (event) {
    event.preventDefault();
    var page = $(this).attr('data-page');
    load(null, page, size);

});

//click event search
$("#txtsearch").on("keyup", function () {
    let keyWord = $("#txtsearch").val();
    let size = parseInt($("#selection-datatable_length").val());
    if (keyWord != null) {
        load(keyWord, 1, size);
    }
});

// load data
$("#selection-datatable_length").on("change", function () {
    let keyWord = $("#txtSearch").val();
    load(keyWord, 1, $(this).val());
});