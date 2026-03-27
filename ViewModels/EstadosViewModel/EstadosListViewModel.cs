using PracticaMvcTi.Models;

namespace PracticaMvcTi.ViewModels.EstadosViewModel
{
    public class EstadosListViewModel
    {
        public IEnumerable<Estado> estados { get; set; }

        public PaginationInfo pagination { get; set; }

        public string search { get; set; }
    }
}
