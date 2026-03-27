using PracticaMvcTi.Models;

namespace PracticaMvcTi.ViewModels.DepartamentoViewModel
{
    public class DeparatamentoListViewModel
    {
        public IEnumerable<Departamento> departamentos {  get; set; }   

        public PaginationInfo pagination { get; set; }

        public string search {  get; set; }
    }
}
