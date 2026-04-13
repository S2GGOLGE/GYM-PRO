// TÜM MODALLARI AL
let modals = {};

// DOM yüklendiğinde
document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll(".modal").forEach(m => {
        modals[m.id] = m;
    });
});

// ======================
// MODAL AÇ
// ======================
function openModal(id) {
    const modal = modals[id];
    if (!modal) return;

    modal.style.display = "flex";
    document.body.style.overflow = "hidden";
}

// ======================
// MODAL KAPAT
// ======================
function closeModal(id) {
    const modal = modals[id];
    if (!modal) return;

    modal.style.display = "none";
    document.body.style.overflow = "auto";
}

// ======================
// DIŞ CLICK
// ======================
window.addEventListener("click", function (e) {
    Object.values(modals).forEach(modal => {
        if (e.target === modal) {
            modal.style.display = "none";
            document.body.style.overflow = "auto";
        }
    });
});

// ======================
// ESC
// ======================
document.addEventListener("keydown", function (e) {
    if (e.key === "Escape") {
        Object.values(modals).forEach(modal => {
            modal.style.display = "none";
        });
        document.body.style.overflow = "auto";
    }
});