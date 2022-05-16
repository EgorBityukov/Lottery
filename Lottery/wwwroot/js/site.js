// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(() => {
    loadPhoto();
});

function loadPhoto() {
    var image = document.getElementsByClassName('header__account__profile__avatar');
    //$(".header__account__profile__avatar").attr('src', 'srcImage.jpg');
    var apiUrl = "/Home/GetUserPhoto";
    $.ajax({
        url: apiUrl,
        method: "GET",
        success: function (data) {
            if (data != null) {
                var str = "data:image;base64," + data;
                $(".header__account__profile__avatar").attr('src', str); }
            //"~/images/avatar.png"
            
            //image.src = "data:image;base64," + data + ";quality=96&amp;crop=0,724,936,936&amp;ava=1";
        }
    });

}
