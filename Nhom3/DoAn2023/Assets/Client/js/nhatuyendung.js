// Add google maps

var addr = _('#address').innerText;

function getMap(){  
    var embed= `<iframe width='100%' height='400' frameborder='0' scrolling='no' marginheight='0' marginwidth='0' src='https://maps.google.com/maps?&amp;q="${encodeURIComponent(addr)}"&amp;output=embed'></iframe>`;  
    _('.introduce__address--ggmap').innerHTML = embed;
};

getMap();

var load = function (idNTD, pageIndex) {
    $.ajax({
        url: "/Employer/GetPaging",
        data: {
            idNTD: idNTD,
            pageIndex: pageIndex,
            pageSize: 5
        },
        type: "GET",
        success: function (response) {
            var pageCurrent = response.pageCurrent;
            var toalPage = response.toalPage;

            var str = "";
            $.each(response.data, function (index, value) {
                str += "<div class='result__item l-12 m-6 c-12'><div class='result__item--wrapper'><div class='result__item--img'>";
                str += "<img class='full_width' src='" + value.AnhDaiDien + "'><div class='result__item--time'>" + value.DiaChi + "</div></div>";
                str += "<div class='result__item--name'><h2 class='result__name--title'><a href='' class='result__name--link text-line-1'>" + value.TenCongViec + "</a>";
                str += "<span class='result__name--save'><a href='' title='Lưu tin tuyển dụng'><i class='fa-solid fa-heart'></i></a></span></h2>";
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
            $(".introduce__employer--list").html(str);
        }
    });
}

var maNTD = $("#MaNTD").val();
load(maNTD, 1);

