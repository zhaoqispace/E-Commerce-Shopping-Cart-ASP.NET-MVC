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

    if (/^[0-9]*$/.test($(".quantity").val()) == false) {
        alert("Sorry, no special characters or alphabets are allowed in quantity field.");
        $(this).val(1);
    }
});

//var currentItems = 0;
//$(document).ready(function () {

//    $(".add-to-cart").click(function () {
//        currentItems++;
//        $(".qtyInCart").text(currentItems);
//    });
//});

$('.search-button').click(function () {
    if ($.trim($('.searchbar').val()) == '' && ($('.searchbar').val()) != '')
        alert('Input is blank. Please fill in your search parameters');
});

window.onload = function () {
    let elemList = document.getElementsByName("add-to-cart");
    let elemList1 = document.getElementsByName("subtract-from-cart");

    for (let i = 0; i < elemList.length; i++) {
        elemList[i].addEventListener("click", onAdd);
        elemList1[i].addEventListener("click", onSubtract);
    }
}

function onAdd(event) {
    let elem = event.currentTarget;
    let elemId = elem.getAttribute("id");

    addcartlogin(elemId);
}

function addcartlogin(elemId) {
    let xhr = new XMLHttpRequest();

    xhr.open("Post", "/ShoppingCart/AddtoCart");
    xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");
    xhr.onreadystatechange = function () {
        if (this.readyState === XMLHttpRequest.DONE) {
            // check if HTTP operation is okay
            if (this.status !== 200)
                return;

            let data = JSON.parse(this.responseText);

            if (!data.success)
                return;

            let elem = document.getElementById(elemId);
            if (!elem)
                return;
            $("#shoppingCartTable").load(" #shoppingCartTable > *", function () {
                   let elemList = document.getElementsByName("add-to-cart");
                    let elemList1 = document.getElementsByName("subtract-from-cart");

                    for (let i = 0; i < elemList.length; i++) {
                        elemList[i].addEventListener("click", onAdd);
                        elemList1[i].addEventListener("click", onSubtract);
                }
            });
            return;
        }
    }

    xhr.send(JSON.stringify({ productid: elemId }));
}

function onSubtract(event) {
    let elem1 = event.currentTarget;
    let elem1Id = elem1.getAttribute("id");

    subtractcartlogin(elem1Id);

}

function subtractcartlogin(elem1Id) {
    let xhr = new XMLHttpRequest();

    xhr.open("Post", "/ShoppingCart/SubtractProductFromCart");
    xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");
    xhr.onreadystatechange = function () {
        if (this.readyState === XMLHttpRequest.DONE) {
            // check if HTTP operation is okay
            if (this.status !== 200)
                return;

            let data = JSON.parse(this.responseText);

            if (!data.success)
                return;

            let elem1 = document.getElementById(elem1Id);
            if (!elem1)
                return;

window.onload = function () {
    let elemList = document.getElementsByClassName("add-to-cart");

    for (let i = 0; i < elemList.length; i++) {
        elemList[i].addEventListener("click", onAdd);
    }
}

function onAdd(event)
{
    let elem = event.currentTarget;
    let elemId = elem.getAttribute("id");

    addcartlogin(elemId);

}

function addcartlogin(elemId)
{
    let xhr = new XMLHttpRequest();

    xhr.open("Post", "/ShoppingCart/AddtoCart");
    xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");
    xhr.onreadystatechange = function ()
    {
        if (this.readyState === XMLHttpRequest.DONE)
        {
            // check if HTTP operation is okay
            if (this.status !== 200)
                return;

            let data = JSON.parse(this.responseText);

            if (!data.success)
                return;

            let elem = document.getElementById(elemId);
            if (!elem)
                return;
        }
    }

    xhr.send(JSON.stringify({productid: elemId}));
}
            $("#shoppingCartTable").load(" #shoppingCartTable > *", function () {
                let elemList = document.getElementsByName("add-to-cart");
                let elemList1 = document.getElementsByName("subtract-from-cart");

                for (let i = 0; i < elemList.length; i++) {
                    elemList[i].addEventListener("click", onAdd);
                    elemList1[i].addEventListener("click", onSubtract);
                }
            });
            return;
        }
    }

    xhr.send(JSON.stringify({ productid: elem1Id }));
}

