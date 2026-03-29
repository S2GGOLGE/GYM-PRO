function Update() {
    const username = document.getElementById("username").value.trim();
    const newPass = document.getElementById("newPass").value.trim();
    const newPassRepeat = document.getElementById("newPassRepeat").value.trim();
        fetch("https://localhost:7074/updatepass", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
             username:Username,
                newPass:Pass,
                newPassRepeat:PassRepeat

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