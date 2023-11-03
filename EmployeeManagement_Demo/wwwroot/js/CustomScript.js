function ConfirmDelete(uniqueID, isDeleteClicked) {
    var deleteSpan = `deleteSpan_${uniqueID}`;
    var confirmDeleteSpan = `confirmDeleteSpan_${uniqueID}`;
    console.log('ConfirmDelete function called.');
    if (isDeleteClicked) {
        $(`#${deleteSpan}`).hide();
        $(`#${confirmDeleteSpan}`).show();
    }
    else {
        $(`#${deleteSpan}`).show();
        $(`#${confirmDeleteSpan}`).hide();
    }

}