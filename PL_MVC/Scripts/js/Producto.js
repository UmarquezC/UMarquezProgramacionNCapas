function CargarSubCategorias() {
    let categoriaId = $('#IdCategoria').val()
    if (categoriaId) {
        $.ajax({
            url: ddlSubCategoriaUrl + categoriaId,
            type: "GET",
            dataType: "JSON",
            success: function (result) {
                if (result.Success) {
                    let ddlSubCategoria = $('#IdSubCategoria');
                    ddlSubCategoria.empty(); 

                    let optionDefault = "<option value=''>Selecciona una subcategoría</option>";
                    ddlSubCategoria.append(optionDefault);

                    $.each(result.Objects, function (index, value) {
                        let option = `<option value='${value.IdSubCategoria}'>${value.Nombre}</option>`;
                        ddlSubCategoria.append(option);
                    });
                } else {
                    console.log("No se encontraron subcategorías.");
                }
            },
            error: function (xhr) {
                console.log("Error al cargar subcategorías", xhr);
            }
        });
    } else {
        $('#IdSubCategoria').empty().append("<option value=''>Selecciona una subcategoría</option>");
    }
}
