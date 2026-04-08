// ======================
// MODAL KONTROL
// ======================
function openModal() {
    modal.style.display = "flex";
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
        headers: { "Content-Type": "application/json" },
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