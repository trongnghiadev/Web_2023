var _ = document.querySelector.bind(document);
var __ = document.querySelectorAll.bind(document);


// Scroll
let icon__scroll = _(".icon__scroll");
icon__scroll.addEventListener('click', function() {
    window.scroll({
        top: 0,
        left: 0,
        behavior: 'smooth'
    });
});

window.addEventListener('scroll', (event) => {
    if(this.scrollY > 200){
        icon__scroll.classList.add('show');
        _("#header").classList.add('fixed');
    }else {
        icon__scroll.classList.remove('show');
        _("#header").classList.remove('fixed');
    }
});

// Show navbar mobile
_('.header__icon--bars').addEventListener('click', () => {
    _('.header__navbar--menu').classList.toggle('show__nav');
});

_('.header__navbar--close').addEventListener('click', () => {
    _('.header__navbar--menu').classList.toggle('show__nav');
});
