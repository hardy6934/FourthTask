function checkforStatus() {
    var check = document.getElementsByClassName('status');
    for (var i = 0; i < check.length; i++) {
        if (check[i].type == 'checkbox') {
            check[i].checked = true;
        }
    }
}

function checkforDelete() {
    var check = document.getElementsByClassName('statusForDelete');
    for (var i = 0; i < check.length; i++) {
        if (check[i].type == 'checkbox') {
            check[i].checked = true;
        }
    }
}


function CheckingForStatus() {
    const checkbox = document.getElementById('myCheckbox');
    if (checkbox.checked) {
        checkforStatus();
    } else {
        var check = document.getElementsByClassName('status');
        for (var i = 0; i < check.length; i++) {
            if (check[i].type == 'checkbox') {
                check[i].checked = false;
            }
        }
    }
}

function CheckingForDelete() {
    const checkbox = document.getElementById('myCheckbox1');
    if (checkbox.checked) {
        checkforDelete();
    } else {
        var check = document.getElementsByClassName('statusForDelete');
        for (var i = 0; i < check.length; i++) {
            if (check[i].type == 'checkbox') {
                check[i].checked = false;
            }
        }
    }
}