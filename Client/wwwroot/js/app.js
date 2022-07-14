function setFocus() {
    var elements = document.getElementsByClassName("auto-focus");
    if (elements != null && elements.length > 0)
        elements[0].focus();
}

function showConfirmation(message, title, confirmText, cancelText, type, confirmButtonColor = '#3085d6', cancelButtonColor = '#6e7d88') {
    return new Promise((resolve) => {
        Swal.fire({
            title: title,
            //text: message,
            html: message,
            icon: type,
            confirmButtonColor: confirmButtonColor,
            cancelButtonColor: cancelButtonColor,
            showCancelButton: true,
            confirmButtonText: confirmText,
            cancelButtonText: cancelText,
        }).then((result) => {
            resolve(result.isConfirmed);
        });
    });
}