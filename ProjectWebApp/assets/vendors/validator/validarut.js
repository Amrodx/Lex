function validarRut(rut,campo) {
       if (rut != '') {
        var rutTemporal = rut;
        rutTemporal = replaceAll(rutTemporal, '.', '');
        rutTemporal = replaceAll(rutTemporal, '-', '');
        if (rutTemporal.length >= 6) {
            rut = rut.replace(' ', '');
            rut = formateaRut(rut);
            var tipoRetorno = validaRetorno(rut);
            $(campo).val(rut);
            if (tipoRetorno != 'C') {
                if (tipoRetorno != 'E') {
                    //                if (tipoRetorno == 'E') {
                    //                    alert('El valor ingresado para el campo RUT Cliente corresponde a una institución comercial, ingrese un RUT valido', campo);
                    //                }
                    //                else {
                    alert('Ingrese un RUT valido', campo);

                    //                }
                    $(campo).val('');
                    $(campo).focus();
                    //if ($("#Ejecutivo").val() == '') {
                    //    $("#EjecutivoRut").val('000');
                    //    $(".Agregar").parent().each(function () {
                    //        $(this).attr('href', $(this).attr('href').replace('ejecutivo=', ejeVacio));
                    //     //   $(this).attr('data-ajax-success', $(this).attr('data-ajax-success').replace($('#EjecutivoRut').val(), $('#Ejecutivo').val()));
                    //    });
                    //}
                }
            }
        }
        else {
            alert('El valor ingresado para el campo RUT debe poseer al menos 6 caracteres', campo);
            
            $(campo).val('');
            $(campo).focus();
            //if ($("#Ejecutivo").val() == '') {
            //    $("#EjecutivoRut").val('000');
            //    $(".Agregar").parent().each(function () {
            //        $(this).attr('href', $(this).attr('href').replace('ejecutivo=', ejeVacio));
            ////        $(this).attr('data-ajax-success', $(this).attr('data-ajax-success').replace($('#EjecutivoRut').val(), $('#Ejecutivo').val()));
            //    });
            //}
        }
    }
}

function formateaRut(rutIngresado) {
    var valor = rutIngresado;
    var rutConsultar;
    var rutDv;
    var valorRetorno;

    valor = replaceAll(valor, '.', '');
    valor = replaceAll(valor, '-', '');

    while (valor.charAt(0) == '0') {
        valor = valor.substring(1, valor.length);
    }

    if (parseInt(valor.substring(0, valor.length - 1)) < 10000000) {
        rutConsultar = valor.substring(0, valor.length - 1);
        rutDv = valor.charAt(valor.length - 1);
        rutDv == 'k' ? rutDv = 'K' : false;
        valorRetorno = separadorMiles(rutConsultar, 0, ',', '.') + '-' + rutDv;
    } else {
        rutConsultar = valor.substring(0, 8);
        rutDv = valor.charAt(valor.length - 1);
        rutDv == 'k' ? rutDv = 'K' : false;
        valorRetorno = separadorMiles(rutConsultar, 0, ',', '.') + '-' + rutDv;
    }

    return valorRetorno;
}

function separadorMiles(numero, decimales, separador_decimal, separador_miles) {
    numero = parseFloat(numero);
    if (isNaN(numero)) {
        return "";
    }

    if (decimales !== undefined) {
        // Redondeamos
        numero = numero.toFixed(decimales);
    }
    // Convertimos el punto en separador_decimal
    numero = numero.toString().replace(".", separador_decimal != undefined ? separador_decimal : ",");

    if (separador_miles) {
        // Añadimos los separadores de miles
        var miles = new RegExp("(-?[0-9]+)([0-9]{3})");
        while (miles.test(numero)) {
            numero = numero.replace(miles, "$1" + separador_miles + "$2");
        }
    }
    return numero;
}

function validaRetorno(rutIngresado) {
    var tipoRetorno;

    while (rutIngresado.toString().indexOf('.') != -1) {
        rutIngresado = rutIngresado.toString().replace('.', '');
    }

    while (rutIngresado.toString().indexOf('-') != -1) {
        rutIngresado = rutIngresado.toString().replace('-', '');
    }

    if (parseInt(rutIngresado.substring(0, rutIngresado.length - 1)) > 50000000) {
        tipoRetorno = 'E';
    }
    else {

        if (revisarDigito2(rutIngresado))
            tipoRetorno = 'C';
        else
            tipoRetorno = 'N';
    }

    return tipoRetorno;
}


function revisarDigito2(crut) {
    largo = crut.length;
    if (largo > 2)
        rut = crut.substring(0, largo - 1);
    else
        rut = crut.charAt(0);
    dv = crut.charAt(largo - 1);

    if (rut == null || dv == null)
        return 0

    var dvr = '0'
    suma = 0
    mul = 2

    for (i = rut.length - 1 ; i >= 0; i--) {
        suma = suma + rut.charAt(i) * mul
        if (mul == 7)
            mul = 2
        else
            mul++
    }
    res = suma % 11
    if (res == 1)
        dvr = 'k'
    else if (res == 0)
        dvr = '0'
    else {
        dvi = 11 - res
        dvr = dvi + ""
    }
    if (dvr != dv.toLowerCase()) {
        return false
    }

    return true
}


function replaceAll(texto, buscar, reemplazar) {
    while (texto.toString().indexOf(buscar) != -1) {
        texto = texto.toString().replace(buscar, reemplazar);
    }
    return texto;
}

