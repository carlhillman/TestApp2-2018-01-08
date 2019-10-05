$(window).load(see_if_i_got_any_request());

function see_if_i_got_any_request()
{
    $.ajax({
        type: "GET",
        url: "/FriendRequest/Notification/",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success:
        function (data)
        {
            $("#requestDiv").html(data);
        },
        error:
        function (data)
        {
          alert("Error");
        }

    });
}