var load = function (KeyWord, pageIndex, CapBac, ChuyenNganh, LoaiCV) {
    $.ajax({
        url: "/nha-tuyen-dung/UngTuyen/GetSearchPaging",
        data: {
            KeyWord: KeyWord,
            pageIndex: pageIndex,
            CapBac: CapBac,
            ChuyenNganh: ChuyenNganh,
            LoaiCV: LoaiCV
        },
        type: "GET",
        success: function (response) {
            var pageCurrent = response.pageCurrent;
            var toalPage = response.toalPage;

            console.log(response)

            if (response.totalRecord == 0) {
                let content = `<p class='result__total--content'>Không tìm được hồ sơ phù hợp</p>`
                $(".result__total").html(content);
                $(".result__paging--list").html(null);
                $(".result__list--search").html(null);
                return;
            }
            $(".result__total").html(`<p class='result__total--content'>Tìm được <span style="color: red;">${response.totalRecord}</span> công việc phù hợp với yêu cầu của bạn</p>`);
            var str = "";
            $.each(response.data, function (index, value) {
                str += "<div class='col result__item l-12 m-6 c-12'><div class='result__item--wrapper'><div class='result__item--img'>";
                str += `<img class='full_width' src='${value.AnhDaiDien}'><div class='result__item--time'><span class="badge badge-success">${value.CapBac}</span></div></div>`;
                str += `<div class='result__item--name'><h2 class='result__name--title'>${value.CapBac} ${value.ChuyenNganh}`;
                str += `<span class='result__name--save'><a href='/hoSoXinViec/HoSo?ungvien=${value.MaUngVien}&id=${value.MaHoSo}' class='btn btn-success' title='Xem hồ sơ xin việc'>Xem hồ sơ</a></span></h2>`;
                str += `<div class='result__name--employer'><span class='result__icon'>`;
                str += `<i class="remixicon-folder-user-fill"></i></span><span>${value.TenUngVien}</span></div>`
                str += "<p class='result__salary my-0'><span class='result__icon'><i class='remixicon-mail-send-fill'></i></span>Email: <span>" + value.Email + "</span></p>";
                str += "<div class='result__date'><p class='result__date--start mb-0'><span class='result__icon'><i class='remixicon-map-pin-time-fill'></i></span>";
                str += "Ngày sinh: <span>" + value.NgaySinh + "</span></p><p class='result__date--end mb-0'>Giới tính: <span>" + value.GioiTinh + "</span></p></div>";
                str += "<p class='result__address mb-0'><span class='result__icon'><i class='fas fa-map-marked'></i></span><span>" + value.LoaiCV + "</span></p></div></div></div>";

                //create pagination
                var pagination_string = "";

                if (pageCurrent > 1) {
                    var pagePrevious = pageCurrent - 1;
                    pagination_string += '<li class="previous"><a href="#" data-page="' + pagePrevious + '">‹</a></li>';
                }
                for (var i = 1; i <= toalPage; i++) {
                    if (i == pageCurrent) {
                        pagination_string += '<li class="active"><a href="#" data-page=' + i + '>' + i + '</a></li>';
                    } else {
                        pagination_string += '<li class=""><a href="#" data-page=' + i + '>' + i + '</a></li>';
                    }
                }
                //create button next
                if (pageCurrent > 0 && pageCurrent < toalPage) {
                    var pageNext = pageCurrent + 1;
                    pagination_string += '<li class="next"><a href="#" data-page=' + pageNext + '>›</a></li>';
                }
                $(".result__paging--list").html(pagination_string);
            });
            //load str to class="load-list"
            $(".result__list--search").html(str);
        }
    });
}

$("body").on("click", ".result__paging--list li a", function (event) {
    event.preventDefault();
    var page = $(this).attr('data-page');
    load($('#Keyword').val(), page, $('#CapBac').val(), $('#ChuyenNganh').val(), $('#LoaiCV').val());
});
