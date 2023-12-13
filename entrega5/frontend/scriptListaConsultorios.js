$(document).ready(function(){
    $("#altaC").on("click", function(){
        // Redirige al usuario a la página de alta consultorio
        alert('Yendo al alta de consultorio');
        window.location.href = 'AltaConsultorio.html?origen=1';
    });

    // Redirige al usuario al inicio
    $("#inicio").on("click", function () {
        window.location.href = 'home/index.html';
    });

    // Redirige al usuario a la lista de odontologos de un consultorio
    $("#listaOC").on("click", function () {
        window.location.href = 'ListaOdontologosDeConsultorio.html';
    });

})