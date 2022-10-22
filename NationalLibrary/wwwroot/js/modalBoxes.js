// Tutor modal box

var tutorModal = document.getElementById("tutorModal");

// Get the button that opens the modal
var openTutorLink = document.getElementById("tutorLink");

// Get the <span> element that closes the modal
var closeTutorLink = document.getElementById("closeTutorModal");

// When the user clicks the button, open the modal 
openTutorLink.onclick = function () {
    tutorModal.style.display = "block";
}

closeTutorLink.onclick = function () {
    tutorModal.style.display = "none";
}

// Details Modal Box
var detailsModal = document.getElementById("detailsModal");

// Get the button that opens the modal
var openDetailsLink = document.getElementById("detailsLink");

// Get the <span> element that closes the modal
var closeDetailsLink = document.getElementById("closeDetailsModal");

// When the user clicks the button, open the modal 
openDetailsLink.onclick = function () {
    detailsModal.style.display = "block";
}

closeDetailsLink.onclick = function () {
    detailsModal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == tutorModal) {
        tutorModal.style.display = "none";
    }
    else if (event.target == detailsModal) {
        detailsModal.style.display = "none";
    }
}