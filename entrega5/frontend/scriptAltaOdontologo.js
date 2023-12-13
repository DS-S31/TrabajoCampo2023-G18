function altaC() {
    var datosFormulario = {
        nombre: document.getElementById('nombre').value,
        apellido: document.getElementById('apellido').value,
        dni: document.getElementById('dni').value,
    };

    // Almacenamos los datos cargados por el usuario en localStorage, 
    //así al volver de otra pestaña, no los perdemos
    localStorage.setItem('datosAltaOdontologo', JSON.stringify(datosFormulario));

    alert('Yendo al alta consulorio');
    window.location.href = 'AltaConsultorio.html?origen=2';
}

//Si hay datos no capatilized, para mostrarlo los pasamos en formato capitalized
function capitalizarNombre(ciudad) {
    const palabras = ciudad.split(' ');

    // Capitaliza la primera letra de cada palabra
    const palabrasCapitalizadas = palabras.map(palabra => capitalize(palabra));

    return palabrasCapitalizadas.join(' ');
}

function capitalize(str) {
    return str.charAt(0).toUpperCase() + str.slice(1);
}

//variable para guardar los datos de los consultorios traidos 
const consultoriosData = [];

document.addEventListener("DOMContentLoaded", function () {

    //Consigo los datos que anteriormente se llenaron
    var datosGuardados = localStorage.getItem('datosAltaOdontologo');

    if (datosGuardados) {
        // Parseo los datos guardados
        var datosFormulario = JSON.parse(datosGuardados);

        // lleno el formulario con los datos recuperados
        document.getElementById('nombre').value = datosFormulario.nombre;
        document.getElementById('apellido').value = datosFormulario.apellido;
        document.getElementById('dni').value = datosFormulario.dni;

        // Elimina los datos guardados en localStorage
        localStorage.removeItem('datosAltaOdontologo');
    }

    
    const selectElement = document.getElementById("opcion");

        //traigo Consultorios para mostrar en las opciones del select
        fetch('https://localhost:7058/api/Gremio/GetAllConsultorio', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
            }
        })
        .then(response => response.json())
        .then(data => {
            
        data.forEach(async consultorio => {
            const optionElement = document.createElement("option");
            optionElement.value = consultorio.id;
            const id = consultorio.idLocalidad;

            const localidad = await obtenerInformacionLocalidad(id)
            const provincia= await obtenerInformacionProvincia(localidad.idProvincia)

            console.log("Información de la localidad:", localidad);
            console.log("Información de la provincia:", provincia);

            if (localidad && provincia ) {
                var capitalizedNombre= capitalizarNombre(localidad.nombre);
                var capitalizedNombre1= capitalizarNombre(provincia.nombre);

                optionElement.textContent = `  ${capitalizedNombre1}, ${capitalizedNombre}, Calle ${consultorio.calle}, Nro ${consultorio.numero}`;
                
                var ConsultorioData = {
                    "id": consultorio.id,
                    "calle": consultorio.calle,
                    "numero": consultorio.numero,
                    "localidad": capitalizedNombre,
                    "provincia":capitalizedNombre1
                };

                consultoriosData.push(ConsultorioData);
                console.log(consultoriosData);

            } else {
                optionElement.textContent = `  Sin información de Provincia ni localidad, calle ${consultorio.calle}, nro ${consultorio.numero}`;
            }

            selectElement.appendChild(optionElement);
        });
    })
    .catch(error => console.error("Error al obtener los datos:", error));


    $(document).ready(function () {
        $("#agregarConsultorio").on("click", function () {
            // los consultorios seleccionados con el boton + pasan a la grilla

            const selectedOption = selectElement.options[selectElement.selectedIndex];
            const idConsultorioSeleccionado = selectedOption.value;

            // Agrega el id del consultorio a la colección
            console.log("Consultorios seleccionados:", idConsultorioSeleccionado);
            
            mostrarConsultorioEnGrilla(idConsultorioSeleccionado);
        });
    });

    function mostrarConsultorioEnGrilla(idConsultorioSeleccionado) {
        // Encuentro el consultorio seleccionado en la lista de consultorios traidos
        const consultorioSeleccionado = consultoriosData.find(consultorio => consultorio.id == idConsultorioSeleccionado);

        // fila a la tabla con los datos del consultorio
        const bodyTableConsultorios = document.getElementById("bodyTable");
        const newRow = bodyTableConsultorios.insertRow();
        const cellId = newRow.insertCell(0);
        cellId.textContent = idConsultorioSeleccionado;
        cellId.style.display = "none"; // Oculto la celda id

        const cellCalle = newRow.insertCell(1);
        const cellNumero = newRow.insertCell(2);
        const cellLocalidad = newRow.insertCell(3);
        const cellProvincia = newRow.insertCell(4);
        const cellAccion = newRow.insertCell(5); 

        // Datos del consultorio
        cellCalle.textContent = consultorioSeleccionado.calle;
        cellNumero.textContent = consultorioSeleccionado.numero;
        cellLocalidad.textContent = consultorioSeleccionado.localidad;
        cellProvincia.textContent = consultorioSeleccionado.provincia;
        
        const btnBorrar = document.createElement("button");
        btnBorrar.type = "button";
        btnBorrar.classList.add("btn", "btn-danger");
        btnBorrar.innerHTML = '<img src="trash-347.png" alt="borrar">';
        btnBorrar.addEventListener("click", function () {
            newRow.remove();
        });

        cellAccion.appendChild(btnBorrar);
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


    $(document).ready(function () {
        $("#salir").on("click", function () {
            // Redirige al usuario a la página home
            alert("Volviendo al inicio");
            window.location.href = 'home/index.html';
        });
    //Doy Alta Odontologo
        $("#guardar").on("click", async  function () {
            try{
            
                    const idConsultoriosEnGrilla = obtenerIdConsultoriosEnGrilla();
                    console.log(idConsultoriosEnGrilla);
                    // Se obtienen los datos del formulario
                    var nombre = $("#nombre").val();
                    var dni = $("#dni").val();
                    var apellido = $("#apellido").val();


                    // Creo el DTO
                    var odontologoData = {
                        "id": 0,
                        "dni": dni,
                        "nombre": nombre,
                        "apellido": apellido
                    };

                    const responseOdontologo = await fetch('https://localhost:7058/api/Gremio/AgregarOdontologo', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(odontologoData)
                    });
                    if (!responseOdontologo.ok) {
                        throw new Error('Error al dar de alta el odontólogo');
                    }

                    const odontologoCreado = await responseOdontologo.json();

                    console.log(odontologoCreado);
                    for (const id of idConsultoriosEnGrilla) {
                        // Asocio los consultorios al odontólogo
                        var consultorioOdontologoData = {
                            "idConsultorio": id,
                            "idOdontologo": odontologoCreado.id
                        };

                        const responseConsultorioOdontologo = await fetch('https://localhost:7058/api/Gremio/CargarCO', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(consultorioOdontologoData)
                        });

                        if (!responseConsultorioOdontologo.ok) {
                            throw new Error('Error al dar de alta el consultorio del odontologo');
                        }
                    }
                    alert('Proceso completado con éxito');
            }
            catch (error) {
                console.error('Error:', error);
                alert('Hubo un error en el proceso. Consulta la consola para más detalles.');
            }
    });

});

function obtenerIdConsultoriosEnGrilla() {
    //guardo el idConsultorio en una lista para despues de crear el odontologo con sus consultorios
    const idConsultoriosEnGrilla = [];

    $('#grillaConsultorios tbody tr').each(function () {
        const idConsultorio = $(this).find('td:first-child').text();
        idConsultoriosEnGrilla.push(idConsultorio);
    });

    return idConsultoriosEnGrilla;
}