// TCP Server'a mesaj gönderen fonksiyon
  function sendToTcpServer(mesaj) {
    fetch("https://localhost:7074/sunucu", {
      method: "POST",                     // Backend POST bekliyor
      headers: {
        "Content-Type": "application/json" // JSON gönderiyoruz
      },
      body: JSON.stringify({
        username: "admin",                // Backend modeline göre property
        password: mesaj                   // Mesaj olarak gönderiyoruz
      })
    })
    .then(res => res.text())
    .then(cevap => {
      console.log("TCP Server cevabı:", cevap);
      alert("Cevap: " + cevap);
    })
    .catch(err => {
      console.error("Hata oluştu:", err);
    });
  }

  // Butona tıklayınca fonksiyon çalışsın
  document.getElementById("sendBtn").addEventListener("click", () => {
    sendToTcpServer("Merhaba TCP Server!");
  });