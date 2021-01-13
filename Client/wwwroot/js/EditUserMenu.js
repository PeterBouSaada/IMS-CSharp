function ShowEditScreen() {
    var editContainer = document.getElementById("edit-container");
    var cardsContainer = document.getElementById("user-container");
    setTimeout(() => { editContainer.style.display = "block"; cardsContainer.style.display = "none"; }, 100);
    editContainer.style.opacity = "1";
    cardsContainer.style.opacity = "0";
}

function RemoveEditScreen() {
    var editContainer = document.getElementById("edit-container");
    var cardsContainer = document.getElementById("user-container");
    setTimeout(() => { cardsContainer.style.display = "block"; editContainer.style.display = "none"; }, 100);
    cardsContainer.style.opacity = "1";
    editContainer.style.opacity = "0";
}