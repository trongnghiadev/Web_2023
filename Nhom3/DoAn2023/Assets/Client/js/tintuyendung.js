
// Show apply form
__('.apply__cv--button, .apply__button--cancel, .apply--close, .main__apply').forEach((item) => {
    item.addEventListener('click', () => {
        _('.main__apply').classList.toggle('show');
    })
})

_('.main__apply--post').addEventListener('click', (e) => {
    e.stopPropagation();
})


// Show name file upload
_('#cv_upload').addEventListener('change', (e) => {
    var file = _('#cv_upload').files[0];
    var message = _('.apply__content--uploadcv > span');

    if (!file) {
        message.innerText = null;
        return;
    }

    if(file.size / (1024 * 1024) >= 5) {
        message.style.color = 'red';
        message.innerText = "Chỉ chấp nhận file dưới 5Mb";
        return;
    }

    if(file.name.endsWith('.docx') || file.name.endsWith('.pdf') || file.name.endsWith('.doc')) {
        message.style.color = 'black';
        message.innerHTML = `${file.name} (${(file.size/1024).toFixed(3)}Kb)`;
    }else {
        message.style.color = 'red';
        message.innerText = "Chỉ chấp nhận file định dạng .docx, .doc, .pdf";
    }
});

$('#cv_type-online').change(() => {
    if ($('#cv_type-online').is(":checked")) {
        $("#cv_upload").val(null);
        $('.apply__content--uploadcv > span').text('');
    }
})

$('#cv_type-upload').change(() => {
    if ($('#cv_type-upload').is(":checked")) {
        $("input:radio[name='HoSo']").prop("checked", false);
    }
})

$('.apply__button--submit').click((e) => {
    if (!$('#cv_type-online').is(":checked") && !$('#cv_type-upload').is(":checked")) {
        alert("Bạn chưa chọn hồ sơ ứng tuyển");
        e.preventDefault();
    }
})