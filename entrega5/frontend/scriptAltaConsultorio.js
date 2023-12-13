function iniciarMap(){
    var coord = {lat:-34.9026196 ,lng: -57.928927};

    var map = new google.maps.Map(document.getElementById('map'),{
      zoom: 10,
      center: coord
    });

    var marker = new google.maps.Marker({
      position: coord,
      map: map,
    });
}

// Variable global que almacena la información sobre el origen
var origen;

// Función que obtiene el valor del parámetro "origen" de la URL
function obtenerOrigen() {
    var urlParams = new URLSearchParams(window.location.search);
    return urlParams.get('origen');
}

// Al cargar la página, asigna el valor del parámetro "origen" a la variable global
window.onload = function () {
    origen = obtenerOrigen();
}

// Se verifica el valor de "origen" y redirige dependiendo de donde se vino
document.getElementById('botonSalir').addEventListener('click', function() {
    if (origen === '2') {
        //Si viene del Alta Odontologo, te devuelve a Alta Odontologo
        alert("Volviendo a Alta Odontologo");
        window.location.href = 'AltaOdontologo.html';
    } else if (origen === '1') {
        //Si viene del index, te lleva al index
        alert("Volviendo al inicio");
        window.location.href = 'home/index.html';
    } else {
        // Si no hay información sobre el origen, redirige al index
        window.location.href = 'home/index.html';
    }
});

document.addEventListener("DOMContentLoaded", function () {
    const provinciaSelect = document.getElementById("provincia");
    const localidadSelect = document.getElementById("localidad");

    // Obtiene las provincias y llena el desplegable
    fetch('https://localhost:7058/api/Gremio/GetAllProvincia', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => response.json())
    .then(provincias => {
        provincias.forEach(provincia => {
            const optionElement = document.createElement("option");
            optionElement.value = provincia.id;
            optionElement.textContent = provincia.nombre;
            provinciaSelect.appendChild(optionElement);
        });

        // Al cargar las provincias, también se cargan las localidades de la primera provincia por defecto
        const primeraProvinciaId = provincias.length > 0 ? provincias[0].id : null;
        if (primeraProvinciaId) {
            cargarLocalidades(primeraProvinciaId);
        }

        // Al cambiar la provincia seleccionada en el desplegable, se cargan las localidades correspondientes
        provinciaSelect.addEventListener("change", function () {
            const selectedProvinciaId = provinciaSelect.value;
            cargarLocalidades(selectedProvinciaId);
        });
    })
    .catch(error => console.error("Error al obtener las provincias:", error));

    // Se inicializa el mapa con la ubicacion de la facultad
    iniciarMap();
});


function cargarLocalidades(provinciaId) {
    const localidadSelect = document.getElementById("localidad");

    // Limpia el desplegable de localidades
    localidadSelect.innerHTML = '<option value="">Seleccionar Localidad</option>';

    // Obtiene todas las localidades y luego filtra por provincia
    fetch(`https://localhost:7058/api/Gremio/GetAllLocalidad`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => {
        if (!response.ok) {
            throw new Error(`Error en la solicitud: ${response.status} - ${response.statusText}`);
        }
        return response.json();
    })
    .then(localidades => {
        // Filtra las localidades por provinciaId
        const localidadesFiltradas = localidades.filter(localidad => localidad.idProvincia == provinciaId);

        // Llena el desplegable con las localidades filtradas
        localidadesFiltradas.forEach(localidad => {
            const optionElement = document.createElement("option");
            optionElement.value = localidad.id;
            optionElement.textContent = localidad.nombre;
            localidadSelect.appendChild(optionElement);
        });
    })
    .catch(error => console.error("Error al obtener las localidades:", error));
}


function guardarDatos() {
    // Obtiene los valores de los campos del formulario
    const numero = document.getElementById("numero").value;
    const calle = document.getElementById("calle").value;
    const localidadId = document.getElementById("localidad").value;

    // Crea el objeto para enviar con los datos
    const consultorio = {
        id: 0,
        numero: parseInt(numero),
        calle: calle,
        idLocalidad: parseInt(localidadId)
    };

    // Se hace el POST
    fetch('https://localhost:7058/api/Gremio/AgregarConsultorio', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(consultorio)
    })
    .then(response => {
        if (!response.ok) {
            throw new Error(`Error en la solicitud: ${response.status} - ${response.statusText}`);
        }
        return response.json();
    })
    .then(data => {
        // Respuesta del servidor
        console.log('Consultorio creado exitosamente:', data);
        alert('Consultorio creado exitosamente');
    })
    .catch(error => {
        console.error('Error al crear el consultorio:', error);
        alert('Error al crear el consultorio');
    });
}