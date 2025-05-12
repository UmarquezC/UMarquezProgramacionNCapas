$(document).ready(function () {
    GetAllUsuarios()

});

function Form() {
    CleanModal()
    GetAllRol()
    GetAllEstado()
    MunicipioGetByEstado()
    ColoniaGetAllByMunicipio()
    ShowModal()
}

function ShowModal() {
    $('#staticBackdrop').modal("show")
}
function CleanModal() {
    $('#inptNombre').val('')
    $('#Nombre').val("");
    $('#UserName').val("");
    $('#ApellidoPaterno').val("");
    $('#ApellidoMaterno').val("");
    $('#Email').val("");
    $('#Password').val("");
    $('#datepicker').val("").toString();
    $('input[name="Sexo"]:checked').val("");
    $('#Telefono').val("");
    $('#Celular').val("");
    $('#CURP').val("");
    $('#ddlRol').val("");
    $('#Calle').val("");
    $('#inptNumeroExterior').val("");
    $('#inptNumeroInterior').val("");
    $('#ddlColonia').val("");
    $('#ddlMunicipio').val("");
    $('#ddlEstado').val("");
}



//PARA PINTAR TODA LA TABLA
function GetAllUsuarios() {
    $.ajax({
        url: pathGetAll,
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            /*console.log(result)*/
            if (result.Success) {
                let tabla = $('#tbody')
                tabla.empty()

                let editar = `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pen-fill" viewBox="0 0 16 16">
                    <path d = "m13.498.795.149-.149a1.207 1.207 0 1 1 1.707 1.708l-.149.148a1.5 1.5 0 0 1-.059 2.059L4.854 14.854a.5.5 0 0 1-.233.131l-4 1a.5.5 0 0 1-.606-.606l1-4a.5.5 0 0 1 .131-.232l9.642-9.642a.5.5 0 0 0-.642.056L6.854 4.854a.5.5 0 1 1-.708-.708L9.44.854A1.5 1.5 0 0 1 11.5.796a1.5 1.5 0 0 1 1.998-.001"/> 
                  </svg > `

                let borrar = `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash3-fill" viewBox="0 0 16 16">
                    <path d="M11 1.5v1h3.5a.5.5 0 0 1 0 1h-.538l-.853 10.66A2 2 0 0 1 11.115 16h-6.23a2 2 0 0 1-1.994-1.84L2.038 3.5H1.5a.5.5 0 0 1 0-1H5v-1A1.5 1.5 0 0 1 6.5 0h3A1.5 1.5 0 0 1 11 1.5m-5 0v1h4v-1a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 0-.5.5M4.5 5.029l.5 8.5a.5.5 0 1 0 .998-.06l-.5-8.5a.5.5 0 1 0-.998.06m6.53-.528a.5.5 0 0 0-.528.47l-.5 8.5a.5.5 0 0 0 .998.058l.5-8.5a.5.5 0 0 0-.47-.528M8 4.5a.5.5 0 0 0-.5.5v8.5a.5.5 0 0 0 1 0V5a.5.5 0 0 0-.5-.5" />
                </svg>`

                
                $.each(result.Objects, function (index, value) {
                    let imagen = value.ImagenBase64 ? `data:image/*;base64,${value.ImagenBase64}` : `https://thumbs.dreamstime.com/b/default-avatar-profile-icon-social-media-user-vector-image-icon-default-avatar-profile-icon-social-media-user-vector-image-209162840.jpg`

                    let etiqueta = `
                        <tr>
                            <td><img style="height:55px; width:55px; border-radius: 50%; object-fit:cover;" id="show" src="${imagen}" alt="Alternate Text" />
                            </td>
                            <td>${value.Nombre} ${value.ApellidoPaterno} ${value.ApellidoMaterno}</td>
                            <td>${value.UserName}</td>
                            <td>${value.Email}</td>
                            <td>${value.Password}</td>
                            <td>${value.FechaNacimiento}</td>
                            <td>${value.Sexo}</td>
                            <td>${value.Telefono}</td>
                            <td>${value.Celular}</td>
                            <td>${value.Curp}</td>
                            
                            <td>${value.Rol.Nombre}</td>
                            
                            <td>${value.Direccion.Calle}, ${value.Direccion.NumeroInterior}, ${value.Direccion.NumeroExterior},${value.Direccion.Colonia.Nombre}, ${value.Direccion.Colonia.CodigoPostal}, ${value.Direccion.Colonia.Municipio.Nombre}, ${value.Direccion.Colonia.Municipio.Estado.Nombre}  </td>

                            <td>${value.Estatus}</td>
                            <td><button type="button" class="btn btn-warning" onclick="UsuarioGetById(${value.IdUsuario})"> ${editar} </button> </td>
                            <td><button type="button" class="btn btn-danger" onclick="deleteUsuario(${value.IdUsuario})">${borrar}</button> </td>

                        </tr>
                    `
                    tabla.append(etiqueta)

                })
            } else {
                console.log("Error" + result.Message)
            }
        },
        error: function (result) {
            console.log(result.Message)
        }
    })
}

