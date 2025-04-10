using ItlaTV.Domain.Entities.dbo;
using ItlaTV.Persistance.Interfaces.dbo;
using ItlaTV.Persistance.Models.dbo;
using Microsoft.AspNetCore.Mvc;

namespace ItlaTV.Web.Controllers.dbo
{
    public class ProductorasController : Controller
    {
        private readonly IProductorasRepository _productorasRepository;

        public ProductorasController(IProductorasRepository productorasRepository)
        {
            _productorasRepository = productorasRepository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _productorasRepository.GetAll();

            if (result.Success)
            {
                List<ProductorasModel> productorasModels = (List<ProductorasModel>)result.Data;
                return View(productorasModels);
            }
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _productorasRepository.GetById(id);

            if (result.Success)
            {
                ProductorasModel productorasModel = (ProductorasModel)result.Data;
                return View(productorasModel);
            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Productoras productoras)
        {
            try
            {
                var result = await _productorasRepository.Save(productoras);

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

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _productorasRepository.GetById(id);

            if (result.Success)
            {
                ProductorasModel productorasModel = (ProductorasModel)result.Data;
                return View(productorasModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Productoras Productoras)
        {
            try
            {
                var result = await _productorasRepository.Update(Productoras);

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

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productorasRepository.GetById(id);

            if (result.Success)
            {
                ProductorasModel productorasModel = (ProductorasModel)result.Data;
                return View(productorasModel);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Productoras productoras)
        {
            try
            {
                var result = await _productorasRepository.Remove(productoras);

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
