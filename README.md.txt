# 📻 Voz del Este – Plataforma Web para Emisora Radial

Aplicación web desarrollada como proyecto final, orientada a la gestión integral de contenidos de una emisora radial. Construida con ASP.NET MVC, SQL Server y Entity Framework en enfoque Database-First.

## 🛠️ Tecnologías utilizadas

- ASP.NET MVC (.NET Framework 4.7.2)
- Entity Framework (Database-First)
- SQL Server
- HTML, CSS y JavaScript
- APIs externas: OpenWeatherMap (clima), CurrencyLayer (cotizaciones)

## 🎯 Funcionalidades principales

- 🔐 Autenticación de usuarios y control de acceso por roles (Admin, Editor, Usuario)
- 📰 CRUD completo de noticias y comentarios
- 📻 Gestión de programas radiales y conductores asociados (múltiples)
- 💬 Módulo de comentarios por parte de oyentes
- 🌦️ Sección de clima con animaciones visuales (soleado, lluvia, tormenta, etc.)
- 💱 Cotizaciones en tiempo real con animaciones
- 💼 Administración de patrocinadores y planes de anuncios
- 👥 Gestión de clientes y usuarios del sistema
- 🧾 Diseño moderno, responsivo y adaptable

## 🧩 Estructura del proyecto

- `Controllers/`: Lógica de negocio
- `Views/`: Interfaz de usuario (Razor)
- `Models/`: Entidades generadas con EF desde SQL Server
- `Content/` y `Scripts/`: Archivos estáticos personalizados

## 🧪 Cómo ejecutar el proyecto

1. Clonar el repositorio:
   ```bash
   git clone https://github.com/ecHydra/AplicacionWebVozDelEste.git
