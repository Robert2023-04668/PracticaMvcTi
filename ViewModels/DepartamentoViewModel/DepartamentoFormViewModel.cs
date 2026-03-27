using System.ComponentModel.DataAnnotations;

namespace PracticaMvcTi.ViewModels.DepartamentoViewModel
{
    public class DepartamentoFormViewModel
    {

        public int IdDepartamento { get; set; }
        [Required]
        public string NombreDepartamento { get; set; }

        public string Descripcion { get; set; }

        public Exception? Exception { get; set; }
    }
}
