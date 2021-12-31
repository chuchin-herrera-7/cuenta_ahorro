function NewManagementAccount() {

    let IdOpeningSavingAccount = document.getElementById("IdOpeningSavingAccount")
    let Amount = document.getElementById("Amount")
    let type = $('input[name="radioInline"]:checked').val()

    if (Amount.value != "") {
        if (Amount.value > 0) {
            Axios("/ManagementAccount/Create", {
                Id: 0,
                Amount: parseFloat(Amount.value),
                Type: parseInt(type),
                Status: 0,
                IdOpeningSavingAccount: parseInt(IdOpeningSavingAccount.value)
            }).then(response => {
                let data = response.data
                CloseModal('modal-client')
                Alert('alert-transacciones', 'success', '', data.message)
                location.reload()
            }).catch(error => {
                Alert('alert-transacciones', 'danger', '', error.response.data.message)
            })
        } else {
            Alert('alert-transacciones', 'danger', '', 'El monto no puede ser menor e igual a 0')
        }
    } else {
        Alert('alert-transacciones', 'danger', '', 'Monto es requerido!!')
    }    
}