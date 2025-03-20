
$("#datepicker").datepicker({
    dateFormat: "dd/mm/yy",
    showAnim: "slideDown"
});

$('Form').on('submit', function (event) {
    let inputs = {
        Nombre: $('#Nombre').val(),
        ApellidoPaterno: $('#ApellidoPaterno').val(),
        ApellidoMaterno: $('#ApellidoMaterno').val(),
        UserName: $('#UserName').val(),
        Email: $('#Email').val(),
        datepicker: $('#datepicker').val(),
        Sexo: $('#Sexo').val(),
        Telefono: $('#Telefono').val(),
        Celular: $('#Celular').val(),
        Curp: $('#Curp').val(),
        Password: $('#Password').val(),
        pass: $('#pass').val(),
        Direccion_Calle: $('#Direccion_Calle').val(),
        Direccion_NumeroExterior: $('#Direccion_NumeroExterior').val()
    }

    let formvalid = true

    for (let input in inputs) {
        if (inputs[input] === '') {
            alert('Falta completar ' + input)
            event.preventDefault()
            formvalid = false
            break
        }
    }
})


function clean(e) {
    let inputField = e.target
    if (inputField.value.trim() === '' || inputField.value) {
        inputField.style.borderColor = ''
        var ErrorMessage = inputField.parentNode.querySelector('.error')
        ErrorMessage.textContent = ''
    }
}

function soloLetras(e) {
    var entrada = String.fromCharCode(e.which)
    var inputField = e.target
    var ErrorMessage = inputField.parentNode.querySelector('.error')
    ErrorMessage.textContent = ''
    if (!(/[a-z A-Z]/.test(entrada))) {
        e.preventDefault()
        inputField.style.borderColor = 'red'
        ErrorMessage.textContent = 'Solo se aceptan letras'
    } else {
        inputField.style.borderColor = 'green'
    }
}

function soloNumero(e) {
    var entrada = String.fromCharCode(e.which)
    var inputField = e.target
    var ErrorMessage = inputField.parentNode.querySelector('.error')
    ErrorMessage.textContent = ''
    if (!(/[0-9]/.test(entrada))) {
        e.preventDefault()
        inputField.style.borderColor = 'red'
        ErrorMessage.textContent = 'Solo se aceptan numeros'
    } else {
        inputField.style.borderColor = 'green'
    }
}

function soloEmail(e) {
    var email = e.value
    var ErrorMessage = e.parentNode.querySelector('.error')
    ErrorMessage.textContent = ''

    let regex = /^[a-zA-Z0-9._%+-]+@@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/

    if (!regex.test(email)) {
        e.style.borderColor = 'red'
        ErrorMessage.textContent = 'Correo invalido'
    } else {
        e.style.borderColor = 'green'
    }

}

function validarContraseña(evt) {
    var inputField = evt.target;
    var ErrorMessage = inputField.parentNode.querySelector('.error');
    ErrorMessage.textContent = '';

    var password = inputField.value;
    var passwordRegex = /^(?=.*[A-Z])(?=.*[\W_]).{8}$/; // 1 mayúscula, 1 carácter especial, exactamente 8 caracteres

    if (password === '') {
        inputField.style.borderColor = ''; // Restablecer el color normal
    } else if (!passwordRegex.test(password)) {
        inputField.style.borderColor = 'red';
        ErrorMessage.textContent = 'La contraseña debe tener al menos 8 caracteres, incluyendo una letra mayuscula y un número';
    } else {
        inputField.style.borderColor = 'green';
    }
}

function comparar(e) {
    let pass1 = $('#Password').val()
    let pass2 = $('#pass').val()
    let inputField = e.target

    var ErrorMessage = e.parentNode.querySelector('.error')
    ErrorMessage.textContent = ''
    if (pass1 === pass2) {

        e.style.borderColor = 'green'
    } else {
        e.style.borderColor = 'red'
        ErrorMessage.textContent = 'Las contraseñas no coinciden'
    }
}


function validarCurp(input) {
    var curp = input.value.toUpperCase(),
        resultado = document.getElementById("resultado"),
        valido = "No válido";

    if (curpValida(curp)) {
        valido = "Válido";
        resultado.classList.add("ok");
    } else {
        resultado.classList.remove("ok");
    }

    resultado.innerText = "CURP: " + curp + "\nFormato: " + valido;
}

function curpValida(curp) {
    var re = /^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0\d|1[0-2])(?:[0-2]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$/,
        validado = curp.match(re);

    if (!validado)  //Coincide con el formato general?
        return false;

    //Validar que coincida el dígito verificador
    function digitoVerificador(curp17) {

        var diccionario = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ",
            lngSuma = 0.0,
            lngDigito = 0.0;
        for (var i = 0; i < 17; i++)
            lngSuma = lngSuma + diccionario.indexOf(curp17.charAt(i)) * (18 - i);
        lngDigito = 10 - lngSuma % 10;
        if (lngDigito == 10)
            return 0;
        return lngDigito;
    }
    if (validado[2] != digitoVerificador(validado[1]))
        return false;

    return true; //Validado
}

function MunicipioGetByEstado() {
    let ddl = $('#ddlEstado').val()

    $.ajax({
        url: ddlMunicipioUrl + ddl,
        type: "GET",
        dataType: 'JSON',
        success: function (result) {
            if (result.Success) {
                let ddlMunicipio = $('#ddlMunicipio')
                ddlMunicipio.empty()

                let ddlColonia = $('#ddlColonia')
                ddlColonia.empty();

                let optionDefault = "<option> Selecciona un municipio</option>"
                ddlMunicipio.append(optionDefault)

                let optionDefaultC = "<option> Selecciona una colonia</option>"
                ddlColonia.append(optionDefaultC)

                $.each(result.Objects, function (index, value) {
                    let tagOption = `<option value = '${value.IdMunicipio}'> ${value.Nombre_Municipio} </option> `
                    ddlMunicipio.append(tagOption)

                    let tagOptionC = `<option value = '${value.IdColonia}'> ${value.Nombre_Colonia} </option> `
                    ddlColonia.append(tagOptionC)
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