# Guía Paso a Paso: Despliegue en Azure y Contenerización

Esta guía detalla el paso a paso estructurado para completar el Desafío 1 de Desarrollo de Software Empresarial, con un enfoque especial en la interacción con la interfaz de Azure y Visual Studio [cite: 1].

## Fase 1: Preparación del Entorno

### RF1: Cuenta Azure for Students [cite: 1]
1. Visita la página de **Azure for Students** en tu navegador [cite: 1].
2. Inicia sesión utilizando tu **cuenta institucional** [cite: 1].
3. Completa los pasos de verificación académica requeridos [cite: 1].
4. Ingresa al Portal de Azure y verifica que tu crédito de **$100 esté activo** [cite: 1].
> **📸 Evidencia requerida:** Captura del portal Azure mostrando el crédito activo [cite: 1].

### RF2: Instalación de Cargas de Trabajo [cite: 1]
1. Abre el **Visual Studio Installer** en tu equipo [cite: 1].
2. Haz clic en **"Modificar"** sobre tu instalación actual de Visual Studio [cite: 1].
3. Asegúrate de marcar e instalar las siguientes cargas de trabajo:
   * Desarrollo de ASP.NET y web [cite: 1]
   * Desarrollo de Azure [cite: 1]
4. Espera a que finalice la descarga e instalación [cite: 1].
> **📸 Evidencia requerida:** Captura del Visual Studio Installer mostrando ambas cargas de trabajo instaladas [cite: 1].

---

## Fase 2: Configuración de Base de Datos en Azure Portal

### RF4: Configuración de Azure SQL Database [cite: 1]
1. Dentro del **Azure Portal**, utiliza la barra de búsqueda superior y busca **"SQL Database"** [cite: 1].
2. Haz clic en el botón **"Crear"** [cite: 1].
3. En la pestaña de configuración básica, define lo siguiente:
   * **Grupo de recursos:** Crea uno nuevo llamado `MiAppRG` [cite: 1].
   * **Nombre de la base de datos:** `[nombre-app]-db-[tucarnet]` [cite: 1].
4. En la sección de **Servidor**, haz clic en "Crear nuevo" e ingresa:
   * **Nombre del servidor:** `[nombre-app]-server-[tucarnet]` [cite: 1].
   * **Usuario:** `admin_app` [cite: 1].
   * **Contraseña:** `App2024!` [cite: 1].
5. En la pestaña de **Redes**, configura el Firewall:
   * Habilita la opción **"Permitir servicios y recursos de Azure"** [cite: 1].
   * Selecciona **Agregar IP actual al firewall** para permitir que tu máquina local se conecte [cite: 1].
6. En la sección de proceso y almacenamiento, selecciona el plan **"Básico"** [cite: 1].
7. Haz clic en **Revisar y crear**, y luego en **Crear** [cite: 1].
> **📸 Evidencia requerida:** Captura del servidor SQL creado y captura de la configuración de firewall [cite: 1].

### RF5: Obtener la Cadena de Conexión [cite: 1]
1. Una vez creado el recurso, ve a tu nueva **Base de datos SQL** en el Azure Portal [cite: 1].
2. En el menú lateral izquierdo, busca y haz clic en **"Cadenas de conexión"** [cite: 1].
3. Copia la cadena correspondiente a **ADO.NET** [cite: 1].
4. (Importante: Recuerda reemplazar `{your_password}` en la cadena copiada con la contraseña real `App2024!` antes de usarla) [cite: 1].

---

## Fase 3: Publicación de la Aplicación

### RF5 (Continuación): Conexión de la App y Migraciones [cite: 1]
1. En Visual Studio, abre tu proyecto ASP.NET MVC funcional [cite: 1].
2. Pega la cadena de conexión copiada de Azure en tu archivo `appsettings.json` o `web.config` [cite: 1].
3. Abre el **Explorador de objetos SQL** en Visual Studio y conéctate al servidor de Azure usando tus credenciales (`admin_app` / `App2024!`) [cite: 1].
4. Ejecuta las migraciones (`Update-Database`) o los scripts SQL necesarios para crear las tablas en la nube [cite: 1].
5. Ejecuta la aplicación localmente y verifica que esté leyendo/guardando datos en Azure [cite: 1].
> **📸 Evidencia requerida:** Captura del Explorador de objetos SQL conectado a Azure y captura de la app mostrando datos desde Azure [cite: 1].

