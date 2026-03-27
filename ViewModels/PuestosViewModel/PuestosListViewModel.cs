using PracticaMvcTi.Models;

namespace PracticaMvcTi.ViewModels.PuestosViewModel
{
    public class PuestosListViewModel
    {
        public IEnumerable<Puesto> puestos { get; set; }

        public PaginationInfo pagination { get; set; }

        public string search { get; set; }
    }
}
