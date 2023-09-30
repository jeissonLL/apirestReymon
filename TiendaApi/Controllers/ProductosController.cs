using Microsoft.AspNetCore.Mvc;
using TiendaApi.Modelo;
using TiendaApi.Datos;

namespace TiendaApi.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController
    {
        [HttpGet]
        public async Task <ActionResult<List<ProductoModel>>> Get() 
        {
            var funsion = new DatosProductos();
            var lista = await funsion.MostrarProductos();
            return lista;
        }

        [HttpPost]
        public async Task Post([FromBody] ProductoModel parametros)
        {
            var funsion = new DatosProductos();
            await funsion.insertarProductos(parametros);
        } 
    }
}