function tecla(tipo, modo, noRepetir, campo) {
    var key_ingresada = window.event;
    var factor = 0;

    modo == 'MAY' ? alfabeticas(key_ingresada.keyCode) ? factor = 32 : false : false;

    if (valida_tecla(key_ingresada.keyCode, get_validaciones(tipo.toLowerCase()), factor) == false) {
        key_ingresada.keyCode = 0;
        return false;
    } else {
        var contenidoCampo = $(campo).val();
        var noRepetir = noRepetir.split('');
        var permitir = true;
        for (var i = 0; i < noRepetir.length; i++) {
            if (noRepetir[i].charCodeAt(0) == key_ingresada.keyCode) {
                var re = new RegExp("[" + noRepetir[i] + "]");
                if (re.test(contenidoCampo)) { key_ingresada.keyCode = 0; }
            }
        }
    }
}

function enter() {
    var tecla = window.event;
    if (tecla.keyCode == 13) {
        tecla.keyCode = 9;
    }
}

function alfabeticas(keyCode_char) {
    if ((keyCode_char > 96 && keyCode_char < 123) || keyCode_char == 225 || keyCode_char == 233 || keyCode_char == 237 || keyCode_char == 243 || keyCode_char == 250 || keyCode_char == 241) {
        return true;
    }
    return false;
}

function valida_tecla(tecla_sp, caracteres_sp, factor) {
    var caracteres_s1 = caracteres_sp;
    caracteres_sp = replace_especiales(caracteres_sp);
    for (var i = 0; i < caracteres_sp.length; i++) {
        var caracter = caracteres_sp.charCodeAt(i);
        if (caracter == 1121) {
            caracter = 209;
        }
        else if (tecla_sp == caracter) {
            return true;
        }
    }
    return (tecla_especial(tecla_sp, caracteres_s1));
}

function tecla_especial(tecla_s, caracteres_sp) {
    switch (tecla_s) {
        case 225:
            return (caracter_especial("/*a*/", caracteres_sp));
            break;
        case 233:
            return (caracter_especial("/*e*/", caracteres_sp));
            break;
        case 237:
            return (caracter_especial("/*i*/", caracteres_sp));
            break;
        case 243:
            return (caracter_especial("/*o*/", caracteres_sp));
            break;
        case 250:
            return (caracter_especial("/*u*/", caracteres_sp));
            break;
        case 241:
            return (caracter_especial("/*n*/", caracteres_sp));
            break;
        case 193:
            return (caracter_especial("/*A*/", caracteres_sp));
            break;
        case 201:
            return (caracter_especial("/*E*/", caracteres_sp));
            break;
        case 205:
            return (caracter_especial("/*I*/", caracteres_sp));
            break;
        case 211:
            return (caracter_especial("/*O*/", caracteres_sp));
            break;
        case 218:
            return (caracter_especial("/*U*/", caracteres_sp));
            break;
        case 209:
            return (caracter_especial("/*N*/", caracteres_sp));
            break;
        case 191:
            return (caracter_especial("/*?*/", caracteres_sp));
            break;
        case 161:
            return (caracter_especial("/*!*/", caracteres_sp));
            break;
        case 186:
            return (caracter_especial("/**o**/", caracteres_sp));
            break;
        case 39:
            return (caracter_especial("/com/", caracteres_sp));
            break;
        default:
            return false;
    }
}

function caracter_especial(texto_sp, caracteres) {
    if (caracteres.indexOf(texto_sp) != -1) {
        return true;
    }
    return false;
}

function get_validaciones(tipo_validacion) {
    return "0123456789K-.k";
    //    alert(tipo_validacion);
    //    var xml = sis_validaciones.value;
    //    if (xmlopen(xml)) {
    //        do {
    //            if (xmlsinglenodo("tipo") == tipo_validacion) {
    //                return (xmlsinglenodo("valores_permitidos"));
    //            }
    //        }
    //        while (xmlmovenext())
    //    }
}

function replace(texto_s, buscado, reemplazado) {
    texto_s += buscado;
    var arreglo_texto = texto_s.split(buscado);
    var valor_devuelto = "";
    for (var i = 0; i < arreglo_texto.length; i++) {

        var reeplazo = "";
        if (i < (arreglo_texto.length - 2)) {
            reeplazo = reemplazado;
        }
        valor_devuelto += arreglo_texto[i] + reeplazo;
    }
    return (valor_devuelto);
}

function replace_especiales(caracteres_s) {
    caracteres_s = replace(caracteres_s, "/*a*/", "");
    caracteres_s = replace(caracteres_s, "/*e*/", "");
    caracteres_s = replace(caracteres_s, "/*i*/", "");
    caracteres_s = replace(caracteres_s, "/*o*/", "");
    caracteres_s = replace(caracteres_s, "/*ua*/", "");
    caracteres_s = replace(caracteres_s, "/*n*/", "");
    caracteres_s = replace(caracteres_s, "/*A*/", "");
    caracteres_s = replace(caracteres_s, "/*E*/", "");
    caracteres_s = replace(caracteres_s, "/*I*/", "");
    caracteres_s = replace(caracteres_s, "/*O*/", "");
    caracteres_s = replace(caracteres_s, "/*U*/", "");
    caracteres_s = replace(caracteres_s, "/*N*/", "");
    caracteres_s = replace(caracteres_s, "/*?*/", "");
    caracteres_s = replace(caracteres_s, "/*!*/", "");
    caracteres_s = replace(caracteres_s, "/**o**/", "");
    caracteres_s = replace(caracteres_s, "/com/", "");
    return caracteres_s
}
