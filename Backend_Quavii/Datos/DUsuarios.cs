using Backend_Quavii.Conexion;
using Backend_Quavii.Models;
using System.Data;
using System.Data.SqlClient;

namespace Backend_Quavii.Datos
{
    public class DUsuarios
    {
        //
        ConexionBD cn = new ConexionBD();

        public async Task<List<Usuarios>> mostrarUsuarios()
        {
            var lista = new List<Usuarios>();

            using (var sql = new SqlConnection(cn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("mostrarUsuarios", sql)) {
                    await sql.OpenAsync();

                    cmd.CommandType=CommandType.StoredProcedure;
                    using (var item = await cmd.ExecuteReaderAsync()) {
                        while (await item.ReadAsync()) {
                            var usu = new Usuarios();
                            usu.Usuario = (string)item["Usuario"];
                            usu.Estado = (int)item["Estado"];
                            usu.Correo = (string)item["Correo"];
                            lista.Add(usu);
                        }
                    }
                }
            }
            return lista;
        } 

        public async Task InsertarUsuario(Usuarios usu)
        {
            using (var sql = new SqlConnection(cn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("insertusuarop", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nom", usu.Nombres);
                    cmd.Parameters.AddWithValue("@correo", usu.Correo);
                    cmd.Parameters.AddWithValue("@usuario", usu.Usuario);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }

        public async Task<Usuarios> IngresarAplicacion(Usuarios usu)
        {
            var lista = new Usuarios();
            using(var sql = new SqlConnection(cn.ConectarSQL()))
            {
                using(var cmd = new SqlCommand("find_usuario", sql))
                {
                    cmd.CommandType=CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario", usu.Usuario);
                    cmd.Parameters.AddWithValue("@password", usu.password);
                    await sql.OpenAsync();
                    using (var item = await cmd.ExecuteReaderAsync())
                    {

                        if (await item.ReadAsync())
                        {
                            lista.Nombres = (string)item["Nombres"];
                            lista.Correo = (string)item["correo"];
                            lista.ApPaterno = (string)item["ApPaterno"];
                            lista.ApMaterno = (string)item["ApMaterno"];
                            lista.IdGrupoReparto = item["IdGrupoReparto"]== DBNull.Value?0:(int)item["IdGrupoReparto"];
                            lista.IdUsuario = (int)item["IdUsuario"];
                        }
                    }
                }
            }

            return lista;
        }

        public async Task EditarUsuario(Usuarios usu)
        {
            using (var sql = new SqlConnection(cn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("editarusu", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", usu.IdUsuario);
                    cmd.Parameters.AddWithValue("@correo", usu.Correo);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }

        public async Task EliminarUsuario(Usuarios usu)
        {
            using (var sql = new SqlConnection(cn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("eliminarusu", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", usu.IdUsuario);
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }
    }
}
