function showDiv(id, btn) {
    document.querySelectorAll('.content-div').forEach(div => div.style.display = 'none');
    document.getElementById(id).style.display = 'block';

    document.querySelectorAll('.menu button').forEach(b => b.classList.remove('active'));
    btn.classList.add('active');
}

// sayfa açıldığında ilk div
const firstButton = document.querySelector('.menu button');
showDiv('sunucu', firstButton);
function logout() {
    // Token veya giriş bilgilerini temizle
    localStorage.removeItem('token');

    // İstersen login sayfasına yönlendir
    window.location.href = 'adminpanel.html';
    alert("Çıkış Yaptınız Tekrar Deneyin")
}
