function login() {
    //Parametler ve Id Ler tanınlanmıştır 
    let user = document.getElementById("user").value;
    let pass = document.getElementById("pass").value;
    // Backend e Bağlan ardından User ve pass adlı değişkenleri gonder 
    fetch("http://localhost:7074/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            Username: user,
            Password: pass
        })
    })
        .then(async res => {

            const data = await res.json().catch(() => null);

            if (!res.ok) {
                alert(data?.message || "Kullanıcı adı veya şifre hatalı");
                return;
            }

            alert(data?.message || "Giriş başarılı!"); //Backend Onaylar ise devam et
            window.location.href = "index.html";
        })
        .catch(err => {
            console.error("Fetch hatası:", err);
            alert("Sunucuya bağlanılamadı!");
        });
}

function kayıt() {
    //Kayıt Ol sayfasına yonlendirme
    window.location.href = "kayıtol.html";
}
function Pass_Update() {
    //Şifre Yenileme Sayfasına Yonlendirme
    window.location.href = "passwordupdate.html";
} 