// ========================
// SIDEBAR KONTROL
// ========================
const menuBtn = document.getElementById("menuBtn");
const sidebar = document.getElementById("sidebar");

if (menuBtn && sidebar) {
    sidebar.classList.add("closed");
    menuBtn.onclick = () => {
        sidebar.classList.toggle("closed");
    };
}

// ========================
// SLIDER KONTROL
// ========================
let index = 0;
const dotsContainer = document.getElementById("dots");
const slides = document.getElementById("slides");

if (slides) {
    const total = slides.children.length;

    // Noktaları oluştur
    for (let i = 0; i < total; i++) {
        let dot = document.createElement("span");
        dot.classList.add("dot");
        if (i === 0) dot.classList.add("active");
        dot.onclick = () => goSlide(i);
        dotsContainer.appendChild(dot);
    }

    function goSlide(i) {
        index = i;
        updateSlider();
    }

    function updateSlider() {
        const allDots = document.querySelectorAll(".dot");
        allDots.forEach((d, i) => {
            d.classList.toggle("active", i === index);
        });
        slides.style.transform = `translateX(-${index * 100}%)`;
    }

    window.nextSlide = function () {
        index = (index + 1) % total;
        updateSlider();
    }

    window.prevSlide = function () {
        index = (index - 1 + total) % total;
        updateSlider();
    }

    // 8 saniyede bir otomatik kaydır
    setInterval(nextSlide, 8000);
}