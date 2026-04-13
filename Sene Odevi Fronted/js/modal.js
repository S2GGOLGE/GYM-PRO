// MODAL SYSTEM (TEK NOKTA KONTROL)

function openModal(id) {
    const modal = document.getElementById(id);
    if (!modal) return;

    modal.style.display = "flex";
    document.body.style.overflow = "hidden";
}

function closeModal(id) {
    const modal = document.getElementById(id);
    if (!modal) return;

    modal.style.display = "none";
    document.body.style.overflow = "auto";
}

// DIŞA TIKLAYINCA KAPAT
window.addEventListener("click", (e) => {
    document.querySelectorAll(".modal").forEach(modal => {
        if (e.target === modal) {
            modal.style.display = "none";
            document.body.style.overflow = "auto";
        }
    });
});

// ESC KAPAT
document.addEventListener("keydown", (e) => {
    if (e.key === "Escape") {
        document.querySelectorAll(".modal").forEach(modal => {
            modal.style.display = "none";
        });
        document.body.style.overflow = "auto";
    }
});