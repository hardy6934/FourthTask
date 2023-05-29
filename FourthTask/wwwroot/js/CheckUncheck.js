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

function uncheck() {
    var uncheck = document.getElementsByTagName('input');
    for (var i = 0; i < uncheck.length; i++) {
        if (uncheck[i].type == 'checkbox') {
            uncheck[i].checked = false;
        }
    }
}