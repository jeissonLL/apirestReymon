using System.Data;
using System.Data.SqlClient;
using TiendaApi.Conexion;
using TiendaApi.Modelo;

namespace TiendaApi.Datos
{
    public class DatosProductos
    {
        Conexionbd conexion = new Conexionbd();
        public async Task <List<ProductoModel>> MostrarProductos()
        {
            var lista = new List<ProductoModel>();
            using(var sql = new SqlConnection(conexion.cadenaSQL()))
            {
                using(var cmd = new SqlCommand("mostrarProductos", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType=CommandType.StoredProcedure;
                    using(var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var pModel = new ProductoModel();
                            pModel.id = (int)item["id"];
                            pModel.descripcion = (string)item["descripcion"];
                            pModel.precio = (decimal)item["precio"];
                            lista.Add(pModel);
                        }
                    }
                }
                return lista;
            }
        }

        public async Task insertarProductos(ProductoModel parametros)
        {
            using (var sql = new SqlConnection(conexion.cadenaSQL())) 
            {
                using (var cmd = new SqlCommand("insertarProductos", sql)) 
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@descripcion", parametros.descripcion);
                    cmd.Parameters.AddWithValue("precio", parametros.precio);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
