// MARIO ESTUARDO SANTOS

/******************************************************************************/
/*                              ENUMERADORES                                  */
/******************************************************************************/
//Enumerador para determinar el tipo de mensaje que se mostrará
var ErrorMessage_Enum = {
    Error: 1,
    Alert: 2,
    Success: 3,
};

/******************************************************************************/
/*                 FUNCION QUE MUESTRA UN MENSAJE DE ALERTA                   */
/******************************************************************************/
function MostrarMensaje(strMensaje, enumOpcion) {

    switch (enumOpcion) {
        case ErrorMessage_Enum.Error:
            swal("¡Error!", strMensaje, "error");
            break;
        case ErrorMessage_Enum.Alert:
            swal("¡Alerta!", strMensaje, "info");
            break;
        case ErrorMessage_Enum.Success:
            swal("¡Operación Exitosa!", strMensaje, "success");
            break;
    }
}