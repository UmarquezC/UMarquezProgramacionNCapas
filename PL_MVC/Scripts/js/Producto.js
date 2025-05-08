function CargarSubCategorias() {
    
    let ddl = $('#ddlCategoria').val()
    $.ajax({
        url: ddlSuCategoriaUrl,
        type: "GET",
        dataType: "JSON",
        data: {
            idCategoria: ddl
        },
        success: function (result) {
            if (result.Success) {
                let ddlSubCategoria = $('#ddlSubCategoria');
                ddlSubCategoria.empty();

                let optionDefault = "<option>Selecciona una subcategoría</option>";
                ddlSubCategoria.append(optionDefault);

                $.each(result.Objects, function (index, value) {
                    let option = `<option value='${value.IdSubCategoria}'>${value.Nombre}</option>`;
                    ddlSubCategoria.append(option);
                });
            }
        },
        error: function (xhr) {
            console.log("Error al cargar subcategorías", xhr);
        }
    })
}