### RF3: Publicación de aplicación en Azure App Service [cite: 1]
1. En Visual Studio, haz **clic derecho sobre tu proyecto** en el Explorador de soluciones y selecciona **"Publicar"** [cite: 1].
2. En la ventana emergente, selecciona **Azure** como destino [cite: 1].
3. Elige **Azure App Service (Windows)** [cite: 1].
4. Configura el nuevo App Service con los siguientes datos:
   * **Nombre:** `[nombre-app]-[tucarnet]` (ej: vetsoft-12345) [cite: 1].
   * **Suscripción:** Azure for Students [cite: 1].
   * **Grupo de recursos:** Selecciona el que ya creaste, `MiAppRG` [cite: 1].
   * **Plan de hospedaje:** Deja el predeterminado [cite: 1].
5. Haz clic en **Publicar** y espera a que el proceso termine [cite: 1].
> **📸 Evidencia requerida:** Captura de la URL de la aplicación funcionando en el navegador y captura del Azure Portal mostrando el App Service creado [cite: 1].

---

## Fase 4: Contenerización (Docker y Docker Compose)

### RF6: Contenerización con Docker [cite: 1]
1. En la raíz de tu proyecto, crea un archivo llamado `Dockerfile` [cite: 1].
2. Configura el archivo base usando las imágenes `mcr.microsoft.com/dotnet/sdk:8.0-alpine` (para build) y `mcr.microsoft.com/dotnet/aspnet:8.0-alpine` (para ejecución) [cite: 1].
3. Abre la terminal, asegúrate de estar en el directorio del proyecto y ejecuta:
   `docker build -t [nombre-app] .` [cite: 1]
4. Una vez construida, ejecuta el contenedor:
   `docker run -d -p 8081:8080 --name [nombre-app]-container [nombre-app]` [cite: 1]
5. Verifica el funcionamiento accediendo a `http://localhost:8081` [cite: 1].
> **📸 Evidencia requerida:** Captura del comando `docker build` y captura de la aplicación corriendo en el contenedor [cite: 1].

### RF7: Orquestación con Docker Compose [cite: 1]
1. Crea un archivo `docker-compose.yml` en la raíz del proyecto [cite: 1].
2. Define dos servicios: tu aplicación (mapeando el puerto `8081:8080`) y la base de datos SQL (`mcr.microsoft.com/mssql/server:2022-latest`, puerto `1433:1433`) [cite: 1].
3. Configura las variables de entorno para que la aplicación apunte a la base de datos contenerizada [cite: 1].
4. Ejecuta el entorno completo con:
   `docker-compose up --build` [cite: 1]
5. Verifica el entorno ingresando a `http://localhost:8081` [cite: 1].
> **📸 Evidencia requerida:** Captura de Docker Desktop mostrando los contenedores corriendo y captura de la app funcionando [cite: 1].

---

## Fase 5: Entrega y Documentación

### Estructura de Carpetas [cite: 1]
Tu repositorio final debe verse así:
```text
[proyecto]-cloud/
  [proyecto]/
    ...
    appsettings.json
    Dockerfile
    docker-compose.yml
  README.md
  evidencias/
    azure-app-service.png
    azure-sql-database.png
    docker-build.png
    docker-compose.png
```

### Commits Requeridos [cite: 1]
Debes registrar tu progreso con los siguientes mensajes exactos:
* `git commit -m "feat(azure): configuración de App Service"` [cite: 1]
* `git commit -m "feat(azure): creación de Azure SQL Database"` [cite: 1]
* `git commit -m "feat(azure): conexión aplicación con Azure SQL"` [cite: 1]
* `git commit -m "feat(docker): creación de Dockerfile"` [cite: 1]
* `git commit -m "feat(docker): configuración de docker-compose"` [cite: 1]

### Documento PDF de Evidencia [cite: 1]
El informe a entregar (`INFORME_EXAMEN2.pdf`) debe contener:
1. Portada con tus datos y nombre del proyecto [cite: 1].
2. Desarrollo de cada requerimiento (RF1 a RF7) con explicaciones y sus respectivas capturas [cite: 1].
3. Pruebas de funcionamiento (URL pública y comandos Docker) [cite: 1].
4. Conclusión [cite: 1].

**Formato final de entrega:** Un archivo `ENTREGA_EXAMEN2_NOMBRE_APELLIDO.zip` [cite: 1].
