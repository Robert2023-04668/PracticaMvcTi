using PracticaMvcTi.Models;

namespace PracticaMvcTi.ViewModels.ImagenesViewModel
{
    public class ImagenesFormViewModel
    {
        public int IdImagen { get; set; }

        public string NombreArchivo { get; set; }

        public int? Tamanio { get; set; }

        public string TipoDeContenido { get; set; }

        public string RutaArchivo { get; set; }

        public Exception? Exception { get; set; }
    }
}
