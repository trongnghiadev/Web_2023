var load = function (keyWord, pageIndex, pageSize) {
    $.ajax({
        url: "/Admin/TinTuyenDung/GetPaging",
        data: {
            trangThai: true,
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
                str += "<td>" + value.MaTTD + "</td>";
                str += "<td>" + value.TenCongViec + "</td>";
                str += "<td>" + value.TenNTD + "</td>";
                str += "<td>" + value.SoLuong + "</td>";
                str += "<td>" + value.NgayDang + "</td>";
                str += "<td>" + value.HanNop + "</td>";
                str += "<td>" + value.LuotXem + "</td>";
                str += "<td><span class='badge badge-success'>" + value.TrangThai + "</span></td>";
                str += '<td style="display: inline-grid;"><a style="min-width: 90px" class="btn btn-warning" href="/Admin/TinTuyenDung/Edit/' + value.MaTTD + '">Cập nhật</a>';
                str += "</td></tr>";

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