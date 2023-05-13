const loaded = document.querySelector("#loaded");

document.addEventListener("DOMContentLoaded", function () {
    var idusu = localStorage.getItem('usuarios')
    if (idusu == 0) {
        setTimeout(() => {
            window.location.href = '/'
        }, 500);
    }
})

$("#logout").on("click", function () {

    swal({
        title: "Cerrar Sesión",
        text: "¿Desea cerrar sesión?",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: "POST",
                    url: "/Listado/cerrar",
                    dataType: "json",
                    success: function (e) {
                        console.log(e)
                        localStorage.setItem("usuarios", 0)
                        eliminarCookies()
                        setTimeout(() => {
                            window.location.href = "/";
                        }, 500)
                        
                    }
                })
            } else {
            }
        });
        
    
})


function eliminarCookies() {
    document.cookie.split(";").forEach(function (c) {
        document.cookie = c.replace(/^ +/, "").replace(/=.*/, "=;expires=" + new Date().toUTCString() + ";path=/");
    });
}


/* Loaded */
function fn_loaded_mostrar() {
    $('#loaded').attr("style", "display: flex !important");
}

function fn_loaded_ocultar() {
    $('#loaded').attr("style", "display: none !important");
}