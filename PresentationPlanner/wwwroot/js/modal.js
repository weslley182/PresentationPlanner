function confirm(message, execute) {
    var confirmModal = new bootstrap.Modal(document.getElementById('confirmationModal'), {
        keyboard: false
    })
    var confirmText = document.getElementById('confirmText')
    $('#btnOK').on('click', function () {
        execute()
        confirmModal.hide()
    });

    confirmText.textContent = message
    confirmModal.show()
}