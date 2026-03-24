async function passUpdate() {
    const data = {
        Username: document.getElementById("username").value,
        Phone: document.getElementById("phone").value,
        Email: document.getElementById("email").value,
        Pass: document.getElementById("pass").value,
        PassRepeat: document.getElementById("passRepeat").value
    };

    try {
        const response = await fetch("https://localhost:7074/PassUpdate", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(data)
        });

        const result = await response.json();

        if (response.ok) {
            alert(result.message); // Başarılı mesaj
        } else {
            alert(result.message); // Hata mesajı
        }
    } catch (error) {
        console.error("Fetch hatası:", error);
        alert("Sunucuya bağlanırken hata oluştu!");
    }
}

// Kullanmak için: passUpdate() fonksiyonunu buton click veya başka yerde çağırabilirsin
document.getElementById("passUpdateBtn").addEventListener("click", passUpdate);