// Tema Kaydetme
async function temaKaydet(theme) {
    try {
        const token = localStorage.getItem("token"); // 👈 token'ı al

        const response = await fetch("http://localhost:7074/theme", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                theme: theme,
                token: token  // 👈 bunu ekle
            })
        });

        const data = await response.json();

        if (data.success) {
            console.log("Tema kaydedildi:", data.message);
            document.body.className = theme;
            localStorage.setItem("theme", theme);
        } else {
            console.log("Hata:", data.message);
        }

    } catch (error) {
        console.log("Sunucu hatası:", error);
    }
}