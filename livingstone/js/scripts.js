var modalStatus = document.getElementById('modalStatus');
var hiddenModal = document.getElementById('hiddenModal'); 


modalStatus.addEventListener('click', function(event) {

   

    if( hiddenModal.className === 'settingsModalWindow hidden' ) {

        hiddenModal.className = 'settingsModalWindow';
    }
    else {
        hiddenModal.className = 'settingsModalWindow hidden';
    }
    
    event.stopPropagation();
});

document.addEventListener('click', function() {
    hiddenModal.className = 'settingsModalWindow hidden';
});

// Cover Photo Modal






openCoverModal.addEventListener('click', function() {
    var openCoverModal = document.getElementById('openCoverModal');
    var coverModal = document.getElementById('coverModal');

    if ( coverModal.className === 'hiddenCoverModal vishidden' ) {

        coverModal.className = 'hiddenCoverModal';
    }
});

closeCover.addEventListener('click', function() {
    var closeCover = document.getElementById('closeCover');
    coverModal.className = 'hiddenCoverModal vishidden';
});








var photomodal = document.getElementById('PhotoModal');
var openuploadModal = document.getElementById('openuploadModal');

openuploadModal.addEventListener('click', function() {

    if ( photomodal.className === 'hiddenPhotoModal vishidden' ) {

        photomodal.className = 'hiddenPhotoModal';
    }
});


var closeModal = document.getElementById('closeModal');
closeModal.addEventListener('click', function() {
    
    if ( photomodal.className === 'hiddenPhotoModal' ) {

        photomodal.className = 'hiddenPhotoModal vishidden';
    }
});

e.stopPropagation();
document.addEventListener('click', function() {
    photomodal.className = 'hiddenPhotoModal vishidden';
    hiddenModal.className = 'settingsModalWindow hidden';
});
