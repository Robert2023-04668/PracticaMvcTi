using Microsoft.AspNetCore.Mvc;
using PracticaMvcTi.Clients;
using PracticaMvcTi.Models;
using PracticaMvcTi.ViewModels.EstadosViewModel;
using PracticaMvcTi.ViewModels.ImagenesViewModel;
using System.Threading.Tasks;

namespace PracticaMvcTi.Controllers
{
    public class ImagenesController : Controller
    {
        private readonly ImagenesApiClient _imagenesApiClient;

        public ImagenesController(ImagenesApiClient imagenesApiClient)
        {
            _imagenesApiClient = imagenesApiClient;
        }

        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            var result = await _imagenesApiClient.Get(page, search);
            ViewBag.Search = search;
            return View(result);
        }
        public async Task<IActionResult> Details(int id)
        {
            var imagene = await _imagenesApiClient.GetPorId(id);
            
            var vieModel = new ImagenesFormViewModel();
            vieModel.IdImagen = imagene?.IdImagen ?? 0;
            vieModel.RutaArchivo = imagene?.RutaArchivo ?? string.Empty;
            vieModel.NombreArchivo = imagene?.NombreArchivo ?? string.Empty;
            vieModel.TipoDeContenido = imagene?.TipoDeContenido ?? string.Empty;
            vieModel.Tamanio = imagene?.Tamanio ?? 0;
            return View(vieModel);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new ImagenesFormViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ImagenesFormViewModel viewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            try
            {
                var imagene = new Imagene
                {
                    RutaArchivo = viewModel.RutaArchivo,
                    NombreArchivo = viewModel.NombreArchivo,
                    TipoDeContenido = viewModel.TipoDeContenido,
                    Tamanio = viewModel.Tamanio
                };
                await _imagenesApiClient.Create(imagene);

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
            var imagene = await _imagenesApiClient.GetPorId(id);
            var vieModel = new ImagenesFormViewModel();
            vieModel.IdImagen = imagene?.IdImagen ?? 0;
            vieModel.RutaArchivo = imagene?.RutaArchivo ?? string.Empty;
            vieModel.NombreArchivo = imagene?.NombreArchivo ?? string.Empty;
            vieModel.TipoDeContenido = imagene?.TipoDeContenido ?? string.Empty;
            vieModel.Tamanio = imagene?.Tamanio ?? 0;
            return View(vieModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, ImagenesFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var imagene = new Imagene
            {
                RutaArchivo = viewModel.RutaArchivo,
                NombreArchivo = viewModel.NombreArchivo,
                TipoDeContenido = viewModel.TipoDeContenido,
                Tamanio = viewModel.Tamanio
            };
            await _imagenesApiClient.Create(imagene);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var imagene = await _imagenesApiClient.GetPorId(id);
            var vieModel = new ImagenesFormViewModel();
            vieModel.IdImagen = imagene?.IdImagen ?? 0;
            vieModel.RutaArchivo = imagene?.RutaArchivo ?? string.Empty;
            vieModel.NombreArchivo = imagene?.NombreArchivo ?? string.Empty;
            vieModel.TipoDeContenido = imagene?.TipoDeContenido ?? string.Empty;
            vieModel.Tamanio = imagene?.Tamanio ?? 0;
            return View(vieModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, ImagenesFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);

            }


            try
            {
                var estado = await _imagenesApiClient.GetPorId(id);

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
