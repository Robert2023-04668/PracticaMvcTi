using Microsoft.AspNetCore.Mvc;
using PracticaMvcTi.Clients;
using PracticaMvcTi.Models;
using PracticaMvcTi.ViewModels.DepartamentoViewModel;

namespace PracticaMvcTi.Controllers
{
    public class DepartamentosController : Controller
    {
        private readonly DepartamentosApiClient _departamentos;

        public DepartamentosController(DepartamentosApiClient departamentos)
        {
            _departamentos = departamentos;
        }

        public async Task<IActionResult> Index(int page = 1, string search  = "")
        {
            var result = await _departamentos.Get(page, search);
            ViewBag.Search = search;
            return View(result);
        }

        //CREATE
        #region
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new DepartamentoFormViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartamentoFormViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                var departamento = new Departamento
                {
                    NombreDepartamento = viewModel.NombreDepartamento,
                    Descripcion = viewModel.Descripcion
                };
                await _departamentos.CreateDepartament(departamento);
              

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                viewModel.Exception = ex;
            }

            return View(viewModel);
        }

        #endregion

        // EDITAR
        #region
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var departamento = await _departamentos.GetDepartamentoPorId(id);
            var vieModel = new DepartamentoFormViewModel();
            vieModel.IdDepartamento = departamento?.IdDepartamento?? 0;
            vieModel.NombreDepartamento = departamento?.NombreDepartamento ?? string.Empty;
            vieModel.Descripcion = departamento?.Descripcion ?? string.Empty;
            return View(vieModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartamentoFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var departamento = new Departamento
            {
                IdDepartamento = viewModel.IdDepartamento,
                NombreDepartamento = viewModel.NombreDepartamento,
                Descripcion = viewModel.Descripcion
            };

            await _departamentos.EditDepartment(viewModel.IdDepartamento, departamento);

            return RedirectToAction(nameof(Index));
        }

        #endregion

        //Borrar
        #region
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var departamento = await _departamentos.GetDepartamentoPorId(id);
            var vieModel = new DepartamentoFormViewModel();
            vieModel.IdDepartamento = departamento?.IdDepartamento ?? 0;
            vieModel.NombreDepartamento = departamento?.NombreDepartamento ?? string.Empty;
            vieModel.Descripcion = departamento?.Descripcion ?? string.Empty;
            return View(vieModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, DepartamentoFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }


            try
            {
                var departamento = await _departamentos.DeleteDepartamentoPorId(id);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                viewModel.Exception = ex;
            }

            return View(viewModel);
        }

        #endregion

        //DETALLE
        #region
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var departamento = await _departamentos.GetDepartamentoPorId(id);
            var vieModel = new DepartamentoFormViewModel();
            vieModel.IdDepartamento = departamento?.IdDepartamento ?? 0;
            vieModel.NombreDepartamento = departamento?.NombreDepartamento ?? string.Empty;
            vieModel.Descripcion = departamento?.Descripcion ?? string.Empty;
            return View(vieModel);


        }
        #endregion

    }
}
