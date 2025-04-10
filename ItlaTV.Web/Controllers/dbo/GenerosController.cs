using ItlaTV.Domain.Entities.dbo;
using ItlaTV.Persistance.Interfaces.dbo;
using ItlaTV.Persistance.Models.dbo;
using Microsoft.AspNetCore.Mvc;

namespace ItlaTV.Web.Controllers.dbo
{
    public class GenerosController : Controller
    {
        private readonly IGenerosRepository _generosRepository;

        public GenerosController(IGenerosRepository generosRepository)
        {
            _generosRepository = generosRepository;
        }

        public async Task <IActionResult> Index()
        {
            var result = await _generosRepository.GetAll();

            if (result.Success)
            {
                List<GenerosModel> generosModels = (List<GenerosModel>)result.Data;
                return View(generosModels);
            }
            return View();
        }

        
        public async Task <IActionResult> Details(int id)
        {
            var result = await _generosRepository.GetById(id);

            if (result.Success)
            {
                GenerosModel generosModel = (GenerosModel)result.Data;
                return View(generosModel);
            }
            return View();
        }


        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(Generos generos)
        {
            try
            {
                var result = await _generosRepository.Save(generos);

                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = result.Message;
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        
        public async Task <IActionResult> Edit(int id)
        {
            var result = await _generosRepository.GetById(id);

            if (result.Success)
            {
                GenerosModel generosModel = (GenerosModel)result.Data;
                return View(generosModel);
            }
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(Generos generos)
        {
            try
            {
                var result = await _generosRepository.Update(generos);

                if (result.Success) 
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = result.Message;
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        
        public async Task <IActionResult> Delete(int id)
        {
            var result = await _generosRepository.GetById(id);

            if (result.Success)
            {
                GenerosModel generosModel = (GenerosModel)result.Data;
                return View(generosModel);
            }
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete(Generos generos)
        {
            try
            {
                var result = await _generosRepository.Remove(generos);

                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = result.Message;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