function GetAllRol() {
    $.ajax({
        url: dllRol,
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            console.log(result)
            if (result.Success) {               
                let ddlRol = $('#ddlRol')
                ddlRol.empty();

                let optionDefault = "<option> Selecciona un Rol</option>"
                ddlRol.append(optionDefault)

                $.each(result.Objects, function (index, value) {
                    let tagOption = `<option value = '${value.IdRol}'> ${value.Nombre} </option> `
                    ddlRol.append(tagOption)
                })
            }
        },
        error: function () {
            alert("Error al obtener los roles.");
        }
    })
}

function GetAllEstado() {
    $.ajax({
        url: ddlEstado,
        type: 'GET',
        dataType: 'JSON',
        success: function (data) {
            let ddlEstado = $("#ddlEstado")
            ddlEstado.empty();
            ddlEstado.append('<option value="">Seleccione un Estado</option>');

            $.each(data.Objects, function (index, estado) {
                let option = `<option value="${estado.IdEstado}">${estado.Nombre}</option>`;
                ddlEstado.append(option)
            });

        },
        error: function () {
            alert("Error al obtener los Estados.");
        }
    })
}

//let GetAllEstado = () => {

//    $.ajax({
//        url: ddlEstado,
//        type: "GET",
//        dataType: "JSON",
//        success: result => {
//            if (result.Correct) {
//                //Buscar el ddl donde pinta el valor
//                let ddlEstado = $('#ddlEstado');
//                ddlEstado.empty();

//                let defaultSelect = `<option value="">Seleccione un Estado</option>`
//                ddlMunicipio.append(defaultSelect)

//                $.each(result.Objects, (i, valor) => {
//                    let option = `<option value="${valor.IdEstado}">${valor.Nombre}</option>`;
//                    ddlEstado.append(option)

//                })

//            }
//        },
//        error: xhr => {
//            console.log(xhr)
//        }
//    })
//}

function MunicipioGetByEstado() {
    let ddl = $('#ddlEstado').val()

    $.ajax({
        url: ddlMunicipioUrl + ddl,
        type: "GET",
        dataType: 'JSON',
        success: function (result) {
            if (result.Success) {
                console.log(result)
                let ddlMunicipio = $('#ddlMunicipio');
                ddlMunicipio.empty();

                let defaultSelect = `<option value="">Seleccione un Municipio</option>`
                ddlMunicipio.append(defaultSelect)

                $.each(result.Objects, (i, valor) => {
                    let option = `<option value="${valor.IdMunicipio}">${valor.Nombre_Municipio}</option>`;
                    ddlMunicipio.append(option)

                })
            }
        },
        error: function (xhr) {
            console.log(xhr)
        },
    })
}

function ColoniaGetAllByMunicipio() {
    let ddl = $('#ddlMunicipio').val();

    $.ajax({
        url: ddlColoniaUrl + ddl,
        type: 'GET',
        dataType: 'JSON',
        success: function (result) {
            if (result.Success) {
                let ddlColonia = $('#ddlColonia')
                ddlColonia.empty();

                let optionDefault = "<option> Selecciona una colonia</option>"
                ddlColonia.append(optionDefault)

                $.each(result.Objects, function (index, value) {
                    let tagOption = `<option value = '${value.IdColonia}'> ${value.Nombre_Colonia} </option> `
                    ddlColonia.append(tagOption)
                })

            }
        },
        error: function (xhr) {
            console.log(xhr)
        }
    })
}

//let MunicipioGetByIdEstado = (e) => {
//    //console.log(e)
//    let ddl = ddlMunicipioUrl + e
//    console.log(ddl)
//    $.ajax({
//        url: ddl,
//        type: "GET",
//        dataType: "JSON",
//        success: result => {
//            if (result.Correct) {
//                //Buscar el ddl donde pinta el valor
//                let ddlMunicipio = $('#ddlMunicipio');
//                ddlMunicipio.empty();

//                let defaultSelect = `<option value="">Seleccione un Municipio</option>`
//                ddlMunicipio.append(defaultSelect)

//                $.each(result.Objects, (i, valor) => {
//                    let option = `<option value="${valor.IdMunicipio}">${valor.Nombre}</option>`;
//                    ddlMunicipio.append(option)

//                })

//            }
//        },
//        error: xhr => {
//            console.log(xhr)
//        }
//    })
//}




//let ColoniaGetAllByMunicipio = () => {
//    let ddl = ddlColoniaUrl + $('#ddlMunicipio').val()

