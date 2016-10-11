$(document).ready(function () {
    $.cookie.raw = true;
    SoccerHighlightsStore.totalVideos = $("#totalVideos").val();
    $(window).scroll(function () {
        if ($(window).scrollTop() >= $(document).height() - $(window).height()) {
            if (SoccerHighlightsStore.totalVideos > SoccerHighlightsStore.currentPage * SoccerHighlightsStore.pageSize) {
                var loader = $(document.createElement("img"));
                loader.attr('id', 'ajaxLoader');
                loader.attr('src', '/Content/Styles/ajax_loader.gif');
                loader.addClass('center');
                $("div #videos").append(loader);
                SoccerHighlightsStore.currentPage++;
                var url = SoccerHighlightsStore.baseUrl + (SoccerHighlightsStore.searchCriteria ? SoccerHighlightsStore.searchCriteria + '&' : "") + 'page=' + SoccerHighlightsStore.currentPage + '&includeTotal=false';
                $.ajax({
                    type: 'GET',
                    url: url,
                    cache: false,
                    success: function (data) {
                        SoccerHighlightsStore.searchCallback(data, false);
                    },
                    error: function () {
                        var template = $.templates('#errorTmpl');
                        var htmlTemplate = template.render();
                        $('div.body-content').html(htmlTemplate);
                    }
                });
            }
        }
    });
    $('.btn-cart').click(function () {
        SoccerHighlightsStore.toggleCart($(this));
    });
    $('.btn-wishlist').click(function () {
        SoccerHighlightsStore.toggleWishlist($(this));
    });
    $("#searchForm").submit(function (e) {
        e.preventDefault();
        var errorSpan = $("#searchForm span[data-valmsg-for='SearchContent']");
        var error = errorSpan.children();
        if (error.length > 0)
            return;
        $('html, body').css("cursor", "wait");
        $("#contentBox, #searchBtn").css("cursor", "inherit");
        SoccerHighlightsStore.currentPage = 1;
        SoccerHighlightsStore.totalVideos = -1;
        SoccerHighlightsStore.searchCriteria = $(this).serialize();
        SoccerHighlightsStore.searchUrl = SoccerHighlightsStore.baseUrl + SoccerHighlightsStore.searchCriteria + '&page=' + SoccerHighlightsStore.currentPage + '&includeTotal=true';
        $.ajax({
            type: 'GET',
            url: SoccerHighlightsStore.searchUrl,
            cache: false,
            success: function (data) {
                SoccerHighlightsStore.searchCallback(data, true);
            },
            error: function () {
                var template = $.templates('#errorTmpl');
                var htmlTemplate = template.render();
                $('div.body-content').html(htmlTemplate);
            }
        });
        $('html, body').css("cursor", "auto");
        $("#searchBtn").css("cursor", "pointer");
    });
    $("#loginForm").submit(function (e) {
        e.preventDefault();
        var error = $("#loginForm span.field-validation-error");
        if (error.length > 0)
            return;
        var email = $("#loginEmail").val();
        var pwd = $("#loginPwd").val();
        var token = $('[name=__RequestVerificationToken]').val();
        $('html, body').css("cursor", "wait");
        $("#loginEmail, #loginPwd, #loginBtn").css("cursor", "inherit");
        $.ajax({
            type: 'POST',
            url: '/Account/Login',
            cache: false,
            headers: { "__RequestVerificationToken": token },
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ Email: email, Password: pwd, returnUrl: window.location.href }),
            success: function (response) {
                if (response === "OK") {
                    $.get('/Account/GetCSRFToken', null, function (data) {
                        $("div#user").html(data);
                    });
                }
                else {
                    $("#loginPwd").val('').after('<span class="text-danger field-validation-error" data-valmsg-for="Password" data-valmsg-replace="true"><span for="loginPwd" class="">Wrong username/password</span></span>');
                }
            },
            error: function () {
                $("#loginPwd").val('').after('<span class="text-danger field-validation-error" data-valmsg-for="Password" data-valmsg-replace="true"><span for="loginPwd" class="">Wrong username/password</span></span>');
            }
        });
        $('html, body').css("cursor", "auto");
    });
});

