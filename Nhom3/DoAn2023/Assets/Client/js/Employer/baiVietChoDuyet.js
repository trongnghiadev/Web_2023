var load = function (keyWord, pageIndex, pageSize) {
    $.ajax({
        url: "/nha-tuyen-dung/BaiViet/GetPaging",
        data: {
            trangThai: false,
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
                str += "<td>" + value.MaBaiViet + "</td>";
                str += "<td><img src='" + value.AnhChinh + "' width='50' height='50' ></td>";
                str += "<td>" + value.TenBaiViet + "</td>";
                str += "<td>" + value.TenTacGia + "</td>";
                str += "<td>" + value.ThoiGian + "</td>";
                str += "<td>" + value.LuotXem + "</td>";
                str += "<td>" + value.TrangThai + "</td>";
                str += '<td class="d-flex"><a class="btn btn-warning" href="/nha-tuyen-dung/BaiViet/Edit/' + value.MaBaiViet + '">Sửa</a>';
                str += '<a class="btn btn-danger ml-1" href="#" data-user=' + value.MaBaiViet + '>Xóa</a>';
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

//click delete button
$("body").on("click", "#datatablesSimple a.btn.btn-danger", function (event) {
    event.preventDefault();
    var member_delete = $(this).attr('data-user');
    if (confirm("Bạn có muốn xóa bài viết có Mã = " + member_delete + " này không?")) {
        $.ajax({
            url: "/nha-tuyen-dung/BaiViet/Delete",
            type: "POST",
            data: { maBaiViet: member_delete },
            dataType: "json",
            success: (result) => {
                location.reload();
            }
        });
    }
});