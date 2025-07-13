function capitalizar(str) {
    return str.charAt(0).toUpperCase() + str.slice(1).toLowerCase();
}

async function obtenerClimaHeader() {
    const contenedor = document.getElementById("clima");
    if (!contenedor) return; // no hay contenedor en esta vista

    try {
        const response = await fetch('/Home/ObtenerClima');
        if (!response.ok) throw new Error();
        const data = await response.json();

        if (data.error) {
            contenedor.innerText = data.error;
        } else {
            contenedor.innerHTML = `
                <img src="https://openweathermap.org/img/wn/${data.icono}.png" alt="${data.descripcion}" style="vertical-align:middle; width: 24px; height: 24px;" />
                <span><strong>${data.temp}°C</strong>, ${capitalizar(data.descripcion)}</span>
            `;
        }
    } catch (e) {
        contenedor.innerText = "Error al cargar clima.";
    }
}

document.addEventListener("DOMContentLoaded", obtenerClimaHeader);
