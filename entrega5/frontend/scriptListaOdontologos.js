$(document).ready(function () {
    // Redirige al usuario a la página de alta odontólogo
    $("#altaO").on("click", function () {
        window.location.href = 'AltaOdontologo.html';
    });

    // Redirige al usuario al inicio
    $("#inicio").on("click", function () {
        window.location.href = 'home/index.html';
    });

    // Traigo a todos los Odontologos
    fetch('https://localhost:7058/api/Gremio/GetAll', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(response => response.json())
        .then(data => {
            const originalData = data;

            // Agrego filas a la tabla
            function agregarFila(odontologo) {
                const newRow = document.createElement("tr");

                const nombreCell = document.createElement("td");
                nombreCell.textContent = odontologo.nombre;
                newRow.appendChild(nombreCell);

                const apellidoCell = document.createElement("td");
                apellidoCell.textContent = odontologo.apellido;
                newRow.appendChild(apellidoCell);

                const dniCell = document.createElement("td");
                dniCell.textContent = odontologo.dni;
                newRow.appendChild(dniCell);

                const agremiacionesCell = document.createElement("td");
                const agremiacionesLink = document.createElement("a");
                agremiacionesLink.href = "#";
                agremiacionesLink.classList.add("link-style");
                agremiacionesLink.textContent = "Ver Agremiaciones";
                agremiacionesCell.appendChild(agremiacionesLink);
                newRow.appendChild(agremiacionesCell);

                const consultoriosCell = document.createElement("td");
                const consultoriosLink = document.createElement("a");
                consultoriosLink.href = "#";
                consultoriosLink.classList.add("link-style");
                consultoriosLink.textContent = "Ver Consultorios";
                consultoriosCell.appendChild(consultoriosLink);
                newRow.appendChild(consultoriosCell);

                consultoriosLink.addEventListener("click", function () {
                    var nombreOdontologo=odontologo.nombre;
                    var apellidoOdontologo=odontologo.apellido;
                    var idOdontologo=odontologo.id;
                    window.location.href = 'ListaConsultorioOdontologo.html?idOdontologo=' + idOdontologo + '&apellidoOdontologo=' + apellidoOdontologo + '&nombreOdontologo=' + nombreOdontologo;

                })

                // Botón para eliminar
                const accionCell = document.createElement("td");
                accionCell.classList.add("text-center");

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
                const tablaBody = document.getElementById("tabla-bodyOdontologo");

                tablaBody.appendChild(newRow);

                // El evento click en el botón hace que se elimine el Odontologo correcto
                eliminarButton.addEventListener("click", function () {
                    deleteOdontologo(odontologo.id);
                });
            }

            originalData.forEach(agregarFila);

            $("#search input").on("input", function () {
                // Obtengo el valor del campo de búsqueda
                var searchTerm = $(this).val().toLowerCase();

                // Limpio el contenido actual de la tabla
                const tablaBody = document.getElementById("tabla-bodyOdontologo");
                tablaBody.innerHTML = "";

                // Se filtran y agregan las filas según lo ingresado
                const regex = new RegExp(searchTerm, 'i');

                originalData.forEach(function (odontologo) {
                    if (
                        regex.test(odontologo.nombre) ||
                        regex.test(odontologo.apellido) ||
                        regex.test(odontologo.dni)
                    ) {
                        agregarFila(odontologo);
                    }
                });
            });
        })
        .catch(error => console.error("Error al obtener los datos:", error));

        

        // Eliminar Odontologo
        function deleteOdontologo(id) {
            // DELETE
            fetch(`https://localhost:7058/api/Gremio/EliminarOdontologoPorId?n1=${id}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(response => {
                if (response.ok) {
                    console.log("Odontólogo eliminado correctamente.");
                    // Se actualiza la tabla después de la eliminación
                    location.reload();
                } else {
                    // Error
                    console.error("Error al eliminar el odontólogo.");
                }
            }).catch(error => console.error("Error en la solicitud DELETE:", error));
        }
});
