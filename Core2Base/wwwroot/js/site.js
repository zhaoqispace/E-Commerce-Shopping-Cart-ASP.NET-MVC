// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.plusButton').click(function Add() {
    numberInCart = $(this).prev();
    let counter = 1;  
    if (numberInCart.val() < 99) {
        numberInCart.val(parseInt(numberInCart.val(), 10) + counter);
    }
    if (numberInCart.val() >= 99) {
        numberInCart.val(parseInt(numberInCart.val(), 10) + 0);
        alert("Sorry, The purchase limit for each product is 99.");
    }
});
        
$('.minusButton').click(function Minus() {
    numberInCart = $(this).next();
    let counter = 1;
    if (numberInCart.val() > 1 && numberInCart.val() != 0) {
        numberInCart.val(parseInt(numberInCart.val(), 10) - counter);
    }
});

$(".quantity").on("keyup", function () {

    if ($(this).val() < 1 && $(this).val() != ' ') {
        alert("Sorry, the minimum purchase quantity is 1. Please click the Remove All button to remove product from your cart.");
        $(this).val(1);
    }

    if ($(this).val() > 99) {
        alert("Sorry, The purchase limit for each product is 99.");
        $(this).val(99);
    }
});

var currentItems = 0;
$(document).ready(function () {

    $(".add-to-cart").click(function () {
        currentItems++;
        $(".qtyInCart").text(currentItems);
    });

});
