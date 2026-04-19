# Sistema Web de Gestión de Empleados (MVC)

Aplicación web desarrollada con ASP.NET Core MVC que consume una API REST para gestionar empleados, incluyendo imágenes, departamentos, puestos y estados.

##  Tecnologías utilizadas

* ASP.NET Core MVC
* Razor Views
* HttpClient
* Bootstrap
* API REST (backend separado)

##  Funcionalidades

* Listado de empleados
* Creación de empleados con imagen
* Edición y eliminación de empleados
* Visualización de detalles
* Consumo de API mediante clientes personalizados
* Manejo de formularios con validación

##  Arquitectura

* **Controllers** → Manejo de vistas y lógica de UI
* **ViewModels** → Modelos para formularios
* **ApiClients** → Consumo de la API
* **Views** → Interfaces de usuario con Razor

## Comunicación con la API

La aplicación consume la API mediante clases personalizadas (`ApiClients`) usando `HttpClient`.

Ejemplo:

* `EmpleadoApiClient`
* `ImagenesApiClient`
* `DepartamentosApiClient`

##  Manejo de imágenes

* El usuario sube una imagen desde el formulario
* La imagen se guarda en `wwwroot/images`
* Se registra en la API
* Se asocia al empleado mediante `IdImagen`
* Se muestra en las vistas usando `ImagenPath`

##  Configuración

1. Clonar el repositorio
2. Asegurarse de que la API esté en ejecución
3. Configurar la URL base de la API en los `ApiClients`
4. Ejecutar el proyecto:

   ```bash
   dotnet run
   ```

##  Vista de la aplicación

El sistema permite visualizar imágenes de empleados en listados y detalles mediante etiquetas `<img>`.

##  Buenas prácticas implementadas

* Separación entre frontend y backend
* Uso de ViewModels
* Consumo de API desacoplado
* Validaciones en formularios
* Manejo de archivos (imágenes)

##  Autor

Proyecto desarrollado como práctica de desarrollo web con ASP.NET Core MVC consumiendo una API REST.
