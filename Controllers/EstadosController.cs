using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaMvcTi.Clients;
using PracticaMvcTi.Models;
using PracticaMvcTi.ViewModels.EstadosViewModel;

namespace PracticaMvcTi.Controllers
{
    public class EstadosController : Controller
    {

        private readonly EstadosApiClient _estados;

        public EstadosController(EstadosApiClient estados)
        {
            _estados = estados;
        }

        public async Task<IActionResult> Index(int page = 1,  string search = "")
        {
            var result = await _estados.Get(page, search);
            ViewBag.Search = search;
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new EstadosFormViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EstadosFormViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                var estado = new Estado
                {
                    NombreEstado = viewModel.NombreEstado,
                    Descripcion = viewModel.Descripcion
                };
                await  _estados.Create(estado);
               
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                viewModel.Exception = ex;
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var estado = await _estados.GetoPorId(id);
            var vieModel = new EstadosFormViewModel();
            vieModel.IdEstado = estado?.IdEstado ?? 0;
            vieModel.NombreEstado = estado?.NombreEstado ?? string.Empty;
            vieModel.Descripcion = estado?.Descripcion ?? string.Empty;
            return View(vieModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, EstadosFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var estado = new Estado
            {
                IdEstado = viewModel.IdEstado,
                NombreEstado = viewModel.NombreEstado,
                Descripcion = viewModel.Descripcion
            };

            await _estados.Edit(viewModel.IdEstado, estado);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var estado = await _estados.GetoPorId(id);
            var vieModel = new EstadosFormViewModel();
            vieModel.IdEstado = estado?.IdEstado ?? 0;
            vieModel.NombreEstado = estado?.NombreEstado ?? string.Empty;
            vieModel.Descripcion = estado?.Descripcion ?? string.Empty;
            return View(vieModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, EstadosFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }


            try
            {
                var estado = await _estados.GetoPorId(id);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                viewModel.Exception = ex;
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
          
            var estado = await _estados.GetoPorId(id);
            var vieModel = new EstadosFormViewModel();
            vieModel.IdEstado = estado?.IdEstado ?? 0;
            vieModel.NombreEstado = estado?.NombreEstado ?? string.Empty;
            vieModel.Descripcion = estado?.Descripcion ?? string.Empty;
            return View(vieModel);

        }



    }
}
