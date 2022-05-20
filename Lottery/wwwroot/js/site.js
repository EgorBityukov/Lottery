// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {
    //loadPhoto();
    //if ($(".userBalance").text() == '') {
    //    loadUserBalance();
    //}
    //loadUserBalance();

    const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/chat")
        .build();

    hubConnection.on('Receive', function (id, image) {
        updateTicketPhoto(id, image);
    });

    async function start() {
        try {
            await hubConnection.start();
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    };

    hubConnection.onclose(async () => {
        await start();
    });

    // Start the connection.
    start();

    $(document.body).on('click', '.ticketList__item', function (e) {
        e.preventDefault();
        var id = $(this).attr('id');
        console.log("id=" + id);

        $.ajax({
            type: "POST",
            url: "/Lots/BuyTicket",
            data: "id=" + id,
            success: function (result) {

                if (result.status == "Error") {
                    alert(result.data);
                }

                if (result.status == "Success") {
                    updateTicketPhoto(id, result.data);
                    hubConnection.invoke("Send", id, result.data);
                }

                if (result.status == "Draw") {
                    updateTicketPhoto(id, result.data);
                    startDraw();
                }

            },
            error: function () {
                alert('error!');
            }
        })
    });

});


function loadUserBalance() {
    var apiUrl = "/Home/GetUserBalace";
    $.ajax({
        url: apiUrl,
        method: "GET",
        success: function (data) {
            if (data != null) {
                $(".userBalance").text(data);
            }
        }
    });
}


function updateTicketPhoto(id, img) {
    var str = "data:image;base64," + img;
    $("a.ticketList__item#" + id + " img").attr('src', str);
    $("a.ticketList__item#" + id + " span").remove();
}

function loadPhoto() {
    var apiUrl = "/Home/GetUserPhoto";
    $.ajax({
        url: apiUrl,
        method: "GET",
        success: function (data) {
            if (data != null) {
                var str = "data:image;base64," + data;
                $(".header__account__profile__avatar").attr('src', str);
            }
        }
    });
}
