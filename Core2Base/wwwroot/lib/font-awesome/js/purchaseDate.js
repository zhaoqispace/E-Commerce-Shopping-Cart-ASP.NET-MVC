

window.onload = function () {
    let activities = document.getElementsByClassName('date_option');
    let product = document.getElementsByClassName('product');
    for (let i = 0; i < activities.length; i++) 
        activities[i].addEventListener('change', (e) => {
            var date = e.target.value;
            console.log(`e.target.value = ${e.target.value}`);
            console.log(`activities.options[activities.selectedIndex].value = ${activities[i].options[activities[i].selectedIndex].value}`);
            var productID = $(product[i]).html();
            sendDate(date, i, productID);
        });
}

function sendDate(date, id, productID) {
    let xhr = new XMLHttpRequest();
    xhr.open("POST", "/Purchase/GetActivationStatus");
    xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");
    xhr.onreadystatechange = function () {
        if (this.readyState === XMLHttpRequest.DONE) {
            // receives response from server
            if (this.status == 200) {
                let data = JSON.parse(this.responseText);
                console.log("Status" + data.status);
                let activations = document.getElementsByClassName('activation_status');
                console.log("Operation Status: " + data.success);
                $(activations[id]).empty();
                for (var code in data.status) {
                    console.log(data.status[code]);
                    $(activations[id]).append(
                        $('<option></option>').val(data.status[code]).html(data.status[code])
                    );
                }
            }
        }
    };

    // send like/unlike choice to server
    xhr.send(JSON.stringify({
        Date: date,
        ProductID: productID
    }));
}
