var modal = document.getElementById("detailsModal");

// Get the button that opens the modal
var openLink = document.getElementById("detailsLink");

// Get the <span> element that closes the modal
var closeLink = document.getElementById("closeDetailsModal");

// When the user clicks the button, open the modal 
openLink.onclick = function () {
    modal.style.display = "block";
}

closeLink.onclick = function () {
    modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}
