$(document).ready(function(){
    // Redirige a la ventana anterior
    $("#ListaOdontologos").on("click", function () {
        window.location.href = 'ListaOdontologos.html';
    });
}
)

var idOdontologo;
console.log('URL:', window.location.href);

document.addEventListener("DOMContentLoaded", async function () {
    // Obtengo el valor del parámetro de la URL
    function getParameterByName(name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    }

    idOdontologo = getParameterByName('idOdontologo');
    var apellidoOdontologo = getParameterByName('apellidoOdontologo');
    var nombreOdontologo=getParameterByName('nombreOdontologo')

    var odontologoInfoElement = document.getElementById('odontologoInfo');
    odontologoInfoElement.textContent = ` Odontólogo: ${nombreOdontologo} ${apellidoOdontologo}`;
    odontologoInfoElement.style.fontSize = '24px'; 
    odontologoInfoElement.style.marginLeft = '20px';


    //Busco los consultorios del odontologo
    try {
        const response = await fetch(`https://localhost:7058/api/Gremio/GetAllCOByO?n=${idOdontologo}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });
    
        if (!response.ok) {
            throw new Error('Error al obtener los consultorios del odontólogo');
        }
    
        const consultoriosOdontologo = await response.json();
    
        const tablaBody = document.getElementById("tabla-bodyConsultorio"); 
    //itero por cada consultorioODdontologo traido y busco el consultorio
        for (const consultorio of consultoriosOdontologo) {
            const idConsultorio = consultorio.idConsultorio;
    
            const response1 = await fetch(`https://localhost:7058/api/Gremio/GetConsultoriobyID?n1=${idConsultorio}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            });
    
            if (!response1.ok) {
                throw new Error(`Error al obtener el consultorio del odontólogo con ID ${idConsultorio}`);
            }
            //por cada consultorio busco sus datos y los muestro
    
            const consultorioDetalle = await response1.json();
    
            const newRow = document.createElement("tr");
    
            const calleCell = document.createElement("td");
            calleCell.textContent = consultorioDetalle.calle;
            newRow.appendChild(calleCell);
    
            const numeroCell = document.createElement("td");
            numeroCell.textContent = consultorioDetalle.numero;
            newRow.appendChild(numeroCell);
    
            const localidadCell = document.createElement("td");
            const localidad = await obtenerInformacionLocalidad(consultorioDetalle.idLocalidad)

            localidadCell.textContent = localidad.nombre;
            newRow.appendChild(localidadCell);

            const provinciaCell = document.createElement("td");
            const provincia = await obtenerInformacionProvincia(localidad.idProvincia)

            provinciaCell.textContent = provincia.nombre;
            newRow.appendChild(provinciaCell);
    
            const accionCell = document.createElement("td");
    
            const eliminarButton = document.createElement("button");
            eliminarButton.type = "button";
            eliminarButton.classList.add("btn", "btn-danger");
            eliminarButton.id = "borrar";
            
            const imagenBorrar = document.createElement("img");
            imagenBorrar.src = "trash-347.png";
            imagenBorrar.alt = "borrar";
            eliminarButton.appendChild(imagenBorrar);
    
            accionCell.appendChild(eliminarButton);
            newRow.appendChild(accionCell);
    
            tablaBody.appendChild(newRow);
        }
        
    } catch (error) {
        console.error('Error:', error);
        alert('Hubo un error al cargar los consultorios del odontólogo');
    }

    async function obtenerInformacionLocalidad(id) {
        // Se hace un GET para traer informacion de la localidad del consultorio
        try {
            const response = await fetch(`https://localhost:7058/api/Gremio/LocalidadPorID?n1=${id}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                throw new Error(`Error en la solicitud: ${response.statusText}`);
            }

            const localidad = await response.json();
            return localidad;
        } catch (error) {
            console.error('Error en la solicitud:', error);
            return null;
        }
    }
        async function obtenerInformacionProvincia(id) {
        try {
            //un Get para traer informacion de la Provincia de la localidad del consultorio
            const response = await fetch(`https://localhost:7058/api/Gremio/GetAllProvincia`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                throw new Error(`Error en la solicitud: ${response.statusText}`);
            }

            const provincias = await response.json();
            const provinciaEncontrada = provincias.find(provincia => provincia.id === id);

            return provinciaEncontrada;

        } catch (error) {
            console.error('Error en la solicitud:', error);
            return null;
        }
    }
});