var SoccerHighlightsStore = SoccerHighlightsStore || {};
SoccerHighlightsStore.baseUrl = '/Store/Index?';
SoccerHighlightsStore.currentPage = 1;
SoccerHighlightsStore.pageSize = 10;
SoccerHighlightsStore.toggleCart = function (element) {
    var vid = element.attr('id').toString();
    var id = vid.substring(vid.indexOf('-') + 1);
    var cart = $.cookie("Cart");
    if (!cart || cart === "") {
        cart = { Videos: [id] };
        $.cookie("Cart", JSON.stringify(cart), { path: "/" });
    }
    else {
        var cartObject = JSON.parse(cart);
        var index = cartObject.Videos.indexOf(id);
        if (index > -1) {
            cartObject.Videos.splice(index, 1);
            if (cartObject.Videos.length > 0)
                $.cookie("Cart", JSON.stringify(cartObject));
            else
                $.removeCookie("Cart", { path: "/" });
        }
        else {
            cartObject.Videos.push(id);
            $.cookie("Cart", JSON.stringify(cartObject));
        }
    }
    this.switchButton(vid, "cart");
}

SoccerHighlightsStore.toggleWishlist = function (element) {
    var url = '/Wishlist/ToggleWishlist';
    var vid = element.attr('id').toString();
    var id = vid.substring(vid.indexOf('-') + 1);
    $.post(url, { videoID: id },
        this.switchButton(vid, "wishlist")
    )
}

SoccerHighlightsStore.switchButton = function (id, kind) {
    if (!$.type(kind) === "string" || !kind || (kind !== "cart" && kind !== "wishlist"))
        return;
    var element = $('#' + id);
    var elementClass = kind === "cart" ? 'btn btn-success' : 'btn btn-primary';
    if (element.hasClass(elementClass)) {
        element.removeClass(elementClass);
        element.addClass('btn btn-danger');
        element.val('Remove from ' + kind);
    }
    else {
        element.removeClass('btn btn-danger');
        element.addClass(elementClass);
        element.val('Add to ' + kind);
    }
}

SoccerHighlightsStore.searchCallback = function (data, replace) {
    var template = $.templates('#videoTmpl');
    if (data.TotalVideos == 0) {
        $('div #videos').html("<h3>Sorry, no videos found!</h3>");
        $('html, body').css("cursor", "auto");
        $("#searchBtn").css("cursor", "pointer");
        return;
    }
    SoccerHighlightsStore.totalVideos = data.TotalVideos;
    $(data.Videos).each(function (i, el) {
        var pattern = /Date\(([^)]+)\)/;
        var results = pattern.exec(el.Added);
        var dt = new Date(parseFloat(results[1]));
        el.Added = (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
        el.IsInCart = data.Cart && data.Cart.Videos.indexOf(el.VideoID) != -1;
        if (data.Wishlist) {
            var wl = $.grep(data.Wishlist, function (e) { return e.VideoID === el.VideoID });
            el.IsInWishlist = wl.length > 0;
        }
    });
    $("#ajaxLoader").remove();
    if (replace) {
        var total = SoccerHighlightsStore.totalVideos;
        var message = "<h3> We found " + total + " video" + (total > 1 ? "s" : "") + " matching your search.</h3>";
        var summary = $("div#searchSummary");
        if (summary.length > 0) {
            $("div#searchSummary").html(message);
        }
        else {
            summary = $(document.createElement("div"));
            summary.attr('id', 'searchSummary');
            summary.html(message);
            $("div#options").after(summary);
        }
    }
    var htmlTemplate = template.render({ videos: data.Videos, cart: data.Cart, wishlist: data.Wishlist });
    if (replace)
        $('div #videos').html(htmlTemplate);
    else
        $('div #videos').append(htmlTemplate);

    var cartButtons = $('.btn-cart');
    var assignedCart = cartButtons.length - (SoccerHighlightsStore.pageSize - cartButtons.length % SoccerHighlightsStore.pageSize);
    cartButtons.splice(0, assignedCart);
    cartButtons.click(function () {
        SoccerHighlightsStore.toggleCart($(this));
    })
    var wishlistButtons = $('.btn-wishlist');
    var assignedWishlist = wishlistButtons.length - (SoccerHighlightsStore.pageSize - wishlistButtons.length % SoccerHighlightsStore.pageSize);
    wishlistButtons.splice(0, assignedWishlist);
    wishlistButtons.click(function () {
        SoccerHighlightsStore.toggleWishlist($(this));
    })
};

