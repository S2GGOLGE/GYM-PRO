// SIDEBAR KONTROL
const menuBtn = document.getElementById("menuBtn");
const sidebar = document.getElementById("sidebar");

if (menuBtn && sidebar) {
    menuBtn.onclick = () => {
        sidebar.classList.toggle("closed");
    };
}

// SLIDER KONTROL
let index = 0;
const slides = document.getElementById("slides");
const dotsContainer = document.getElementById("dots");

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

    function updateSlider() {
        slides.style.transform = `translateX(-${index * 100}%)`;
        const allDots = document.querySelectorAll(".dot");
        allDots.forEach((d, i) => {
            d.classList.toggle("active", i === index);
        });
    }

    window.nextSlide = function () {
        index = (index + 1) % total;
        updateSlider();
    }

    window.prevSlide = function () {
        index = (index - 1 + total) % total;
        updateSlider();
    }

    function goSlide(i) {
        index = i;
        updateSlider();
    }

    // 8 Saniyede bir otomatik kaydır
    setInterval(nextSlide, 8000);
}