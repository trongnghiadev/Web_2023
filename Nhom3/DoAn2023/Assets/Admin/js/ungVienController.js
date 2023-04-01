var load = function (keyWord, pageIndex, pageSize) {
    $.ajax({
        url: "/Admin/UngVien/GetPaging",
        data: {
            keyWord: keyWord,
            pageIndex: pageIndex,
            pageSize: pageSize
        },
        type: "GET",
        success: function (response) {
            console.log(response)
            var pageCurrent = response.pageCurrent;
            var toalPage = response.toalPage;

            var str = "";
            var info = `Trang ${pageCurrent} / ${toalPage}`;
            $("#selection-datatable_info").text(info);
            $.each(response.data, function (index, value) {
                str += "<tr>";
                str += "<td>" + value.MaUngVien + "</td>";
                str += "<td>" + value.TenUngVien + "</td>";
                str += "<td>" + value.SoDienThoai + "</td>";
                str += "<td>" + value.GioiTinh + "</td>";
                str += "<td>" + value.NgaySinh + "</td>";
                str += "<td>" + value.DiaChi + "</td>";
                str += '<td style="display: inline-grid;"><a style="min-width: 90px" class="btn btn-success" href="/Admin/UngVien/Detail/' + value.MaUngVien + '">Xem thông tin</a>';
                str += '<a class="btn btn-danger mt-1" href="#" data-user=' + value.MaUngVien + '>Xóa</a>';
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
    if (confirm("Bạn có muốn xóa ứng viên có Mã = " + member_delete + " này không?")) {
        $.ajax({
            url: "/Admin/UngVien/Delete",
            type: "POST",
            data: { maUngVien: member_delete },
            dataType: "json",
            success: (result) => {
                location.reload();
            }
        });
    }
});