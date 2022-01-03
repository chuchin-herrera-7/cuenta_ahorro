function ShowCreate() {
    ajaxViews("/Client/Create", "GET", "HTML", {}, function (response) {
        document.getElementById('modal-body-client').innerHTML = response
        OpenModal("modal-client")
    })
}

function NewClient() {
    if (Validate()) {
        let FullName = document.getElementById("FullName")
        let Email = document.getElementById("Email")
        let Password = document.getElementById("Password")
        let ConfirmPassword = document.getElementById("ConfirmPassword")
        let Type = document.getElementById("Type")
        if (Password.value == ConfirmPassword.value) {
            Axios("/Client/Create", {
                Id: 0,
                FullName: FullName.value,
                Email: Email.value,
                Password: Password.value,
                Type: parseInt(Type.value)
            }).then(response => {
                let data = response.data
                List()
                CloseModal('modal-client')
                Alert('alert-client', 'success', '', data.message)
            }).catch(error => {
                console.log(error)
            })
        } else {
            Alert('alert-modal-client', 'danger', '', 'las contraseñas no coinciden')
        }
    }
}

async function List() {
    await axios.get('/Client/List/').then(response => {
        document.getElementById("list-client").innerHTML = response.data
        InitialDatatable('table-client')
    })
}

async function AccountList() {
    await axios.get('/Client/AccountList').then(response => {
        document.getElementById("list-account").innerHTML = response.data
    })
}


function Validate() {
    let FullName = document.getElementById("FullName")
    let Email = document.getElementById("Email")
    let Password = document.getElementById("Password")

    let dataValidate = [
        { element: FullName, message: 'Nombre completo es requerido!' },
        { element: Email, message: 'Correo es requerido!' },
        { element: Password, message: 'Contraseña es requerido!' },
        { element: ConfirmPassword, message: 'Confirmar contraseña es requerido!' },
    ]

    let validate = ValideFieldEmpty(dataValidate, "alert-modal-client")
    if (!validate) ScrollTopModal('modal-client')

    return validate
}

InitialDatatable('table-client')