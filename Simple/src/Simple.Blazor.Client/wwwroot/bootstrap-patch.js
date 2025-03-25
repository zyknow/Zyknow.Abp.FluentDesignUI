$(document.querySelectorAll('.dropdown-toggle')).each(function ( index, element ) {
    
    if (element.getAttribute('data-initialized')) {
        return;
    }

    element.setAttribute('data-initialized', 'true');
    $(element).on("click", function () {
        new bootstrap.Dropdown($(this)).toggle();
    })
});
