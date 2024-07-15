function openModal() {
    console.log("test show modal")
    document.getElementById('modal').classList.remove('hidden');
}

function closeModal() {
    console.log("test hide modal")
    document.getElementById('modal').classList.add('hidden');
}

function reloadPage() {
    location.reload();
}