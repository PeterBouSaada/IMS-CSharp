function ShowUsersMenu() {
    var userContainer = document.getElementById("user-container");
    var addContainer = document.getElementById("add-container");
    setTimeout(() => { userContainer.style.display = "block"; addContainer.style.display = "none"; }, 100);
    userContainer.style.opacity = "1";
    addContainer.style.opacity = "0";
}

function ShowAddMenu() {
    var userContainer = document.getElementById("user-container");
    var addContainer = document.getElementById("add-container");
    setTimeout(() => { addContainer.style.display = "flex"; userContainer.style.display = "none"; }, 100);
    addContainer.style.opacity = "1";
    userContainer.style.opacity = "0";
}