//    console.log(ddl)
//    $.ajax({
//        url: ddl,
//        type: "GET",
//        dataType: "JSON",
//        success: result => {
//            if (result.Correct) {
//                //Buscar el ddl donde pinta el valor
//                let ddlColonia = $('#ddlColonia');
//                ddlColonia.empty();

//                let defaultSelect = `<option value="">Seleccione una Colonia</option>`
//                ddlColonia.append(defaultSelect)

//                $.each(result.Objects, (i, valor) => {
//                    let option = `<option value="${valor.IdColonia}">${valor.Nombre}</option>`;
//                    ddlColonia.append(option)
//                })

//            }
//        },
//        error: xhr => {
//            console.log(xhr)
//        }
//    })
//}

/**AQUI HAY QUE CHECAR TODO ESTO*/

/*
function AgregarUsuario() {
    var formData = new FormData();

    var idUsuario = $("#IdUsuario").val();
    if (idUsuario) {
        formData.append("IdUsuario", idUsuario);
    }
    formData.append("UserName", $("#inptUserName").val());
    formData.append("Nombre", $("#inptNombre").val());
    formData.append("ApellidoPaterno", $("#inptApellidoPaterno").val());
    formData.append("ApellidoMaterno", $("#inpApellidoMaterno").val());
    formData.append("Email", $("#inptEmail").val());
    formData.append("Password", $("#inptPassword").val());
    formData.append("FechaNacimiento", $("#datepicker").val());
    formData.append("Sexo", $("input[name='Sexo']:checked").val());
    formData.append("Curp", $("#inptCurp").val());
    formData.append("Telefono", $("#inptTelefono").val());
    formData.append("Celular", $("#inptCelular").val());
    formData.append("Rol.IdRol", $("#ddlRol").val() || 0);
    formData.append("Direccion.Calle", $("#inptCalle").val());
    formData.append("Direccion.NumeroExterior", $("#inptNumeroExterior").val());
    formData.append("Direccion.NumeroInterior", $("#inptNumeroExterior").val());
    formData.append("Direccion.Colonia.IdColonia", $("#ddlColonia").val() || 0);
    formData.append("Direccion.Colonia.Municipio.IdMunicipio", $("#ddlMunicipio").val() || 0);
    formData.append("Direccion.Colonia.Municipio.Estado.IdEstado", $("#ddlEstado").val() || 0);


    var fileInput = $("#inptFileImagen")[0];
    if (fileInput.files.length > 0) {
        formData.append("ImagenUsuario", fileInput.files[0]);
    }

    $.ajax({
        url: AddURL,
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            if (response.Success) {
                alert("Usuario guardado correctamente.");
                location.reload(); // Recarga la página
            } else {
                alert("Error al guardar el usuario: " + response.Message);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error en la solicitud:", error);
            alert("Hubo un problema al guardar el usuario.");
        }
    });
}
*/



function BtnGuardar() {
    var inpIdUsuario = $('#IdUsuario').val();
    var inptNombre = $('#inptNombre').val();
    var inptUserName = $('#inptUserName').val();
    var inptApellidoPaterno = $('#inptApellidoPaterno').val();
    var inptApellidoMaterno = $('#inpApellidoMaterno').val();
    var inptEmail = $('#inptEmail').val();
    var inptPassword = $('#inptPassword').val();
    var inptFecha = $('#datepicker').val().toString();
    var sexoSeleccionado = $('input[name="Sexo"]:checked').val();
    var inptTelefono = $('#inptTelefono').val();
    var inptCelular = $('#inptCelular').val();
    var inptCURP = $('#inptCurp').val();
    var inptRol = $('#ddlRol').val();
    var inptCalle = $('#inptCalle').val();
    var inptNumeroExterior = $('#inptNumeroExterior').val();
    var inptNumeroInterior = $('#inptNumeroInterior').val();
    var inptColonia = $('#ddlColonia').val();
    var inptMunicipio = $('#ddlMunicipio').val();
    var inptEstado = $('#ddlEstado').val();


    if (inpIdUsuario == "") {
        inpIdUsuario = 0;
        var json = {
            UserName: inptUserName,
            Nombre: inptNombre,
            ApellidoPaterno: inptApellidoPaterno,
            ApellidoMaterno: inptApellidoMaterno,
            Email: inptEmail,
            Password: inptPassword,
            FechaNacimiento: inptFecha,
            Sexo: sexoSeleccionado,
            Telefono: inptTelefono,
            Celular: inptCelular,
            CURP: inptCURP,
            Rol: {
                IdRol: inptRol,
            },
            Direccion: {
                Calle: inptCalle,
                NumeroExterior: inptNumeroExterior,
                NumeroInterior: inptNumeroInterior,
                Colonia: {
                    IdColonia: inptColonia,
                    Municipio: {
                        IdMunicipio: inptMunicipio,
                        Estado: {
                            IdEstado: inptEstado
                        }
                    }
                }
            }

        };
        /* console.log(json)*/
        Add(json);
    }
    else {
        var json = {
            IdUsuario: inpIdUsuario,
            UserName: inptUserName,
            Nombre: inptNombre,
            ApellidoPaterno: inptApellidoPaterno,
            ApellidoMaterno: inptApellidoMaterno,
            Email: inptEmail,
            Password: inptPassword,
            FechaNacimiento: inptFecha,
            Sexo: sexoSeleccionado,
            Telefono: inptTelefono,
            Celular: inptCelular,
            CURP: inptCURP,
            Rol: {
                IdRol: inptRol,
            },
            Direccion: {
                Calle: inptCalle,
                NumeroExterior: inptNumeroExterior,
                NumeroInterior: inptNumeroInterior,
                Colonia: {
                    IdColonia: inptColonia,
                    Municipio: {
                        IdMunicipio: inptMunicIpio,
                        Estado: {
                            IdEstado: inptEstado
                        }
                    }
                }
            }

        };
        Update(json);
    }

}


