document.addEventListener("DOMContentLoaded", () => {

    const idusu = localStorage.getItem('usuarios');
    
    if (idusu == 0) {
        window.location.href='/'
    }
    fn_loaded_mostrar()
    setTimeout(function () {
        fn_listar()

        fn_loaded_ocultar()
    }, 1500)
    
})

const contenidoImg = document.querySelector("#img-content");

$("#imagenOT").hide()

$("#lbRegistro").on("click", function () {
    if ($("#lbRegistro").is(":checked")) {
        $("#lbRegistro").val(1)
        $("#txtregistrado").val(1)
    } else {
        $("#lbRegistro").val(0)
        $("#txtregistrado").val(0)
    }

    var opcion1 = $("#lbRegistro").val();
    var opcion2 = $("#lbPendiente").val()

    const data = {
        opcion1,
        opcion2
    }
    fn_listarMedidorRegistroPendiente(data)
})

$("#lbPendiente").on("click", function () {
    if ($("#lbPendiente").is(":checked")) {
        $("#lbPendiente").val(2)
        $("#txtpendiente").val(2)
    } else {
        $("#lbPendiente").val(0)
        $("#txtpendiente").val(0)
    }
    var opcion1 = $("#lbRegistro").val();
    var opcion2 = $("#lbPendiente").val()

    const data = {
        opcion1,
        opcion2
    }
    fn_listarMedidorRegistroPendiente(data)
})

$("#btnBuscar").on("click", function (e) {
    e.preventDefault();
    const medidor = $("#medidor").val()
    const ciclo = $("#ciclo").val()

    const data = {
        medidor: medidor,
        ciclo:ciclo
    }
    fn_loaded_mostrar()
    if (medidor == "" && ciclo == "") {
        fn_listar()
    } else {
        fn_listarMedidorCiclo(data)
    }
    fn_loaded_ocultar()
})

$("#nuevaLectura").on("change", function (e) {
    var nuevalectura = parseInt(e.target.value)
    var newLe;
    if (isNaN(newLe) || newLe == 0) {
        newLe = 0
    } else {
        newLe = nuevaLectura
    }
    if (parseInt(e.target.value) == 0) {
        fn_listarTObs(newLe)
        //$("#caja-tipoObs").show()
    } else {
        fn_listarTObs(nuevalectura)
        //$("#caja-tipoObs").hide()
    }
})

function fn_listarMedidorCiclo(data) {

    $('#tblListado').DataTable({
        "bDestroy": true,
        "ajax": {
            url: "/Listado/ListadoMedidorCiclo",
            type: "POST",
            "dataSrc":"",
            data: data,
        },
        "columns": [
            {
                "data": null,
                "bSortable": false,
                "mRender": function (data, type, value) {
                    return `<button type="button" id="editar" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" data-ot="${value["idOrdenTrabajo"]}">Editar</button>`
                }
            },
            
            { "data": "medidor" },
            {
                "data": "fechaHoraLectura",
                "bSortable": false,
                "mRender": function (data, type, value) {
                    const fecha = new Date(value["fechaHoraLectura"]);
                    const horas = fecha.getHours();
                    var ampm = horas >= 12 ? 'pm' : 'am'
                    var mes = fecha.getMonth() + 1 < 10 ? "0" + (fecha.getMonth() + 1) : fecha.getMonth();
                    var dia = fecha.getDate() < 10 ? "0" + fecha.getDate() : fecha.getDate();
                    var anio = fecha.getFullYear();

                    var fechas = dia + "/" + mes + "/" + anio;
                    return `${value["fechaHoraLectura"] ? fechas + " " + fecha.toLocaleTimeString() : "Pendiente"}`
                }
            },
            { "data": "ciclo" },
            { "data": "ordenReparto" },
            { "data": "usuarioReparto"}
            //{ "data": "nuevaLectura" }
            
        ]
    })
}

function fn_listarMedidorRegistroPendiente(data) {

    $('#tblListado').DataTable({
        "bDestroy": true,
        "ajax": {
            url: "/Listado/ListadoRegistroPendiente",
            type: "POST",
            "dataSrc": "",
            data: data,
        },
        "columns": [
            {
                "data": null,
                "bSortable": false,
                "mRender": function (data, type, value) {
                    return `<button type="button"  id="editar" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal" data-ot="${value["idOrdenTrabajo"]}">Editar</button>`
                }
            },
           
            { "data": "medidor" },
            {
                "data": "fechaHoraLectura",
                "bSortable": false,
                "mRender": function (data, type, value) {
                    const fecha = new Date(value["fechaHoraLectura"]);
                    const horas = fecha.getHours();
                    var ampm = horas >= 12 ? 'pm' : 'am'
                    var mes = fecha.getMonth() + 1 < 10 ? "0" + (fecha.getMonth() + 1) : fecha.getMonth();
                    var dia = fecha.getDate() < 10 ? "0" + fecha.getDate() : fecha.getDate();
                    var anio = fecha.getFullYear();

                    var fechas = dia + "/" + mes + "/" + anio;
                    return `${value["fechaHoraLectura"] ? fechas + " " + fecha.toLocaleTimeString() : "Pendiente"}`
                }
            },
            { "data": "ciclo" },
            { "data": "ordenReparto" },
            { "data": "usuarioReparto" }
            //{ "data": "nuevaLectura" }
            
        ]
    })
}

