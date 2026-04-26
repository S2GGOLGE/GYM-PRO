const btn = document.getElementById("menuBtn");
const sidebar = document.getElementById("sidebar");

btn.addEventListener("click", () => {
  sidebar.classList.toggle("closed");
});