// ======================
// GLOBAL
// ======================
let modal;

// DOM tamamen yüklendiğinde çalış
document.addEventListener("DOMContentLoaded", () => {
    modal = document.getElementById("modal");
});

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

// ======================
// MODAL KONTROL
// ======================
function openModal() {
    modal.style.display = "flex"; // FIX
    document.body.style.overflow = "hidden";
}

function closeModal() {
    modal.style.display = "none";
    document.body.style.overflow = "auto";
}

// dışarı tıklayınca kapat
window.addEventListener("click", function (e) {
    if (e.target === modal) {
        closeModal();
    }
});

// ESC ile kapat
document.addEventListener("keydown", function (e) {
    if (e.key === "Escape") {
        closeModal();
    }
});

// ======================
// ŞİFRE SIFIRLAMA
// ======================
function sendCode() {
    const user = document.getElementById("resetUser").value.trim();

    if (!user) {
        alert("Kullanıcı adı gir");
        return;
    }

    fetch("http://localhost:7074/send-code", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ Username: user })
    })
        .then(res => res.json())
        .then(data => {
            alert(data?.message || "Kod gönderildi");
        })
        .catch(() => {
            alert("Kod gönderilemedi!");
        });
}