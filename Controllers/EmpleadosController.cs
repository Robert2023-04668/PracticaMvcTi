using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PracticaMvcTi.Clients;
using PracticaMvcTi.Models;
using PracticaMvcTi.ViewModels.EmpleadoViewModel;

namespace PracticaMvcTi.Controllers
{
    public class EmpleadosController : Controller
    {

        const long maxFileSize = 10 * 1024 * 1024;
        const string allowedExtensions = ".jpg,.png";
        private readonly EmpleadoApiClient _empleadoApi;
        private readonly DepartamentosApiClient _departamentos;
        private readonly EstadosApiClient _estadosApiClient;
        private readonly PuestosApiClient _puestosApiClient;
        private readonly ImagenesApiClient _imagenesApi;

        public EmpleadosController(EmpleadoApiClient empleadoApi, DepartamentosApiClient departamentos, EstadosApiClient estados, PuestosApiClient puestos, ImagenesApiClient imagenesApi)
        {
            _empleadoApi = empleadoApi;
            _departamentos = departamentos;
            _estadosApiClient = estados;
            _puestosApiClient = puestos;
            _imagenesApi = imagenesApi;

        }

        // INDEX
        public async Task<IActionResult> Index(int page = 1,string search = "",DateTime? fechaInicio = null,DateTime? fechaFin = null)
        {
            var resultado = await _empleadoApi.Get(page, search, fechaInicio, fechaFin);

            ViewBag.CurrentSearch = search;
            ViewBag.FechaInicio = fechaInicio?.ToString("yyyy-MM-dd");
            ViewBag.FechaFin = fechaFin?.ToString("yyyy-MM-dd");

            return View(resultado);
        }
        //CREAR
        #region

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var departamentosResponse = await _departamentos.Get();
            var departamentos = departamentosResponse.Data;
            ViewBag.IdDepartamento = new SelectList(departamentos, "IdDepartamento", "NombreDepartamento");

            var puestosResponse = await _puestosApiClient.Get();
            var puestos = puestosResponse.Data;
            ViewBag.IdPuesto = new SelectList(puestos, "IdPuesto", "NombrePuesto");


            var estadosResponse = await _estadosApiClient.Get();
            var estados = estadosResponse.Data;
            ViewBag.IdEstado = new SelectList(estados, "IdEstado", "NombreEstado");

            var viewModel = new EmpleadosFormViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmpleadosFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                int? idImagenSubida = null;


                if (model.Imagen.Length > maxFileSize)
                {
                    return BadRequest("El archivo sobrepasa los limites de 10mb");
                }
                if (!allowedExtensions.Contains(System.IO.Path.GetExtension(model.Imagen.FileName).ToLower()))
                {
                    return BadRequest("El tipo de archivo no es valido");
                }
                // 1. Procesar la imagen 
                if (model.Imagen != null && model.Imagen.Length > 0)
                {
                   
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Imagen.FileName);
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Imagen.CopyToAsync(fileStream);
                    }

                    // 2. Registrar la imagen en la API 
                    var nuevaImagen = new Imagene
                    {
                        NombreArchivo = fileName,
                        RutaArchivo = "/uploads/" + fileName,
                        TipoDeContenido = model.Imagen.ContentType,
                        Tamanio = (int)model.Imagen.Length
                    };

