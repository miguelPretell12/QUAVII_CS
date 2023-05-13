using Backend_Quavii.Conexion;
using Backend_Quavii.Models;
using System.Data.SqlClient;
using System.Data;

namespace Backend_Quavii.Datos
{
    public class DTipoObservacion
    {
        ConexionBD cn = new ConexionBD();

        public async Task<List<TiposObservaciones>> ListarTipoObservacion() {
            var oList = new List<TiposObservaciones>();

               using(var sql = new SqlConnection(cn.ConectarSQL()))
                {
                    using(var cmd= new SqlCommand("views_tiposObservaciones", sql))
                    {
                        await sql.OpenAsync();
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (var item = await cmd.ExecuteReaderAsync())
                        {
                            while (await item.ReadAsync())
                            {
                                var to = new TiposObservaciones();
                                to.IdTipoObservacion = (int)item["IdTipoObservacion"];
                                to.descripcion = item["descripcion"] == DBNull.Value ? String.Empty : (string)item["descripcion"];
                                oList.Add(to);
                            }
                            
                        }
                    }
                }
            return oList;
        }
    }
}
