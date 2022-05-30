// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document.head).ready(function () {
    loadPhoto();
    loadUserBalance();
});

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
                loadUserBalance();

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

    $(function () {
        $.ajaxSetup({ cache: false });
        $("#modalItemAdd").click(function (e) {

            e.preventDefault();
            $.get("/Home/BalanceView", function (data) {
                $('#modalPay').html(data);
                $('.modalOverlay').removeClass("modal_hidden");
                $('#modalPay').removeClass("modal_hidden");
                $('.boxesList__item__price').hide();
            });
        });
    });

    $(function () {
        $.ajaxSetup({ cache: false });
        $("#modalItemTake").click(function (e) {

            e.preventDefault();
            $.get("/Home/BalanceViewWithdraw", function (data) {
                $('#modalPay').html(data);
                $('.modalOverlay').removeClass("modal_hidden");
                $('#modalPay').removeClass("modal_hidden");
                $('.boxesList__item__price').hide();
            });
        });
    });

    $(document).on('click', '.close', function (e) {
        console.log('Here');
        $('.modalOverlay').addClass("modal_hidden");
        $('#modalPay').addClass("modal_hidden");
        $('.boxesList__item__price').show();
    });

    $(document).on('click', '.SellLot', function (e) {
        var id = $('.SellLot').attr('id');
        $.post("/Home/SellLot", { drawId:id }, function (data) {

        });
    });

    $(document).on('click', '.OrderLot', function (e) {
        var id = $('.OrderLot').attr('id');
        $.post("/Home/OrderLot", { drawId: id }, function (data) {

        });
    });

    $(document).on('click', '.OrderChangeStatus', function (e) {
        var id = $('.OrderChangeStatus').attr('id');
        var text = $('#draw_Status option:selected')[0].text;
        $.post("/Home/OrderChangeStatus", { drawId: id, status: text }, function (data) {

        });
    });

    $(document).on('click', '.ShowAddress', function (e) {
        var id = $('.ShowAddress').attr('id');
        window.open("./AddressViewUser?id=" + id, "User Address", 'width=600,height=600')
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

function addUserBalance() {
    console.log("addUserBalance");
    $.post("/Home/AddBalance", $('#AddBalance').serialize(), function (data) {
        loadUserBalance();
    })
}

function takeUserBalance() {
    console.log("addUserBalance");
    $.post("/Home/TakeBalance", $('#TakeBalance').serialize(), function (data) {
        loadUserBalance();
    })
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
