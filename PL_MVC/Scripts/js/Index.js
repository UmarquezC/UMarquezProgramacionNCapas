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