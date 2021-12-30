function ShowCreate() {
    ajaxViews("/Client/Create", "GET", "HTML", {}, function (response) {
        document.getElementById('modal-body-client').innerHTML = response
        OpenModal("modal-client")
    })
}

function NewClient() {
    if (Validate()) {
        let FullName = document.getElementById("FullName")
        Axios("/Client/Create", {
            Id: 0,
            FullName: FullName.value
        }).then(response => {
            let data = response.data
            List()
            CloseModal('modal-client')
            Alert('alert-client', 'success', '', data.message)
        }).catch(error => {
            console.log(error)
        })
    }
}

async function List() {
    await axios.get('/Client/List/').then(response => {
        document.getElementById("list-client").innerHTML = response.data
        InitialDatatable('table-client')
    })
}

function Validate() {
    let FullName = document.getElementById("FullName")

    let dataValidate = [
        { element: FullName, message: 'Nombre completo es requerido!' },
    ]

    let validate = ValideFieldEmpty(dataValidate, "alert-modal-client")
    if (!validate) ScrollTopModal('modal-client')

    return validate
}

InitialDatatable('table-client')