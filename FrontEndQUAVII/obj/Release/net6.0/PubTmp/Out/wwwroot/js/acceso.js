// Variable global
const loaded = document.querySelector("#loaded");

//$("#formIngreso").on("submit", function (e) {
//    e.preventDefault();
//    const usuario = $("#usuario").val();
//    const credenciales = $("#credencial").val();

//    $.ajax({
//        type: "POST",
//        url: "/Home/Ingresar",
//        dataType: "json",
//        data: {
//            Usuario: usuario,
//            password:credenciales
//        },
//        success: function (resp) {
//            console.log(resp.idUsuario)
//            fn_loaded_ocultar()
//            if (resp.idUsuario == 0) {
//                swal({
//                    title: "Error",
//                    text: "Usuario y/o Contraseña incorrectos",
//                    icon:"error"
//                })
//            } else {
//                fn_localstorage(resp.idUsuario)
//                setTimeout(() => {
//                    window.location.href = "/Listado";
//                }, 300)
//            }
//        },
//        error: function (req, status, error) {
//            console.log(req);
//            console.log(error);
//        }
//    })
//})


$("#btnIngresar").on("click", function (e) {
	e.preventDefault();
	const usuario = $("#usuario").val();
	const credenciales = $("#credencial").val();
	var response = grecaptcha.getResponse();

	if (usuario == '' || credenciales == '') {
		swal({
			title: "Error",
			text: "Todos los campos son obligatorios",
			icon: "error"
		})
	} else {
		//if (response.length != 0) {
			fn_loaded_mostrar()
			$.ajax({
				type: "POST",
				url: "/Home/Ingresar",
				dataType: "json",
				data: {
					Usuario: usuario,
					password: credenciales
				},
				success: function (resp) {
					fn_loaded_ocultar()
					if (resp.idUsuario == 0) {
						swal({
							title: "Error",
							text: "Usuario y/o Contraseña incorrectos",
							icon: "error"
						})
					} else {
						fn_localstorage(resp.idUsuario)
						setTimeout(() => {
							window.location.href = "/Listado";
						}, 300)
					}
				},
				error: function (req, status, error) {
					console.log(req);
					console.log(error);
				}
			})
		/*} else {
			swal({
				title: "Error",
				text: "Marcar el Captcha",
				icon: "error"
			})
		}*/
	}
})


function fn_localstorage(idusu) {
	if (!localStorage.getItem("usuarios")) {
		localStorage.setItem("usuarios", idusu);
	}
}



function fn_localstorage(idusu) {
    if (!localStorage.getItem("usuarios")) {
        localStorage.setItem("usuarios", idusu);
    } else {
        localStorage.setItem("usuarios", idusu);
    }
}

function fn_loaded_mostrar() {
    loaded.style.display="flex"
}

function fn_loaded_ocultar() {
    loaded.style.display = "none"
}