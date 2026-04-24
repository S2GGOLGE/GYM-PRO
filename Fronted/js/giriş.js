// ======================
// LOGIN
// ======================
function login() {
    const user = document.getElementById("user").value.trim();
    const pass = document.getElementById("pass").value.trim();

    if (!user || !pass) {
        alert("Boş alan bırakma");
        return;
    }

    fetch("http://localhost:7074/login", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ Username: user, Password: pass })
    })
        .then(async res => {
            const data = await res.json().catch(() => null);

            if (!res.ok) {
                alert(data?.message || "Kullanıcı adı veya şifre hatalı");
                return;
            }

            // 🔥 TOKEN KAYDET
            // OLMASI GEREKEN
            if (data?.token) {
                localStorage.setItem("token", data.token);
                localStorage.setItem("username", user);
                console.log("Token:", data.token); // 🔥
            }

            alert(data?.message || "Giriş başarılı!");
            window.location.href = "index.html";
        })
        .catch(err => {
            console.error("Fetch hatası:", err);
            alert("Sunucuya bağlanılamadı!");
        });
}

// ======================
// KAYIT SAYFASI
// ======================
function kayıt() {
    window.location.href = "kayıtol.html";
}