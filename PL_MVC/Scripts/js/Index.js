$(document).ready(function () {
    cargarSubCategorias()
})


function cargarSubCategorias() {
    let ddl = $("#ddlCategoria").val()
    $.ajax({

        url: ddlSuCategoriaUrl,
        type: "GET",
        dataType: 'JSON',
        data: { idCategoria: ddl },
        success: function (result) {
            if (result.Success) {
                let ddlSubCategoria = $('#ddlSubCategoria')
                ddlSubCategoria.empty()

                let optionDefault = "<option> Selecciona una subcategoría</option>"
                ddlSubCategoria.append(optionDefault)

                $.each(result.Objects, function (index, value) {
                    let tagOption = `<option value='${value.IdSubCategoria}'>${value.Nombre}</option>`
                    ddlSubCategoria.append(tagOption)
                })
            }
        },
    })
}

function validarImagen() {
    let input = $('#inptFileImagen')[0].files[0].name.split('.').pop().toLowerCase()
    let extensiones = ['png', 'jpeg', 'jpg', 'webp']
    let banderaImg = false

    for (var i = 0; i <= input.length; i++) {
        if (input == extensiones[i]) {
            banderaImg = true
            break
        }
    }
    //No se le da una imagen
    if (!banderaImg) {
        alert(`Las extensiones permitidas son: ${extensiones}`)
        $('#inptFileImagen').val('')
    }
}

function visualizarImg(input) {
    if (input.files) {
        var reader = new FileReader()
        reader.onload = function (e) {
            $('#show').attr('src', e.target.result)
        }
        reader.readAsDataURL(input.files[0])
    }
}