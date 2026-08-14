# Desflix

Plataforma web de catálogo de películas desarrollada con ASP.NET Core MVC, Entity Framework Core y SQL Server. El proyecto está preparado para ejecutarse de forma local, mediante contenedores Docker y desplegarse en Microsoft Azure.

---

## Tabla de contenidos

1. [Descripción](#descripción)
2. [Características principales](#características-principales)
3. [Tecnologías utilizadas](#tecnologías-utilizadas)
4. [Arquitectura](#arquitectura)
5. [Requisitos previos](#requisitos-previos)
6. [Configuración inicial](#configuración-inicial)
7. [Ejecución local](#ejecución-local)
8. [Ejecución con Docker](#ejecución-con-docker)
9. [Base de datos](#base-de-datos)
10. [Estructura del proyecto](#estructura-del-proyecto)
11. [Variables de entorno](#variables-de-entorno)
12. [Endpoints principales](#endpoints-principales)
13. [Despliegue en Azure](#despliegue-en-azure)
14. [Solución de problemas](#solución-de-problemas)
15. [Autor](#autor)

---

## Descripción

Desflix es una aplicación web que permite gestionar un catálogo de películas y géneros cinematográficos. Incluye un panel de administración con operaciones CRUD, un carrusel de películas destacadas en la página de inicio y un diseño responsivo inspirado en plataformas de streaming modernas.

El proyecto forma parte del desafío académico de contenerización y despliegue en la nube, demostrando la integración de .NET con Docker y Azure.

---

## Características principales

- CRUD completo de películas y géneros.
- Catálogo de películas con pósters, directores, año de lanzamiento y calificación.
- Carrusel principal (Hero Carousel) con películas destacadas.
- Interfaz responsiva para escritorio, tablet y dispositivos móviles.
- Capa de persistencia con Entity Framework Core y SQL Server.
- Patrón de repositorio y unidad de trabajo.
- Contenerización con Docker y Docker Compose.
- Preparado para despliegue en Azure App Service y Azure SQL Database.

---

## Tecnologías utilizadas

- **.NET 10** con ASP.NET Core MVC
- **Entity Framework Core 10**
- **SQL Server 2022**
- **Docker** y **Docker Compose**
- **Microsoft Azure** (App Service y SQL Database)
- **HTML5, CSS3 y JavaScript**
- **Bootstrap**

---

## Arquitectura

La aplicación sigue una arquitectura por capas dentro de un único proyecto web:

- **Controllers**: manejan las solicitudes HTTP y coordinan la respuesta.
- **Business/Services**: contienen la lógica de negocio.
- **Business/Factories**: generan entidades y DTOs.
- **Data**: contexto de base de datos, repositorios, unidad de trabajo y semillas de datos.
- **Core/Entities**: entidades del dominio.
- **Core/Interfaces**: contratos de repositorios, servicios y unidad de trabajo.
- **DTOs**: objetos de transferencia de datos para peticiones y respuestas.
- **Views**: vistas Razor con el motor de ASP.NET Core MVC.
- **wwwroot**: archivos estáticos (CSS, JavaScript, imágenes, bibliotecas del cliente).

---

## Requisitos previos

### Para desarrollo local

- SDK de .NET 10
- SQL Server (instancia local, contenedor o Azure SQL)
- Visual Studio 2022 o Visual Studio Code

### Para contenedores

- Docker Engine 24.x o superior
- Docker Compose

### Para Azure

- Cuenta activa de Microsoft Azure
- Azure CLI (opcional, pero recomendado)

---

## Configuración inicial

1. Clonar o descargar el repositorio.
2. Verificar que la cadena de conexión en `appsettings.json` apunte a una instancia de SQL Server accesible.
3. Aplicar las migraciones de Entity Framework Core para crear el esquema de base de datos.
4. Ejecutar el proyecto y acceder a la dirección indicada por el servidor.

### Cadena de conexión por defecto

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=sqlserver,1433;Database=Peliculas;User Id=sa;Password=YourStrongPassword123!;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;"
}
```

> En un entorno productivo, la contraseña y la cadena de conexión deben configurarse mediante variables de entorno o Azure Key Vault, nunca en archivos de configuración versionados.

---

## Ejecución local

### Desde Visual Studio

1. Abrir `Desflix.slnx`.
2. Asegurarse de que `Desflix` sea el proyecto de inicio.
3. Presionar `F5` o `Ctrl + F5` para ejecutar la aplicación.

### Desde la línea de comandos

```powershell
cd Desflix\Desflix
dotnet restore
dotnet build
dotnet run
```

La aplicación estará disponible en el puerto configurado en `launchSettings.json`, normalmente `https://localhost:7001` y `http://localhost:5001`.

---

## Ejecución con Docker

El repositorio incluye un `Dockerfile` y un `docker-compose.yaml` para levantar la aplicación junto con una instancia de SQL Server.

### Construir la imagen

```powershell
cd Desflix\Desflix
docker compose build
```

### Iniciar los contenedores

```powershell
docker compose up -d
```

Esto inicia dos contenedores:

- `desflix-sqlserver`: base de datos SQL Server 2022 Express.
- `desflix-app`: aplicación web expuesta en el puerto `5000`.

### Verificar el estado

```powershell
docker compose ps
docker compose logs -f app
```

### Detener los contenedores

```powershell
docker compose down
```

> Los datos de SQL Server se persisten en el volumen `sqlserver_data`, por lo que sobreviven a reinicios del contenedor.

### Acceso a la aplicación

Una vez iniciados los servicios, abrir el navegador en:

```
http://localhost:5000
```

---

## Base de datos

El esquema de base de datos se gestiona mediante migraciones de Entity Framework Core. Los archivos de migración se encuentran en la carpeta `Migrations`.

### Aplicar migraciones en desarrollo

```powershell
dotnet ef database update
```

### Aplicar migraciones en el contenedor

La aplicación puede configurarse para aplicar migraciones automáticamente al iniciar. En la versión actual, asegúrate de ejecutar `dotnet ef database update` antes de iniciar la aplicación si la base de datos está vacía.

### Datos iniciales

El archivo `Data/SeedData.cs` contiene géneros y películas de ejemplo que pueden cargarse para probar el carrusel y el catálogo.

---

## Estructura del proyecto

```
Desflix/
├── Business/
│   ├── Factories/          # Generación de entidades y DTOs
│   └── Services/           # Lógica de negocio
├── Controllers/            # Controladores MVC
├── Core/
│   ├── Entities/           # Entidades de dominio
│   └── Interfaces/         # Contratos
├── Data/
│   ├── Repositories/       # Implementaciones de repositorio
│   ├── UnitOfWork/         # Unidad de trabajo
│   ├── PeliculasDbContext.cs
│   └── SeedData.cs
├── DTOs/                   # Objetos de transferencia
├── Migrations/             # Migraciones de EF Core
├── Models/                 # ViewModels
├── Views/                  # Vistas Razor
├── wwwroot/                # Archivos estáticos
├── appsettings.json
├── Dockerfile
├── docker-compose.yaml
└── Desflix.csproj
```

---

## Variables de entorno

| Variable | Descripción | Ejemplo |
|----------|-------------|---------|
| `ASPNETCORE_ENVIRONMENT` | Entorno de ejecución | `Production` |
| `ASPNETCORE_URLS` | URLs de escucha del servidor | `http://+:80` |
| `ConnectionStrings__DefaultConnection` | Cadena de conexión a SQL Server | Ver `appsettings.json` |

Para Docker Compose, estas variables se inyectan directamente en el servicio `app`.

---

## Endpoints principales

| Ruta | Descripción |
|------|-------------|
| `/` | Página de inicio con Hero Carousel y catálogo destacado |
| `/Pelicula` | Listado de películas |
| `/Pelicula/Create` | Crear una nueva película |
| `/Pelicula/Edit/{id}` | Editar una película |
| `/Pelicula/Details/{id}` | Ver detalles de una película |
| `/Pelicula/Delete/{id}` | Eliminar una película |
| `/Home/Privacy` | Página de privacidad |

---

## Despliegue en Azure

El proyecto incluye evidencias de despliegue en Azure dentro de la carpeta `evidencias/`:

- `docker-build.png`: compilación exitosa de la imagen Docker.
- `docker-compose.png`: contenedores en ejecución con Docker Compose.
- `azure-sql-database.png`: base de datos SQL configurada en Azure.
- `azure-app-service.png`: aplicación desplegada en Azure App Service.

### Pasos generales para Azure

1. Crear un Azure Container Registry (ACR) y subir la imagen de la aplicación.
2. Crear una Azure SQL Database y configurar el firewall para permitir acceso desde Azure.
3. Crear una Azure App Service basada en la imagen del contenedor.
4. Configurar las cadenas de conexión y variables de entorno en App Service.
5. Validar el despliegue accediendo a la URL proporcionada por App Service.

---

## Solución de problemas

### Error: no se encuentra el archivo de configuración de Docker Compose

Asegúrate de ejecutar los comandos de Docker desde la carpeta `Desflix/Desflix`, donde se encuentran el `Dockerfile` y `docker-compose.yaml`.

### Error de conexión a SQL Server

- Verifica que el contenedor `desflix-sqlserver` esté en estado `Healthy`.
- Confirma que la contraseña de `SA` coincida en `docker-compose.yaml` y `appsettings.json`.
- Comprueba que el puerto `1433` no esté en uso por otra instancia de SQL Server local.

### No se muestran películas en el carrusel

- Verifica que existan registros en las tablas `Peliculas` y `Generos`.
- Ejecuta el método de semilla `SeedData` si la base de datos está vacía.

---

## Autor

Desarrollado como parte del desafío académico de contenerización y despliegue en la nube.
