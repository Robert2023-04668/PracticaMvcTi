using System.ComponentModel.DataAnnotations;

namespace PracticaMvcTi.ViewModels.PuestosViewModel
{
    public class PuestosFormViewModel
    {

        public int idPuesto { get; set; }
        public string NombrePuesto { get; set; }


        public string Descripcion { get; set; }

        [Required]
        public int IdDepartamento { get; set; }
       
        public Exception? Exception { get; set; }
    }
}
