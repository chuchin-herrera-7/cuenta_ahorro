var ajaxViews = function (url, method, type, data, success) {
    $.ajax({
        url: url,
        method: method,
        data: data,
        dataType: type,
        success: function (response) {
            if (response != null && success != null) {
                success(response);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log("Error: " + errorThrown, "Hubo un error en la llamada:  " + url + " | " + textStatus)
        }
    });
};

// Abrir Modal
var OpenModal = function (Elem) {
    $('#' + Elem).modal('show')
}

// Cerrar Modal
var CloseModal = function (Elem) {
    $('#' + Elem).modal('hide')
}

// Inicializar tabla de acuerdo a su id
var InitialDatatable = function (Elem) {
    let table = $('#' + Elem).DataTable({
        "language": { "url": "https://cdn.datatables.net/plug-ins/1.10.25/i18n/Spanish.json" },
        "pageLength": 100,
        "dom": 'Bfrtip',
        //"ordering": false,
        "buttons": [
            'pdf', 'excel'
        ]
    });
    return table;
}

function InitialFeatherIcon(prop = {}) {
    feather.replace(prop)
}

function AxiosInterceptors(){
    // Add a request interceptor
    axios.interceptors.request.use(function (config) {
        // Do something before request is sent
        console.log("Do something before request is sent")
        document.querySelector(".preloader").style.display = "block"
        return config;
    }, function (error) {
        // Do something with request error
        console.log("Do something with request error")
        document.querySelector(".preloader").style.display = "none"
        return Promise.reject(error);
    });

    // Add a response interceptor
    axios.interceptors.response.use(function (response) {
        // Any status code that lie within the range of 2xx cause this function to trigger
        // Do something with response data
        console.log("Do something with response data")
        document.querySelector(".preloader").style.display = "none"
        return response;
    }, function (error) {
        // Any status codes that falls outside the range of 2xx cause this function to trigger
        // Do something with response error
        console.log("Do something with response error")
        document.querySelector(".preloader").style.display = "none"
        return Promise.reject(error);
    });
}

function Axios(url, data) {
    AxiosInterceptors()

    return axios.post(url, data, {
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
    })
}

function AxiosGet(url, data) {
    AxiosInterceptors()

    return axios.get(url, data, {
        headers: {
            RequestVerificationToken: $('input:hidden[name="__RequestVerificationToken"]').val()
        },
    })
}

function Alert(element, type, title, message) {
    let alert ='<div class="alert alert-'+type+'" role="alert">'+
                '<h4 class="alert-heading">'+title+'</h4>'+
                '<p class="mb-0">'+message+'</p>'+
            '</div>'
    document.getElementById(element).innerHTML = alert
    setTimeout(function () { document.getElementById(element).innerHTML = '' }, 8000)
}

function ScrollTopModal(Elem) {
    $('#' + Elem).scrollTop(0)
    console.log($('#' + Elem))
}

function ValideFieldEmpty(data, element = 'alert-modal',) {
    let isValid = true
    $.each(data, function (index, item) {
        if (item.element.value == "") {
            Alert(element, 'danger', '', item.message)
            return isValid = false
        }
    })
    return isValid
}