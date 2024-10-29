document.addEventListener('DOMContentLoaded', function() {
    const toRegister = document.getElementById('toRegister');
    const toLogin = document.getElementById('toLogin');
    const cardWrapper = document.getElementById('card-wrapper');

    if(toRegister) {
        toRegister.addEventListener('click', function(e) {
            e.preventDefault();
            cardWrapper.style.transform = 'rotateY(180deg)';
            setTimeout(() => {
                window.location.href = 'register.html';
            }, 600);
        });
    }

    if(toLogin) {
        toLogin.addEventListener('click', function(e) {
            e.preventDefault();
            cardWrapper.style.transform = 'rotateY(-180deg)';
            setTimeout(() => {
                window.location.href = 'login.html';
            }, 600);
        });
    }
});