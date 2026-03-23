function kayit() {
    //Aşagı tanımlanan Parametreleri Tanımlanmıştır ve Id ler Verimiştir
    let ad = document.getElementById("name").value;
    let soyad = document.getElementById("surname").value;
    let username = document.getElementById("username").value;
    let mail = document.getElementById("mail").value;
    let iletisim = document.getElementById("iletisim").value;

    let cinsiyet =
        document.getElementById("cinsiyetErkek").checked ? "Erkek" :
            document.getElementById("cinsiyetBayan").checked ? "Bayan" : "";

    let password = document.getElementById("pass").value;
    let passRepeat = document.getElementById("passRepeat").value;
    //Backend e Bağlan ardından parametleri post et
    fetch("https://localhost:7074/Sign_up", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            name: ad,
            surname: soyad,
            username: username,
            email: mail,
            phone: iletisim,
            gender: cinsiyet,
            password: password, 
            passwordRepeat: passRepeat
        })
    })
        .then(res => res.json())
        .then(data => {
            alert(data.message)
            if (data.success) {   // backend tarafında success = true dönüyorsa
                window.location.href = "giriş.html"; // Login e git 
            }
        })
        .catch(err => {
            //Hata Olur ise consola yazdır 
            console.error("Fetch hatası:", err);
        });
}