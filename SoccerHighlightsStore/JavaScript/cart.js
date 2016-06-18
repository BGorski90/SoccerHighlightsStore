$(document).ready(function () {
    $.cookie.raw = true;
    $('.btn-cart').click(function () {
        var vid = $(this).attr('id').toString();
        var id = vid.substring(vid.indexOf('-') + 1);
        var cart = $.cookie("Cart");
        if (cart && cart !== "") {
            var cartObject = JSON.parse(cart);
            var index = cartObject.Videos.indexOf(id);
            if (index > -1) {
                cartObject.Videos.splice(index, 1);
                if (cartObject.Videos.length > 0)
                    $.cookie("Cart", JSON.stringify(cartObject));
                else
                    $.removeCookie("Cart", { path: "/" });
                SoccerHighlightsStore.clearCartDiv(id);
            }
        }
    })
});

var SoccerHighlightsStore = SoccerHighlightsStore || {};
SoccerHighlightsStore.clearCartDiv = function (id) {
    var divID = '#videodiv-' + id;
    $(divID).remove();
    var videos = $('.videocontainer').length;
    if (videos == 0)
        $('#checkoutButton').remove();
}