function fn_listar() {
    //fn_redirect()
    try {
        $('#tblListado').DataTable({
            "bDestroy": true,
            "ajax": {
                "type":"POST",
                "url": "/Listado/ListadoMedidor",
                "dataSrc": ""
            },
            "columns": [
                {
                    "data": null,
                    "bSortable": false,
                    "mRender": function (data, type, value) {
                        return `<button type="button" id="editar" class="btn btn-primary" data-toggle="modal" data-ot="${value["idOrdenTrabajo"]}" data-target="#exampleModal">Editar</button>`
                    }
                },

                { "data": "medidor" },
                {
                    "data": null,
                    "bSortable": false,
                    "mRender": function (data, type, value) {
                        const fecha = new Date(value["fechaHoraLectura"]);
                        const horas = fecha.getHours();
                        var ampm = horas >= 12 ? 'pm' : 'am'
                        var mes = fecha.getMonth() + 1 < 10 ? "0" + (fecha.getMonth() + 1) : fecha.getMonth();
                        var dia = fecha.getDate() < 10 ? "0" + fecha.getDate() : fecha.getDate();
                        var anio = fecha.getFullYear();

                        var fechas = dia + "/" + mes + "/" + anio;
                        return `${value["fechaHoraLectura"] ? fechas + " " + fecha.toLocaleTimeString() : "Pendiente"}`
                    }
                },
                { "data": "ciclo" },
                { "data": "ordenReparto" },
                { "data": "usuarioReparto" }
                //{ "data": "nuevaLectura" }

            ]
        });

    } catch (error) {
        console.log("Error")
        //window.location.href='/'
    }
}

function fn_ExportExcel(data) {
    $.ajax({
        type: "POST",
        url: "/Listado/exportData",
        dataType: "json",
        data: data,
        success: function (resp) {
            console.log(resp)
        },
        error: function () {

        }
    })
}

$(document).on("click", "#editar", function (e) {
    e.preventDefault()
    var idordentrabajo = e.target.dataset.ot;
    limpiar();
    fn_loaded_mostrar()
    $.ajax({
        type: "POST",
        url: "/Listado/obtenerOT",
        dataType: "json",
        data: {
            idOrdenTrabajo: idordentrabajo
        },
        success: function (resp) {            
            const {
                medidor, departamentoSuministro, provinciaSuministro, distritoSuministro, detalleDireccion,
                nuevaLectura, observacion, idOrdenTrabajo, imagenes, coordenadaX, coordenadaY, ruta,
                idTipoObservacion
            } = resp;
            setTimeout(() => {
                fn_loaded_ocultar()
                fn_listarTObs(nuevaLectura, idTipoObservacion)
            }, 400)
            
            $("#idordentrabajos").val(idOrdenTrabajo)
            $("#medidorHtml").html(medidor)
            $("#departamento").html(departamentoSuministro)
            $("#provincia").html(provinciaSuministro)
            $("#distrito").html(distritoSuministro)
            $("#direccion").html(detalleDireccion)
            $("#nuevaLectura").val(nuevaLectura)
            
            if (imagenes == '') {
                //$("#imagenOT").hide()
            } else {
                //$("#imagenOT").show()
                //document.querySelector("#imagenOT").src = "\\Uploads\\OrdenTrabajo\\"+imagenes;
                //$("#imgot").val(imagenes);
            }
            fn_mostrar_imagenes(imagenes.slice(0,-1))

            fn_mostrarUbicacion(resp.coordenadaX, resp.coordenadaY);
        },
        error: function (req, status, error) {
            console.log(status);
        }
    })
})

function iniciarMap() {
    var coord = { lat: -34.5956145, lng: -58.4431949 };
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 10,
        center: coord
    });
    var marker = new google.maps.Marker({
        position: coord,
        map: map
    });
}


function limpiar() {
    $("#idordentrabajos").val("")
    $("#observaciones").val("")
    $("#fechalectura").val("")
    $("#imagenLectura").val("")
}


