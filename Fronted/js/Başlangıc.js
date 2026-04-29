function sayacBaslat(hedef, elementId) {
    let baslangic = 0;
    const artis = hedef / 100;
    const element = document.getElementById(elementId);

    const guncelle = setInterval(() => {
        baslangic += artis;
        if (baslangic >= hedef) {
            element.innerText = Math.floor(hedef).toLocaleString();
            clearInterval(guncelle);
        } else {
            element.innerText = Math.floor(baslangic).toLocaleString();
        }
    }, 20);
}

window.onload = () => {
    sayacBaslat(1250, "aktif-sayi");
};

function Basla_btn() {
    window.location.href = "giriş.html";
}