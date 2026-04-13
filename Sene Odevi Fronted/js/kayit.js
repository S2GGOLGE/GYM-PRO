function kayit() {

    console.log("BUTON ÇALIŞTI");

    let name = document.getElementById("name").value.trim();
    let surname = document.getElementById("surname").value.trim();
    let username = document.getElementById("username").value.trim();
    let email = document.getElementById("mail").value.trim();
    let phone = document.getElementById("iletisim").value.trim();
    let password = document.getElementById("pass").value;
    let passwordRepeat = document.getElementById("passRepeat").value;

    let gender = document.getElementById("erkek").checked ? "Erkek" :
        document.getElementById("bayan").checked ? "Bayan" : "";

    let sozlesme = document.getElementById("sozlesme").checked;

    if (!name || !surname || !username || !email || !password || !passwordRepeat) {
        alert("Boş alan bırakma!");
        return;
    }

    if (password !== passwordRepeat) {
        alert("Şifreler uyuşmuyor!");
        return;
    }

    if (!sozlesme) {
        alert("Sözleşmeyi kabul etmeden kayıt olamazsın!");
        return;
    }

    let data = {
        name,
        surname,
        username,
        email,
        phone,
        gender,
        sozlesme: true,
        password
    };

    console.log("DATA:", data);

    fetch("http://localhost:7074/sign_up", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })
        .then(async res => {
            const result = await res.json();
            if (!res.ok) throw new Error(result.message);
            return result;
        })
        .then(res => {
            alert(res.message);
            if (res.success) window.location.href = "giriş.html";
        })
        .catch(err => {
            console.error(err);
            alert("Hata: " + err.message);
        });
}
function sozlesmeOnayla() {
    document.getElementById("sozlesme").checked = true;
}