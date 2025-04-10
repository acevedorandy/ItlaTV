using ItlaTV.Domain.Entities.dbo;
using ItlaTV.Persistance.Interfaces.dbo;
using ItlaTV.Persistance.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ItlaTV.Web.Controllers.dbo
{
    public class SeriesController : Controller
    {
        private readonly ISeriesRepository _seriesRepository;
        private readonly SerieMethods _serieMethods;


        public SeriesController(ISeriesRepository seriesRepository, SerieMethods serieMethods)
        {
            _seriesRepository = seriesRepository;
            _serieMethods = serieMethods;
        }
        
        public async Task <IActionResult> Index(string name, int generoId, int productoraId)
        {
            ViewBag.Productoras = await _serieMethods.GetProductoras();
            ViewBag.Generos = await _serieMethods.GetGeneros();

            var result = await _seriesRepository.GetSeriesByGenres(name, generoId, productoraId);

            if (result.Success)
            {
                List<SeriesViewModel> seriesViews = (List<SeriesViewModel>)result.Data;
                return View(seriesViews);
            }
            return View();
        }

        
        public async Task <IActionResult> Details(int id)
        {
            var result = await _seriesRepository.GetById(id);

            if (result.Success)
            {
                Series seriesModel = (Series)result.Data;
                return View(seriesModel);
            }
            return View();
        }


        public async Task<IActionResult> Create()
        {
            var productorasResult = await _serieMethods.GetProductoras();
            var generosResult = await _serieMethods.GetGeneros();
            
            ViewBag.Productoras = productorasResult;
            ViewBag.Generos = generosResult;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(Series series)
        {
            try
            {
                var result = await _seriesRepository.Save(series);

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
            var productorasResult = await _serieMethods.GetProductoras();
            var generosResult = await _serieMethods.GetGeneros();

            ViewBag.Productoras = productorasResult;
            ViewBag.Generos = generosResult;

            var result = await _seriesRepository.GetById(id);

            if (result.Success)
            {
                Series series = (Series)result.Data;
                return View(series);
            }
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Edit(Series series)
        {
            try
            {
                var result = await _seriesRepository.Update(series);

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
            var result = await _seriesRepository.GetById(id);

            if (result.Success) 
            {
                Series series = (Series)result.Data;
                return View(series);
            }
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete(Series series)
        {
            try
            {
                var result = await _seriesRepository.Remove(series);

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
