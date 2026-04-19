import { closeModal } from "./modal.js";

document.getElementById("passForm").addEventListener("submit", async function (e) {
    e.preventDefault();

    const username = document.getElementById("username").value.trim();
    const newPass = document.getElementById("newPass").value.trim();
    const newPassRepeat = document.getElementById("newPassRepeat").value.trim();
    const resultEl = document.getElementById("result");

    // Front validation
    if (newPass !== newPassRepeat) {
        resultEl.innerText = "Şifreler uyuşmuyor ❌";
        return;
    }

    try {
        const res = await fetch("http://localhost:7074/updatepass", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                Username: username,
                NewPass: newPass,
                NewPassRepeat: newPassRepeat
            })
        });

        const data = await res.json();

        resultEl.innerText = data.message || "İşlem tamamlandı";

        if (res.ok && data.success) {
            console.log("Şifre güncellendi ✅");

            closeModal(); // 🔥 UI bağımlılığı burada temiz çözüldü

            setTimeout(() => {
                window.location.href = "giriş.html";
            }, 1000);
        } else {
            console.log("Başarısız ❌");
        }

    } catch (err) {
        console.error("Fetch hatası:", err);
        resultEl.innerText = "Sunucuya bağlanılamadı.";
    }
});