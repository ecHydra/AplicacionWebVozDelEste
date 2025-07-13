function capitalizar(str) {
    return str.charAt(0).toUpperCase() + str.slice(1).toLowerCase();
}

async function obtenerClimaActual() {
    const contenedor = document.getElementById("climaActual");
    try {
        const response = await fetch('/Home/ObtenerClima');
        if (!response.ok) throw new Error();
        const data = await response.json();

        if (data.error) {
            contenedor.innerText = data.error;
        } else {
            contenedor.innerHTML = `
                <img src="https://openweathermap.org/img/wn/${data.icono}@2x.png" alt="${data.descripcion}" />
                <div>
                    <p><strong>${data.temp}°C</strong></p>
                    <p>${capitalizar(data.descripcion)}</p>
                </div>
            `;
        }
    } catch (e) {
        contenedor.innerText = "No se pudo obtener el clima actual.";
    }
}

async function obtenerPronostico() {
    const contenedor = document.getElementById("pronosticoDias");
    try {
        const response = await fetch('/Home/ObtenerPronostico');
        if (!response.ok) throw new Error();
        const data = await response.json();

        if (data.error) {
            contenedor.innerText = data.error;
        } else {
            contenedor.innerHTML = data.pronostico.map(dia => `
                <div class="tarjeta-dia">
                    <h5>${dia.fecha}</h5>
                    <img src="https://openweathermap.org/img/wn/${dia.icono}@2x.png" alt="${dia.descripcion}" />
                    <p>${dia.temp_min}°C / ${dia.temp_max}°C</p>
                    <p>${capitalizar(dia.descripcion)}</p>
                </div>
            `).join('');
        }
    } catch (e) {
        contenedor.innerText = "No se pudo obtener el pronóstico.";
    }
}

document.addEventListener("DOMContentLoaded", () => {
    obtenerClimaActual();
    obtenerPronostico();
});