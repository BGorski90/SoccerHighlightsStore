$(document).ready(function () {
    $("#addCategoryBtn").click(function () {
        var category = $("#addCategoryTxtBox").val();
        if (category !== null && category !== "")
            $.post("/Videos/AddCategory", { Name: category })
                .success(function (data) {
                    $("#categoriesList").append($('<option/>', {
                        value: category,
                        text: category
                    }));
                    $("#addCategoryTxtBox").val("");
                })
                .error(function (status, data) {
                    //handle error
                })
    })
});
