var load = function (Url, KeyWord, pageIndex, CapBac, DiaChi, ChuyenNganh, LoaiCV, MucLuong) {
    $.ajax({
        url: Url,
        data: {
            KeyWord: KeyWord,
            pageIndex: pageIndex,
            pageSize: 5,
            CapBac: CapBac,
            DiaChi: DiaChi,
            ChuyenNganh: ChuyenNganh,
            LoaiCV: LoaiCV,
            MucLuong: MucLuong
        },
        type: "GET",
        success: function (response) {
            var pageCurrent = response.pageCurrent;
            var toalPage = response.toalPage;

            if (response.totalRecord == 0) {
                let content = `<div>Không tìm được tin tuyển dụng phù hợp</div>`
                $(".result__total--content").html(content);
                $(".result__paging--list").html(null);
                $(".result__list--search").html(null);
                return;
            }
            $(".result__total--content").html(`Tìm được <span>${response.totalRecord}</span> công việc phù hợp với yêu cầu của bạn`);
            var str = "";
            $.each(response.data, function (index, value) {
                str += "<div class='col result__item l-12 m-6 c-12'><div class='result__item--wrapper'><div class='result__item--img'>";
                str += "<img class='full_width' src='" + value.AnhDaiDien + "'><div class='result__item--time'>" + value.DiaChi + "</div></div>";
                str += `<div class='result__item--name'><h2 class='result__name--title'><a href='/tin-tuyen-dung/${value.TieuDeTTD}-${value.MaTTD}' class='result__name--link text-line-1'>${value.TenCongViec}</a>`;
                str += "<span class='result__name--save'><a href='' title='Lưu tin tuyển dụng'><i class='fa-solid fa-heart'></i></a></span></h2>";
                str += `<div class='result__name--employer mb-5'><a href='/cong-ty/${value.TieuDeNTD}-${value.MaNTD}' class='result__employer--link text-line-1'><span class='result__icon'>`;
                str += `<i class='far fa-building'></i></span><span>${value.TenNTD}</span></a></div>`
                str += "<p class='result__salary mb-5'><span class='result__icon'><i class='fas fa-money-check-alt'></i></span>Lương: <span>" + value.MucLuong + "</span></p>";
                str += "<div class='result__date mb-5'><p class='result__date--start mb-0'><span class='result__icon'><i class='fa-regular fa-calendar-check'></i></span>";
                str += "Ngày đăng: <span>" + value.NgayDang + "</span></p><p class='result__date--end mb-0'>Hết hạn: <span>" + value.HanNop + "</span></p></div>";
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

