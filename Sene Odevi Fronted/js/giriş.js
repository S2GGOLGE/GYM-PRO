function login() {
    let user = document.getElementById("user").value;
    let pass = document.getElementById("pass").value;

    fetch("https://localhost:7074/login", {
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

function kayıt() {
    window.location.href = "kayıtol.html";
}