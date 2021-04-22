$(window).on('load', function () {
    $('#page-wrapper').css("display", "block"); // показываем блок с контентом 
    $('#preloader').delay(350).fadeOut('slow'); // и скрываем сам блок прелоудера.
});