                    var imagenCreada = await _imagenesApi.Create(nuevaImagen);
                    idImagenSubida = imagenCreada.IdImagen;
                }

                // 3. Crear el Empleado vinculando el ID de la imagen
                var nuevoEmpleado = new EmpleadosDto
                {
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Edad = model.Edad,
                    IdDepartamento = model.IdDepartamento,
                    IdPuesto = model.IdPuesto,
                    Salario = model.Salario,
                    FechaDeNacimiento = model.FechaDeNacimiento,
                    FechaDeContratacion = model.FechaDeContratacion,
                    Direccion = model.Direccion,
                    Telefono = model.Telefono,
                    Correo = model.Correo,
                    IdEstado = model.IdEstado,
                    IdImagen = idImagenSubida 
                };

                await _empleadoApi.CreateEmpleado(nuevoEmpleado);
                return RedirectToAction(nameof(Index));
            }


            return RedirectToAction(nameof(Index));
        }

        #endregion


        // EDITAR
        #region
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
           
            var departamentosResponse = await _departamentos.Get();
            var departamentos = departamentosResponse.Data;
            ViewBag.IdDepartamento = new SelectList(departamentos, "IdDepartamento", "NombreDepartamento");

            var puestosResponse = await _puestosApiClient.Get();
            var puestos = puestosResponse.Data;
            ViewBag.IdPuesto = new SelectList(puestos, "IdPuesto", "NombrePuesto");

            var estadosResponse = await _estadosApiClient.Get();
            var estados = estadosResponse.Data;
            ViewBag.IdEstado = new SelectList(estados, "IdEstado", "NombreEstado");

           
            var empleado = await _empleadoApi.GetPorId(id);

            if (empleado == null) return NotFound();

           
            var viewModel = new EmpleadosFormViewModel();
            viewModel.IdEmpleado = empleado.IdEmpleado;
            viewModel.Nombre = empleado.Nombre ?? string.Empty;
            viewModel.Apellido = empleado.Apellido ?? string.Empty;
            viewModel.Direccion = empleado.Direccion ?? string.Empty;
            viewModel.FechaDeNacimiento = empleado.FechaDeNacimiento;
            viewModel.FechaDeContratacion = empleado.FechaDeContratacion;
            viewModel.Telefono = empleado.Telefono;
            viewModel.IdPuesto = empleado.IdPuesto ?? 0;
            viewModel.IdDepartamento = empleado.IdDepartamento ?? 0;
            viewModel.IdEstado = empleado.IdEstado ?? 0;
            viewModel.Correo = empleado.Correo;
            viewModel.Edad = empleado.Edad;
            viewModel.Salario = empleado.Salario;

            viewModel.Fotografia = empleado.ImagenPath;
      
            return View(viewModel);
        }
   
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EmpleadosFormViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                var empleadoActual = await _empleadoApi.GetPorId(id);
                int? idImagenFinal = empleadoActual.IdImagen;


                if (viewModel.Imagen != null && viewModel.Imagen.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(viewModel.Imagen.FileName);
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await viewModel.Imagen.CopyToAsync(fileStream);
                    }

                    var nuevaImagen = new Imagene
                    {
                        NombreArchivo = fileName,
                        RutaArchivo = "/uploads/" + fileName,
                        TipoDeContenido = viewModel.Imagen.ContentType,
                        Tamanio = (int)viewModel.Imagen.Length
                    };

                    var imagenCreada = await _imagenesApi.Create(nuevaImagen);
                    idImagenFinal = imagenCreada.IdImagen;
                }

                var empleadoDto = new EmpleadosDto
                {
                    IdEmpleado = id,
                    Nombre = viewModel.Nombre,
                    Apellido = viewModel.Apellido,
                    Edad = viewModel.Edad,
                    IdDepartamento = viewModel.IdDepartamento,
                    IdEstado = viewModel.IdEstado,
                    IdPuesto = viewModel.IdPuesto,
                    Salario = viewModel.Salario,
                    FechaDeContratacion = viewModel.FechaDeContratacion,
                    FechaDeNacimiento = viewModel.FechaDeNacimiento,
                    Direccion = viewModel.Direccion,
                    Telefono = viewModel.Telefono,
                    Correo = viewModel.Correo,
                    IdImagen = idImagenFinal
                };

                var departamentosResponse = await _departamentos.Get();
                var departamentos = departamentosResponse.Data;
                ViewBag.IdDepartamento = new SelectList(departamentos, "IdDepartamento", "NombreDepartamento");

                var puestosResponse = await _puestosApiClient.Get();
                var puestos = puestosResponse.Data;
                ViewBag.IdPuesto = new SelectList(puestos, "IdPuesto", "NombrePuesto");


                var estadosResponse = await _estadosApiClient.Get();
                var estados = estadosResponse.Data;
                ViewBag.IdEstado = new SelectList(estados, "IdEstado", "NombreEstado");
                await _empleadoApi.Edit(id, empleadoDto);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                viewModel.Exception = ex;
            }

            return RedirectToAction(nameof(Index));
        }


        #endregion

        //BORRAR
        #region

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var empleado = await _empleadoApi.GetPorId(id);
            if (empleado == null) return NotFound();

            var departamentosResponse = await _departamentos.Get();
            var departamentos = departamentosResponse.Data;
            ViewBag.IdDepartamento = new SelectList(departamentos, "IdDepartamento", "NombreDepartamento");

            var puestosResponse = await _puestosApiClient.Get();
            var puestos = puestosResponse.Data;
            ViewBag.IdPuesto = new SelectList(puestos, "IdPuesto", "NombrePuesto");


            var estadosResponse = await _estadosApiClient.Get();
            var estados = estadosResponse.Data;
            ViewBag.IdEstado = new SelectList(estados, "IdEstado", "NombreEstado");

            var viewModel = new EmpleadosFormViewModel();
            viewModel.IdEmpleado = empleado?.IdEmpleado ?? 0;
            viewModel.Nombre = empleado?.Nombre ?? string.Empty;
            viewModel.Apellido = empleado?.Apellido ?? string.Empty;
            viewModel.Direccion = empleado?.Direccion ?? string.Empty;
            viewModel.FechaDeNacimiento = empleado.FechaDeNacimiento;
            viewModel.FechaDeContratacion = empleado.FechaDeContratacion;
            viewModel.Telefono = empleado.Telefono;
            viewModel.IdPuesto = empleado?.IdPuesto ?? 0;
            viewModel.IdDepartamento = empleado?.IdDepartamento ?? 0;
            viewModel.IdEstado = empleado?.IdEstado ?? 0;
            viewModel.Correo = empleado.Correo;
            viewModel.Edad = empleado?.Edad;
            viewModel.Salario = empleado?.Salario;

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _empleadoApi.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        #endregion

        //DETAILS
        #region
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var empleado = await _empleadoApi.GetPorId(id);
            if (empleado == null) return NotFound();

            var departamentosResponse = await _departamentos.Get();
            var departamentos = departamentosResponse.Data;
            ViewBag.IdDepartamento = new SelectList(departamentos, "IdDepartamento", "NombreDepartamento");

            var puestosResponse = await _puestosApiClient.Get();
            var puestos = puestosResponse.Data;
            ViewBag.IdPuesto = new SelectList(puestos, "IdPuesto", "NombrePuesto");


            var estadosResponse = await _estadosApiClient.Get();
            var estados = estadosResponse.Data;
            ViewBag.IdEstado = new SelectList(estados, "IdEstado", "NombreEstado");

            var viewModel = new EmpleadosFormViewModel();
            viewModel.IdEmpleado = empleado?.IdEmpleado ?? 0;
            viewModel.Nombre = empleado?.Nombre ?? string.Empty;
            viewModel.Apellido = empleado?.Apellido ?? string.Empty;
            viewModel.Direccion = empleado?.Direccion ?? string.Empty;
            viewModel.FechaDeNacimiento = empleado.FechaDeNacimiento;
            viewModel.FechaDeContratacion = empleado.FechaDeContratacion;
            viewModel.Telefono = empleado.Telefono;
            viewModel.IdPuesto = empleado?.IdPuesto ?? 0;
            viewModel.IdDepartamento = empleado?.IdDepartamento ?? 0;
            viewModel.IdEstado = empleado?.IdEstado ?? 0;
            viewModel.Correo = empleado.Correo;
            viewModel.Edad = empleado?.Edad;
            viewModel.Salario = empleado?.Salario;
            viewModel.Fotografia = empleado?.ImagenPath;

            return View(viewModel);
        }
        #endregion
    }

}