function Add(json) {
    console.log(json)
    $.ajax({
        url: AddURL,
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json",
        dataType: "json",
        success: function (result) {
            if (result.Success) {
                console.log("Entro al ajax add");
                CleanModal();
                GetAllUsuarios();

            } else {
                console.log("Hubo un error al insertar");
            }
        },
        error: function (xhr, status, error) {
            console.log("Status: " + status);
            console.log("Error: " + error);

        }
    });
}
function Update(json) {
    console.log(json)
    $.ajax({
        url: UpdateURL,
        type: "POST",
        data: JSON.stringify(json),
        contentType: "application/json",
        dataType: "json",
        success: function (result) {
            if (result.Success) {
                console.log("Actualizo de manera correcta");

                GetAllTabla();
                CloseModal();
            } else {
                console.log("Hubo un error al actualizar");
            }
        },
        error: function (xhr, status, error) {
            console.log("Status: " + status);
            console.log("Error: " + error);

        }
    });
}




function UsuarioGetById(IdUsuario) {
    $.ajax({
        url: UsuarioGetByIdURL + IdUsuario,
        type: "GET",
        dataType: "json",
        success: function (result) {
            if (result.Success) {
                console.log("Entro al ajax get by id");
                var usuario = result.Object;
                console.log(usuario)
                CleanModal();
                GetAllRol();
                GetAllEstado()
                MunicipioGetByEstado()
                ColoniaGetAllByMunicipio()
    
                $('#IdUsuario').val(usuario.IdUsuario);
                $('#inptNombre').val(usuario.Nombre);
                $('#inptApellidoPaterno').val(usuario.ApellidoPaterno);
                $('#inpApellidoMaterno').val(usuario.ApellidoMaterno);
                $('#inptUserName').val(usuario.UserName);
                $('#inptEmail').val(usuario.Email);
                $('#datepicker').val(usuario.FechaNacimiento).toString();
                $('input[name="Sexo"]:checked').val(usuario.Sexo);
                $('#inptTelefono').val(usuario.Telefono);
                $('#inptCelular').val(usuario.Celular);
                $('#inptCurp').val(usuario.Curp);
                $('#inptPassword').val(usuario.Password);
                $("#ddlRol :selected").text();
                $('#ddlRol').val(usuario.Rol.IdRol);
                $('#inptCalle').val(usuario.Direccion.Calle);
                $('#inptNumeroInterior').val(usuario.Direccion.NumeroInteriror);
                $('#inptNumeroExterior').val(usuario.Direccion.NumeroExterior);
                $('#ddlEstado').val(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                $('#ddlMunicipio').val(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                $('#ddlColonia').val(usuario.Direccion.Colonia.IdColonia);

                ShowModal();
            } else {
                alert("No hay datos de usuario");
            }
        },
        error: function (xhr, status, error) {
            console.log("Status: " + status);
            console.log("Error: " + error);

        }
    });
}


function deleteUsuario(idUsuario) {

    if (confirm("¿Estás seguro de que deseas eliminar este usuario?")) {
        $.ajax({
            url: eliminarUrl,
            type: 'POST',
            data: { IdUsuario: idUsuario },
            success: function (response) {
                if (response.Success) {
                    alert("Usuario eliminado correctamente.");

                    obtenerUsuarios();
                } else {
                    alert("Error al eliminar el usuario: " + response.Message);
                }
            },
            error: function (xhr, status, error) {
                console.error("Error en la solicitud de eliminación: ", error);
                alert("Hubo un problema al eliminar el usuario.");
            }
        });
    }
}