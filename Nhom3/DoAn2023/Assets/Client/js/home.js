
window.addEventListener('click', () => {
    _(".search__autocom--box").classList.remove('active');
})

// Show login form
__('#btn-login, .login--close, .main__login').forEach((item) => {
    item.addEventListener('click', () => {
        _('.main__login').classList.toggle('show');
    })
})

__('.register--close, .main__register').forEach((item) => {
    item.addEventListener('click', () => {
        _('.main__register').classList.toggle('show');
    })
})

__('.main__login--wrapper, .main__register--wrapper').forEach((item) => {
    item.addEventListener('click', (e) => {
        e.stopPropagation();
    });
});

_('#link-register').addEventListener('click', (e) => {
    _('.main__register').classList.add('show');
    _('.main__login').classList.remove('show');
});

_('#link-login').addEventListener('click', (e) => {
    _('.main__register').classList.remove('show');
    _('.main__login').classList.add('show');
});

// Login controller
var user = {
    init: function () {
        user.login();
        user.register();
    },

    login: function () {
        $('#form_btn-login').off('click').on('click', (e) => {
            let login_email = $('#login_email').val();
            let login_password = $('#login_password').val();

            var data = {
                "login_email": login_email,
                "login_password": login_password
            };

            if (login_email.length > 0 && login_password.length > 0) {
                e.preventDefault();

                $.ajax({
                    url: "/Home/Login",
                    type: "POST",
                    data: JSON.stringify(data),
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {
                        if (response.success) {
                            window.location.href = "/"
                        } else {
                            $('#login_message').text(response.data);
                            setTimeout(() => {
                                $('#login_message').text("");
                                $('#login_message').css("display","block");
                            }, 2000);
                        }
                    },
                    error: () => {
                        alert("Đã có lỗi xảy ra...");
                    }
                });
            }
        })
    },

    register: function () {
        $('#form_btn-register').off('click').on('click', (e) => {
            let register_name = $('#register_name').val();
            let register_email = $('#register_email').val();
            let register_password = $('#register_password').val();
            let password_confirm = $('#password_confirm').val();

            var data = {
                "register_name": register_name,
                "register_email": register_email,
                "register_password": register_password,
                "password_confirm": password_confirm
            };

            if (register_name.length > 0 && register_email.length > 0 && register_password.length > 0) {
                e.preventDefault();

                $.ajax({
                    url: "/Home/Register",
                    type: "POST",
                    data: JSON.stringify(data),
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {
                        if (response.success) {
                            _('.main__register').classList.remove('show');
                            _('.main__login').classList.add('show');
                        } else {
                            $('#register_message').text(response.data);
                            setTimeout(() => {
                                $('#register_message').text("");
                                $('#register_message').css("display", "block");
                            }, 2000);
                        }
                        console.log(response)
                    },
                    error: () => {
                        alert("Đã có lỗi xảy ra...");
                    }
                });
            }
        })
    }
}

user.init();