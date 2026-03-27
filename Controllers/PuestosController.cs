using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PracticaMvcTi.Clients;
using PracticaMvcTi.Models;
using PracticaMvcTi.ViewModels.PuestosViewModel;

namespace PracticaMvcTi.Controllers
{
    public class PuestosController : Controller
    {
        private readonly PuestosApiClient _puestos;
        private readonly DepartamentosApiClient _departamentos;

        public PuestosController(PuestosApiClient puestos, DepartamentosApiClient departamentos)
        {
            _puestos = puestos;
            _departamentos = departamentos;
        }

        public async Task<IActionResult> Index(int page = 1, string search = "")
        {

            var result = await _puestos.Get(page, search);
            ViewBag.Search = search;
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var departamentosResponse = await _departamentos.Get();
            var departamentos = departamentosResponse.Data;

            ViewBag.IdDepartamento = new SelectList(departamentos, "IdDepartamento", "NombreDepartamento");


            var viewModel = new PuestosFormViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PuestosFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var departamentosResponse = await _departamentos.Get();
                var departamentos = departamentosResponse.Data;
                ViewBag.IdDepartamento = new SelectList(departamentos, "IdDepartamento", "NombreDepartamento", viewModel.IdDepartamento);


                return View(viewModel);
            }

            try
            {
                var puesto = new PuestosDto
                {
                    NombrePuesto = viewModel.NombrePuesto,
                    Descripcion = viewModel.Descripcion,
                    IdDepartamento = viewModel.IdDepartamento

                };
                await _puestos.Create(puesto);


                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                viewModel.Exception = ex;
            }
            var departamentosError = await _departamentos.Get();
            ViewBag.IdDepartamento = new SelectList(departamentosError.Data, "IdDepartamento", "NombreDepartamento", viewModel.IdDepartamento);



            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var departamentosResponse = await _departamentos.Get();
            var departamentos = departamentosResponse.Data;

            ViewBag.IdDepartamento = new SelectList(departamentos, "IdDepartamento", "NombreDepartamento");

            var puesto = await _puestos.GetPorId(id);
            var vieModel = new PuestosFormViewModel();
            vieModel.idPuesto = puesto?.IdPuesto ?? 0;
            vieModel.IdDepartamento = puesto?.IdDepartamento ?? 0;
            vieModel.NombrePuesto = puesto?.NombrePuesto ?? string.Empty;
            vieModel.Descripcion = puesto?.Descripcion ?? string.Empty;
            return View(vieModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(PuestosFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }


            try
            {
                var puesto = new Puesto
                {
                    IdPuesto = viewModel.idPuesto,
                    NombrePuesto = viewModel.NombrePuesto,
                    Descripcion = viewModel.Descripcion,
                    IdDepartamento = viewModel.IdDepartamento,
                };


                await _puestos.Edit(viewModel.idPuesto, puesto);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                viewModel.Exception = ex;
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var puesto = await _puestos.GetPorId(id);
            var vieModel = new PuestosFormViewModel();
            vieModel.idPuesto = puesto?.IdPuesto ?? 0;
            vieModel.NombrePuesto = puesto?.NombrePuesto ?? string.Empty;
            vieModel.Descripcion = puesto?.Descripcion ?? string.Empty;
            vieModel.IdDepartamento = puesto?.IdDepartamento ?? 0;
            return View(vieModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, PuestosFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }


            try
            {
                var puesto = await _puestos.Delete(id);


                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                viewModel.Exception = ex;
            }

            return View(viewModel);
        }

    }
}
