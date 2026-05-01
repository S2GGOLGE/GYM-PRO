// ========================
// SIDEBAR KONTROL
// ========================
const menuBtn = document.getElementById("menuBtn");
const sidebar = document.getElementById("sidebar");

if (menuBtn && sidebar) {
    sidebar.classList.add("closed");
    menuBtn.onclick = () => sidebar.classList.toggle("closed");
}

// ========================
// SLIDER KONTROL
// ========================
const dotsContainer = document.getElementById("dots");
const slidesEl = document.getElementById("slides");

if (slidesEl && dotsContainer) {
    let index = 0; // DÜZELTME: global değil, if bloğu içinde tanımla
    const total = slidesEl.children.length;
    let autoTimer = null; // DÜZELTME: timer'ı tutuyoruz, gerekirse durdurabiliriz

    // Dots oluştur
    for (let i = 0; i < total; i++) {
        const dot = document.createElement("span");
        dot.classList.add("dot");
        if (i === 0) dot.classList.add("active");
        dot.onclick = () => goSlide(i);
        dotsContainer.appendChild(dot);
    }

    function goSlide(i) {
        index = (i + total) % total; // DÜZELTME: negatif index'e karşı güvenli
        updateSlider();
    }

    function updateSlider() {
        document.querySelectorAll(".dot").forEach((d, i) => {
            d.classList.toggle("active", i === index);
        });
        slidesEl.style.transform = `translateX(-${index * 100}%)`;
    }

    window.nextSlide = () => goSlide(index + 1);
    window.prevSlide = () => goSlide(index - 1);

    // DÜZELTME: Kullanıcı manuel geçiş yapınca timer sıfırlansın
    function resetTimer() {
        clearInterval(autoTimer);
        autoTimer = setInterval(() => goSlide(index + 1), 8000);
    }

    // Buton tıklamalarında timer sıfırla
    document.querySelector(".prev")?.addEventListener("click", resetTimer);
    document.querySelector(".next")?.addEventListener("click", resetTimer);

    // Slider'a hover'da durdur, çıkınca devam et
    slidesEl.closest(".slider")?.addEventListener("mouseenter", () => clearInterval(autoTimer));
    slidesEl.closest(".slider")?.addEventListener("mouseleave", resetTimer);

    resetTimer(); // setInterval yerine bunu çağır
}