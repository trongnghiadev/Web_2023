
let img_main = _('.img_main');
let img_cover = _('.img_cover');
let input_main = _('#inputMain');
let input_cover = _('#inputCover');

input_main.addEventListener('change', (e) => {
    var file = input_main.files['0'];

    if (!file) return;

    if (file.size / (1024 * 1024) >= 1) {
        alert(`Chỉ chấp nhận file dưới 1Mb`);
        return;
    }

    if (file.type == 'image/jpeg' || file.type == 'image/png') {
        img_main.src = URL.createObjectURL(file);
    } else {
        alert(`File có định dạng ${file.type} không hợp lệ`);
        return;
    }
});

input_cover.addEventListener('change', (e) => {
    var file = input_cover.files['0'];

    if (!file) return;

    if (file.size / (1024 * 1024) >= 1) {
        alert(`Chỉ chấp nhận file dưới 1Mb`);
        return;
    }

    if (file.type == 'image/jpeg' || file.type == 'image/png') {
        img_cover.src = URL.createObjectURL(file);
    } else {
        alert(`File có định dạng ${file.type} không hợp lệ`);
        return;
    }
});