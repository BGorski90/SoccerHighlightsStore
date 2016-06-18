$(document).ready(function () {
    $.cookie.raw = true;
    $('.btn-wishlist').click(function () {
        var url = '/Wishlist/RemoveFromWishlist';
        var vid = $(this).attr('id').toString();
        var id = vid.substring(vid.indexOf('-') + 1);
        $.post(url, { videoID: id },
            SoccerHighlightsStore.clearVideoDiv(id)
        )
    });
    $('.btn-cart').click(function () {
        var vid = $(this).attr('id').toString();
        var id = vid.substring(vid.indexOf('-') + 1);
        var cart = $.cookie("Cart");
        if (!cart || cart === "") {
            cart = { Videos: [id] };
            $.cookie("Cart", JSON.stringify(cart), { path: "/" });
        }
        else {
            var cartObject = JSON.parse(cart);
            cartObject.Videos.push(id);
            $.cookie("Cart", JSON.stringify(cartObject));
        }
        $(this).remove();
    })
});
var SoccerHighlightsStore = SoccerHighlightsStore || {};
SoccerHighlightsStore.clearVideoDiv = function (id) {
    var divID = '#videodiv-' + id;
    $(divID).remove();
}