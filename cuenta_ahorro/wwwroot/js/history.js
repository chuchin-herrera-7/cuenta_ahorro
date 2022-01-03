
function GetReport(Elem) {
    ajaxViews("/History/GetReport/" + Elem.getAttribute('AccountNumber'), "GET", "HTML", {}, function (response) {
        document.getElementById('estado').innerHTML = response
        document.getElementById('IdOpeningSavingAccount').value = Elem.getAttribute('AccountNumberId')
        AddColor(Elem)
    })
}

function AddColor(Elem) {
    $('.add-color').removeClass('bg-warning')
    Elem.classList.add("bg-warning");
}

function InitialHistory() {
    if (document.getElementById('account-number-0')) {
        GetReport(document.getElementById('account-number-0'))
        $('#list-chat-content').removeClass('d-none')
    }
}

InitialHistory()