using ItlaTV.Persistance.Interfaces.dbo;
using ItlaTV.Persistance.Models.dbo;
using Microsoft.AspNetCore.Mvc.Rendering;

public sealed class SerieMethods
{
    private readonly IGenerosRepository _generosRepository;
    private readonly IProductorasRepository _productorasRepository;

    public SerieMethods(IGenerosRepository generosRepository, IProductorasRepository productorasRepository)
    {
        _generosRepository = generosRepository;
        _productorasRepository = productorasRepository;
    }


    public async Task<List<SelectListItem>> GetProductoras()
    {
        var productoraList = new List<SelectListItem>();

        var result = await _productorasRepository.GetAll();

        if (result.Success)
        {
            var productoras = (IEnumerable<ProductorasModel>)result.Data;
            productoraList.AddRange(productoras.Select(x => new SelectListItem
            {
                Text = x.Nombre,
                Value = x.ProductoraID.ToString()

            }));
        }
        return productoraList;
    }


    public async Task<List<SelectListItem>> GetGeneros()
    {
        var generosList = new List<SelectListItem>();

        var generosResult = await _generosRepository.GetAll();
        if (generosResult.Success)
        {
            var generos = (IEnumerable<GenerosModel>)generosResult.Data;
            generosList.AddRange(generos.Select(x => new SelectListItem
            {
                Text = x.Nombre,
                Value = x.GeneroID.ToString()

            }));
        }
        return generosList;
    }
}
