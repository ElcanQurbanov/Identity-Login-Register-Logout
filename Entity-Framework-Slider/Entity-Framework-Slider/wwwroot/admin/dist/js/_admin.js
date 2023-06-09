﻿$(function () {

    $(document).on("click", ".slider-status", function () {

        let sliderId = $(this).parent().attr("data-id");
        let changeElem = $(this);
        let data = { id: sliderId }

        $.ajax({
            url: "slider/setstatus",
            type: "Post",
            data: data,
            success: function (res) {
                if (res) {
                    $(changeElem).removeClass("active-status");
                    $(changeElem).addClass("deActive-status");
                } else {
                    $(changeElem).addClass("active-status");
                    $(changeElem).removeClass("deActive-status");
                }
            }

        })
    })

    $(document).on("submit", "#category-delete-form", function (e) {
        e.preventDefault();
        let categoryId = $(this).attr("data-id");
        let deletedElem = $(this);
        let data = { id: categoryId }
        console.log("aksinin dassagi")

        $.ajax({
            url: "category/softdelete",
            type: "Post",
            data: data,
            success: function (res) {
                $(deletedElem).parent().parent().remove();
            }

        })
    })
})