$("#formRegistro").on("submit", function (e) {
    e.preventDefault()
    
    var nuevaLectura = $("#nuevaLectura").val()
    var observaciones = $("#observaciones").val()
    var idot = $("#idordentrabajos").val()
    var imgot = $("#imgot").val();
    var tipoobse = $("#tipoobs").val();

    var dato_archivo = $('#imagenLectura').get(0);
    var files = dato_archivo.files;
    const test = new Image();

    if (tipoobse == 0) {
        swal({
            title: "Error",
            text: "Es obligatorio seleccionar el tipo de observacion",
            icon: "error"
        })
        return;
    }

    if (imgot == '') {
        if ($('#imagenLectura').val() == '' ) {
            swal({
                title: "Error",
                text: "Completar la lectura e Imagenes, son obligatorios ",
                icon: "error"
            })
            return
        }

        if (files.length > 3 ) {
            swal({
                title: "Error",
                text: "Se deben de seleccionar tres imagenes",
                icon: "error"
            })
            return
        }
    }

    var formdata = new FormData();
    for (var i = 0; i < files.length; i++) {
        formdata.append("files", files[i])
    }

    formdata.append("NuevaLectura", nuevaLectura);
    formdata.append("Observacion", observaciones);
    formdata.append("IdOrdenTrabajo", idot);
    formdata.append("Imagenes", imgot);
    formdata.append("IdTipoObservacion", tipoobse);

    fn_loaded_mostrar();

    $.ajax({
        type: "POST",
        url: "/Listado/guardarLectura",
        dataType: "json",
        data: formdata,
        processData: false,
        contentType: false,
        success: function (resp) {
            const { idOrdenTrabajo } = resp;
            fn_loaded_ocultar()
            if (idOrdenTrabajo != 0) {
                swal({
                    title: "Éxito",
                    text: "Se registró la lectura correctamente",
                    icon: "success"
                })
                $("#exampleModal").modal("hide")
                $("#observaciones").hide()
                $("#imagenOT").hide()
                var opcion1 = $("#lbRegistro").val();
                var opcion2 = $("#lbPendiente").val();

                const data = {
                    opcion1,
                    opcion2
                }
                fn_listarMedidorRegistroPendiente(data)
                limpiar()
            }
        }, error: function (req, status, error) {
        }
    })
})

function fn_mostrarUbicacion(CoordenaX, CoordenaY) {
    const maps = document.querySelector("#map")
    maps.src = `https://www.google.com/maps/embed?pb=!1m14!1m12!1m3!1d1950.986966216731!2d${CoordenaX}!3d${CoordenaY}!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!5e0!3m2!1ses!2spe!4v1680559733941!5m2!1ses!2spe`;
}

function fn_mostrarUbi(CoordenadaX, CoordenadaY) {
    var container = L.DomUtil.get('map');
    if (container != null) {
        container._leaflet_id = null;
    }
    var map = L.map('map').setView([CoordenadaY, CoordenadaX], 17);

    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);

    L.marker([CoordenadaY, CoordenadaX]).addTo(map);
        //.bindPopup('A pretty CSS3 popup.<br> Easily customizable.')
        //.openPopup();
    
}

function fn_redirect() {
    $.ajax({
        type: "GET",
        url: "/Listado/sesion",
        dataType: "json",
        processData: false,
        contentType: false,
        success: function (resp) {
            const { IdUsuario } = resp
            if (IdUsuario == 0) {
                window.location.href = '/';
            }
        }, error: function (req, status, error) {
        }
    })
}

function fn_listarTObs(nuevaLectura, idTipoObservacion) {
    const selectInput = document.querySelector("#tipoobs");
    var newLe
    while (selectInput.firstChild) {
        selectInput.removeChild(selectInput.firstChild);
    }

    if (isNaN(nuevaLectura) || newLe ==0) {
        newLe = 0
    } else {
        newLe = nuevaLectura
    }

    const option0 = document.createElement("option");
    option0.value = 0;
    option0.innerHTML = `-- seleccione --`;
    selectInput.appendChild(option0);
    $.ajax({
        type: "GET",
        url: "/Listado/ListarTipoObservaciones",
        dataType: "json",
        success: function (resp) {
            
            resp.forEach((tipoOb) => {
                const { idTipoObservacion, descripcion } = tipoOb;
                const option = document.createElement("option");
                if (nuevaLectura == 0) {
                    if (idTipoObservacion != 21) {
                        
                        option.value = idTipoObservacion;
                        option.innerHTML = `${descripcion}`;
                        selectInput.appendChild(option);
                    }
                } else {
                    if (idTipoObservacion == 21) {
                        
                        option.value = idTipoObservacion;
                        option.innerHTML = `${descripcion}`;
                        selectInput.appendChild(option);
                    }
                }
            })
            if (newLe != 0) {
                $("#tipoobs").val(21).trigger('change')
            } else {
                $("#tipoobs").val(idTipoObservacion).trigger('change')
            }
            
        }

    })
}

function fn_mostrar_imagenes(imgs) {
    if (imgs.length != 0) {
        $("#imgot").val(imgs)
        fn_limpiar_contenedor_imagenes()
        const splits = imgs.split(";");
        splits.forEach((imgs) => {
            const div = document.createElement("div");
            contenidoImg.classList.add("contenedor-img");
            div.innerHTML = `<img src="/Uploads/OrdenTrabajo/${imgs.trim()}" class="w-100" />`;
            contenidoImg.appendChild(div);
        })
    }
    
}

function fn_limpiar_contenedor_imagenes() {
    while (contenidoImg.firstChild) {
        contenidoImg.removeChild(contenidoImg.firstChild);
    }
}