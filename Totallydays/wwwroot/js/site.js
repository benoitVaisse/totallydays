﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
showResult = (result) => {
    let $prototype = $("#prototype-flash")[0];
    let $prototypehtml = $($prototype).html();
    let message = "";
    for (const [key, value] of Object.entries(result.message)) {
        value.forEach((message) => {
            message = $prototypehtml.replace(/__message__/gi, message).replace("none", "block").replace(/__status__/gi, key);
            $($prototype).append(message);
        })

    }
}