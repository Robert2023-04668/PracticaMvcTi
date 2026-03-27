namespace PracticaMvcTi.ViewModels.EmpleadoViewModel
{
    using Microsoft.AspNetCore.Http;
    public class EmpleadosFormViewModel
    {
        public int IdEmpleado { get; set; }

        public string? Fotografia { get; set; }

        public IFormFile? Imagen { get; set; }
        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public int? Edad { get; set; }

        public int? IdDepartamento { get; set; }
       
       
        public int? IdPuesto { get; set; }
       

        public int? Salario { get; set; }

        public DateOnly? FechaDeNacimiento { get; set; }

        public DateOnly? FechaDeContratacion { get; set; }

        public string? Direccion { get; set; }

        public string ?Telefono { get; set; }

        public string ?Correo { get; set; }
      
        public int? IdEstado { get; set; }

        public Exception? Exception { get; set; }
    }
}
