using PracticaMvcTi.Models;

namespace PracticaMvcTi.ViewModels.EmpleadoViewModel
{
    public class EmpleadoListViewModel
    {
        public IEnumerable<Empleado> empleados { get; set; }

        public PaginationInfo pagination { get; set; }

        public string search { get; set; }
    }
}
