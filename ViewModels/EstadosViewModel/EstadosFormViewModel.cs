namespace PracticaMvcTi.ViewModels.EstadosViewModel
{
    public class EstadosFormViewModel
    {
        public int IdEstado {  get; set; }
        public string NombreEstado { get; set; }
        public string Descripcion { get; set; }

        public Exception? Exception { get; set; }
    }
}
