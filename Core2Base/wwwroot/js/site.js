// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('.plusButton').click(function Add() {
    numberInCart = $(this).prev();
    let counter = 1;
    numberInCart.val(parseInt(numberInCart.val(), 10) + counter);
});

$('.minusButton').click(function Minus() {
    numberInCart = $(this).next();
    let counter = 1;
    if (numberInCart.val() > 0 && numberInCart.val() != 0) {
        numberInCart.val(parseInt(numberInCart.val(), 10) - counter);
    }
});
