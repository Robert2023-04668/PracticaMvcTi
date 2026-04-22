using Microsoft.AspNetCore.Mvc;
using PracticaMvcTi.Clients;
using PracticaMvcTi.Models;
using PracticaMvcTi.ViewModels.DepartamentoViewModel;

namespace PracticaMvcTi.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class DepartamentosController : Controller
    {
        private readonly DepartamentosApiClient _departamentos;

        public DepartamentosController(DepartamentosApiClient departamentos)
        {
            _departamentos = departamentos;
        }

        // INDEX
        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            if (page <= 0)
            {
                page = 1;
            }

            var result = await _departamentos.Get(page, search ?? "");
            ViewBag.Search = search;

            return View(result);
        }

        // ========================= CREATE =========================

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new DepartamentoFormViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartamentoFormViewModel viewModel)
        {
            // Validaciones manuales adicionales
            if (string.IsNullOrWhiteSpace(viewModel.NombreDepartamento))
            {
                ModelState.AddModelError("NombreDepartamento", "El nombre no puede estar vacío");
            }

            if (!string.IsNullOrEmpty(viewModel.NombreDepartamento) && viewModel.NombreDepartamento.Length < 3)
            {
                ModelState.AddModelError("NombreDepartamento", "Debe tener al menos 3 caracteres");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                var departamento = new Departamento
                {
                    NombreDepartamento = viewModel.NombreDepartamento.Trim(),
                    Descripcion = viewModel.Descripcion?.Trim()
                };

                await _departamentos.CreateDepartament(departamento);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al crear el departamento");
                viewModel.Exception = ex;
            }

            return View(viewModel);
        }

        // ========================= EDIT =========================

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido");
            }

            var departamento = await _departamentos.GetDepartamentoPorId(id);

            if (departamento == null)
            {
                return NotFound();
            }

            var viewModel = new DepartamentoFormViewModel
            {
                IdDepartamento = departamento.IdDepartamento,
                NombreDepartamento = departamento.NombreDepartamento,
                Descripcion = departamento.Descripcion
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartamentoFormViewModel viewModel)
        {
            if (viewModel.IdDepartamento <= 0)
            {
                ModelState.AddModelError("", "ID inválido");
            }

            if (string.IsNullOrWhiteSpace(viewModel.NombreDepartamento))
            {
                ModelState.AddModelError("NombreDepartamento", "El nombre no puede estar vacío");
            }

            if (!string.IsNullOrEmpty(viewModel.NombreDepartamento) && viewModel.NombreDepartamento.Length < 3)
            {
                ModelState.AddModelError("NombreDepartamento", "Debe tener al menos 3 caracteres");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                var departamento = new Departamento
                {
                    IdDepartamento = viewModel.IdDepartamento,
                    NombreDepartamento = viewModel.NombreDepartamento.Trim(),
                    Descripcion = viewModel.Descripcion?.Trim()
                };

                await _departamentos.EditDepartment(viewModel.IdDepartamento, departamento);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error al editar el departamento");
            }

            return View(viewModel);
        }

        // ========================= DELETE =========================

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido");
            }

            var departamento = await _departamentos.GetDepartamentoPorId(id);

            if (departamento == null)
            {
                return NotFound();
            }

            var viewModel = new DepartamentoFormViewModel
            {
                IdDepartamento = departamento.IdDepartamento,
                NombreDepartamento = departamento.NombreDepartamento,
                Descripcion = departamento.Descripcion
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, DepartamentoFormViewModel viewModel)
        {
            if (id <= 0)
            {
                ModelState.AddModelError("", "ID inválido");
                return View(viewModel);
            }

            try
            {
                await _departamentos.DeleteDepartamentoPorId(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al eliminar el departamento");
                viewModel.Exception = ex;
            }

            return View(viewModel);
        }

        // ========================= DETAILS =========================

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido");
            }

            var departamento = await _departamentos.GetDepartamentoPorId(id);

            if (departamento == null)
            {
                return NotFound();
            }

            var viewModel = new DepartamentoFormViewModel
            {
                IdDepartamento = departamento.IdDepartamento,
                NombreDepartamento = departamento.NombreDepartamento,
                Descripcion = departamento.Descripcion
            };

            return View(viewModel);
        }
    }
}