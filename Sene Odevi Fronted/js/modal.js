// ======================
// MODAL KONTROL
// ======================
function openModal() {
    modal.style.display = "flex";
    document.body.style.overflow = "hidden";
}

function closeModal() {
    modal.style.display = "none";
    document.body.style.overflow = "auto";
}

// dışarı tıklayınca kapat
window.addEventListener("click", function (e) {
    if (e.target === modal) {
        closeModal();
    }
});

// ESC ile kapat
document.addEventListener("keydown", function (e) {
    if (e.key === "Escape") {
        closeModal();
    }
});