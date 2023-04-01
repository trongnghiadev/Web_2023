
function load(keyWord, pageIndex, pageSize) {
    $.ajax({
        url: "/Admin/DanhMuc/GetPagingCapBac",
        data: {
            keyWord: keyWord,
            pageIndex: pageIndex,
            pageSize: pageSize
        },
        type: "GET",
        success: function (response) {
            var pageCurrent = response.pageCurrent;
            var toalPage = response.toalPage;

            var str = "";
            var info = `Trang ${pageCurrent} / ${toalPage}`;
            $("#selection-datatable_info").text(info);
            $.each(response.data, function (index, value) {
                str += "<tr>";
                str += "<td>" + value.MaCapBac + "</td>";
                str += "<td>" + value.TenCapBac + "</td>";
                str += '<td class="d-flex"><a class="btn btn-warning" data-edit=' + value.MaCapBac + '>Sửa</a>';
                str += '<a class="btn btn-danger ml-1" href="#" data-delete=' + value.MaCapBac + '>Xóa</a>';
                str += "</tr>";

                //create pagination
                var pagination_string = "";

                if (pageCurrent > 1) {
                    var pagePrevious = pageCurrent - 1;
                    pagination_string += '<li class="paginate_button page-item previous"><a href="#" class="page-link" data-page="' + pagePrevious + '">‹</a></li>';
                }
                for (var i = 1; i <= toalPage; i++) {
                    if (i == pageCurrent) {
                        pagination_string += '<li class="paginate_button page-item active"><a class="page-link" href="#" data-page=' + i + '>' + i + '</a></li>';
                    } else {
                        pagination_string += '<li class="paginate_button page-item"><a href="#" class="page-link" data-page=' + i + '>' + i + '</a></li>';
                    }
                }
                //create button next
                if (pageCurrent > 0 && pageCurrent < toalPage) {
                    var pageNext = pageCurrent + 1;
                    pagination_string += '<li class="paginate_button page-item next"><a href="#" class="page-link" data-page=' + pageNext + '>›</a></li>';
                }
                $("#load-pagination").html(pagination_string);
            });
            //load str to class="load-list"
            $("#datatablesSimple > tbody").html(str);
        }
    });
}

var btnCreate = $("#btnCreate");

btnCreate.on("click", function (event) {
    event.preventDefault();
    let name = prompt("Nhập tên cấp bậc");
    if (name) {
        $.ajax({
            url: "/Admin/DanhMuc/CreateCapBac",
            type: "POST",
            data: { TenCapBac: name },
            dataType: "json",
            success: (result) => {
                location.reload();
            }
        });
    }
});

$("body").on("click", "#datatablesSimple a.btn.btn-danger", function (event) {
    event.preventDefault();

    let itemDelete = $(this).attr('data-delete');

    if (confirm("Bạn có muốn xóa cấp bậc có mã " + itemDelete + " này không?")) {
        $.ajax({
            url: "/Admin/DanhMuc/DeleteCapBac",
            type: "POST",
            data: { MaCB: itemDelete },
            dataType: "json",
            success: (result) => {
                location.reload();
            }
        });
    }
});

$("body").on("click", "#datatablesSimple a.btn.btn-warning", function (event) {
    event.preventDefault();

    let itemEdit = $(this).attr('data-edit');
    let value = prompt("Nhập tên cấp bậc mới:");

    if (value) {
        $.ajax({
            url: "/Admin/DanhMuc/EditCapBac",
            type: "POST",
            data: {
                MaCapBac: itemEdit,
                TenCapBac: value
            },
            dataType: "json",
            success: (result) => {
                location.reload();
            }
        });
    }
});