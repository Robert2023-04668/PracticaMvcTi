namespace PracticaMvcTi.Models
{
    public class Imagene
    {
        public int IdImagen { get; set; }

        public string NombreArchivo { get; set; }

        public int? Tamanio { get; set; }

        public string TipoDeContenido { get; set; }

        public string RutaArchivo { get; set; }

        public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
    }
}
