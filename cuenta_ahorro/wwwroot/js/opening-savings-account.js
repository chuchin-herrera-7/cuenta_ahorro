function ShowCreateNewSavingsAccount() {
    ajaxViews("/Opening/Create", "GET", "HTML", {}, function (response) {
        document.getElementById('modal-body-client').innerHTML = response
        OpenModal("modal-client")
    })
}

function NewSavingsAccount() {
    swal({
        title: "Aperturar nueva cuenta?",
        text: "Comienza con tu nueva cuenta ..",
        buttons: true,
        dangerMode: true,
    })
    .then((willDelete) => {
        if (willDelete) {
            Axios("/Opening/NewOpeningSavingsAccount", {}).then(async response => {
                let data = response.data
                Alert('alert-account', 'success', '', data.message)
                await AccountList()
                InitialHistory()
            }).catch(error => {
                console.log(error)
            })
        } else {
            swal("Cancelado...");
        }
    });
    
}

function Validate() {
    let AccountNumber = document.getElementById("AccountNumber")
    let Balance = document.getElementById("Balance")

    let dataValidate = [
        { element: AccountNumber, message: 'Numero de cuenta es requerido!' },
        { element: Balance, message: 'Saldo de cuenta es requerido!' },
    ]

    let validate = ValideFieldEmpty(dataValidate, "alert-modal-client")
    if (!validate) ScrollTopModal('modal-client')

    return validate
}
