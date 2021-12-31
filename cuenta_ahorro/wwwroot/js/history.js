
function GetReport(Elem) {
    ajaxViews("/History/GetReport/" + Elem.getAttribute('AccountNumber'), "GET", "HTML", {}, function (response) {
        document.getElementById('estado').innerHTML = response
        document.getElementById('IdOpeningSavingAccount').value = Elem.id
    })
}
