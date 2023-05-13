using Backend_Quavii.Conexion;
using Backend_Quavii.Models;
using System.Data;
using System.Data.SqlClient;

namespace Backend_Quavii.Datos
{
    public class DOrdenTrabajo
    {
        ConexionBD cn = new ConexionBD();
        public static class GlobalData
        {
            public static string? detail { get; set; }
        }

        public List<ListarTableDBO> listarTableDBO()
        {
            var oLista = new List<ListarTableDBO>();
            try
            {
                //var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.ConectarSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("view_OrdenTrabajo", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new ListarTableDBO()
                            {
                                COLUMNA_1 = Convert.ToInt32(dr["IdOrdenTrabajo"]),
                                COLUMNA_2 = dr["TipoTrabajo"].ToString(),
                            });
                        }
                    }
                }
                return oLista;
            }
            catch (Exception e)
            {
                GlobalData.detail = e.Message;
                return oLista;
            }
        }

        public async Task<List<OrdenTrabajo>> mostrarMedidor(Usuarios usu)
        {
            var lista = new List<OrdenTrabajo>();

            try {
                using (var sql = new SqlConnection(cn.ConectarSQL()))
                {
                    using (var cmd = new SqlCommand("find_OrdenTrabajo", sql))
                    {
                        await sql.OpenAsync();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idusuario", usu.IdUsuario);
                        using (var item = await cmd.ExecuteReaderAsync())
                        {
                            while (await item.ReadAsync())
                            {
                                var ordenTrabajo = new OrdenTrabajo();
                                ordenTrabajo.IdOrdenTrabajo = (int)item["IdOrdenTrabajo"];
                                ordenTrabajo.TipoTrabajo = item["TipoTrabajo"] == DBNull.Value ? String.Empty : (string)item["TipoTrabajo"];
                                ordenTrabajo.SectorOperativo = item["SectorOperativo"] == DBNull.Value ? String.Empty : (string)item["SectorOperativo"];
                                ordenTrabajo.Producto = (double)item["Producto"];
                                ordenTrabajo.Contrato = (double)item["Contrato"];
                                ordenTrabajo.Ciclo = (double)item["Ciclo"];
                                ordenTrabajo.Medidor = item["Medidor"] == DBNull.Value ? String.Empty : (string)item["Medidor"];
                                ordenTrabajo.CoordenadaX = (double)item["CoordenadaX"];
                                ordenTrabajo.CoordenadaY = (double)item["CoordenadaY"];
                                ordenTrabajo.LEC = item["LEC"] == DBNull.Value ? String.Empty : (string)item["LEC"];
                                ordenTrabajo.DistritoSuministro = item["DistritoSuministro"] == DBNull.Value ? String.Empty : (string)item["DistritoSuministro"];
                                ordenTrabajo.ProvinciaSuministro = item["ProvinciaSuministro"] == DBNull.Value ? String.Empty : (string)item["ProvinciaSuministro"];
                                ordenTrabajo.DepartamentoSuministro = item["DepartamentoSuministro"] == DBNull.Value ? String.Empty : (string)item["DepartamentoSuministro"];
                                ordenTrabajo.Barrio = item["Barrio"] == DBNull.Value ? String.Empty : (string)item["Barrio"];
                                ordenTrabajo.UbicacionGeografica = item["UbicacionGeografica"] == DBNull.Value ? String.Empty : (string)item["UbicacionGeografica"];
                                ordenTrabajo.DetalleDireccion = item["DetalleDireccion"] == DBNull.Value ? String.Empty : (string)item["DetalleDireccion"];
                                ordenTrabajo.TipoInconsistencia = item["TipoInconsistencia"] == DBNull.Value ? String.Empty : (string)item["TipoInconsistencia"];
                                ordenTrabajo.OrdenReparto = (double)item["OrdenReparto"];
                                ordenTrabajo.UsuarioReparto = item["UsuarioReparto"] == DBNull.Value ? String.Empty : (string)item["UsuarioReparto"];
                                ordenTrabajo.observacion = item["Observacion"] == DBNull.Value ? String.Empty : (string)item["Observacion"];
                                ordenTrabajo.imagenes = item["imagenes"] == DBNull.Value ? String.Empty : (string)item["imagenes"];
                                ordenTrabajo.NuevaLectura = item["NuevaLectura"] == DBNull.Value ? String.Empty : (string)item["NuevaLectura"];
                                ordenTrabajo.ruta = item["ruta"] == DBNull.Value ? String.Empty : (string)item["ruta"];
                                ordenTrabajo.FechaHoraLectura = item["FechaHoraLectura"] == DBNull.Value ? (DateTime?)null : (DateTime)item["FechaHoraLectura"];
                                ordenTrabajo.IdTipoObservacion = item["idTipoObservacion"] == DBNull.Value ? 0 : (int)item["idTipoObservacion"];
                                ordenTrabajo.descripcion = item["descripcion"] == DBNull.Value ? String.Empty : (string)item["descripcion"];
                                lista.Add(ordenTrabajo);
                            }
                            return lista;
                        }

                    }
                }
            }
            catch(Exception e) {
                GlobalData.detail = e.Message;
                return lista;
            }
        }

        public async Task<List<OrdenTrabajo>> mostrarMedidorCiclo(OrdenTrabajo ot)
        {
            var lista = new List<OrdenTrabajo>();

            using (var sql = new SqlConnection(cn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("find_OrdenTrabajoXMedidorCiclo", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idusuario", ot.IdUsuario);
                    cmd.Parameters.AddWithValue("@medidor", ot.Medidor);
                    cmd.Parameters.AddWithValue("@ciclo", ot.Ciclo);
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var ordenTrabajo = new OrdenTrabajo();
                            ordenTrabajo.IdOrdenTrabajo = (int)item["IdOrdenTrabajo"];
                            ordenTrabajo.TipoTrabajo = item["TipoTrabajo"] == DBNull.Value ? String.Empty : (string)item["TipoTrabajo"];
                            ordenTrabajo.SectorOperativo = item["SectorOperativo"] == DBNull.Value ? String.Empty : (string)item["SectorOperativo"];
                            ordenTrabajo.Producto = (double)item["Producto"];
                            ordenTrabajo.Contrato = (double)item["Contrato"];
                            ordenTrabajo.Ciclo = (double)item["Ciclo"];
                            ordenTrabajo.Medidor = item["Medidor"] == DBNull.Value ? String.Empty : (string)item["Medidor"];
                            ordenTrabajo.CoordenadaX = (double)item["CoordenadaX"];
                            ordenTrabajo.CoordenadaY = (double)item["CoordenadaY"];
                            ordenTrabajo.LEC = item["LEC"] == DBNull.Value ? String.Empty : (string)item["LEC"];
                            ordenTrabajo.DistritoSuministro = item["DistritoSuministro"] == DBNull.Value ? String.Empty : (string)item["DistritoSuministro"];
                            ordenTrabajo.ProvinciaSuministro = item["ProvinciaSuministro"] == DBNull.Value ? String.Empty : (string)item["ProvinciaSuministro"];
                            ordenTrabajo.DepartamentoSuministro = item["DepartamentoSuministro"] == DBNull.Value ? String.Empty : (string)item["DepartamentoSuministro"];
                            ordenTrabajo.Barrio = item["Barrio"] == DBNull.Value ? String.Empty : (string)item["Barrio"];
                            ordenTrabajo.UbicacionGeografica = item["UbicacionGeografica"] == DBNull.Value ? String.Empty : (string)item["UbicacionGeografica"];
                            ordenTrabajo.DetalleDireccion = item["DetalleDireccion"] == DBNull.Value ? String.Empty : (string)item["DetalleDireccion"];
                            ordenTrabajo.TipoInconsistencia = item["TipoInconsistencia"] == DBNull.Value ? String.Empty : (string)item["TipoInconsistencia"];
                            ordenTrabajo.OrdenReparto = (double)item["OrdenReparto"];
                            ordenTrabajo.UsuarioReparto = item["UsuarioReparto"] == DBNull.Value ? String.Empty : (string)item["UsuarioReparto"];
                            ordenTrabajo.observacion = item["Observacion"] == DBNull.Value ? String.Empty : (string)item["Observacion"];
                            ordenTrabajo.imagenes = item["imagenes"] == DBNull.Value ? String.Empty : (string)item["imagenes"];
                            ordenTrabajo.NuevaLectura = item["NuevaLectura"] == DBNull.Value ? String.Empty : (string)item["NuevaLectura"];
                            ordenTrabajo.ruta = item["ruta"] == DBNull.Value ? String.Empty : (string)item["ruta"];
                            ordenTrabajo.FechaHoraLectura = item["FechaHoraLectura"] == DBNull.Value ? (DateTime?)null : (DateTime)item["FechaHoraLectura"];
                            ordenTrabajo.IdTipoObservacion = item["idTipoObservacion"] == DBNull.Value ? 0 : (int)item["idTipoObservacion"];
                            ordenTrabajo.descripcion = item["descripcion"] == DBNull.Value ? String.Empty : (string)item["descripcion"];
                            lista.Add(ordenTrabajo);
                        }
                    }
                }
            }
            return lista;
        }

        public async Task<List<OrdenTrabajo>> mostrarMedidorPendienteRegistrado(OrdenTrabajo ot)
        {
            var lista = new List<OrdenTrabajo>();

            using (var sql = new SqlConnection(cn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("find_OrdenTrabajoRegistradoPendientes", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idusuario", ot.IdUsuario);
                    cmd.Parameters.AddWithValue("@opcion1", ot.opcion1);
                    cmd.Parameters.AddWithValue("@opcion2", ot.opcion2);
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var ordenTrabajo = new OrdenTrabajo();
                            ordenTrabajo.IdOrdenTrabajo = (int)item["IdOrdenTrabajo"];
                            ordenTrabajo.TipoTrabajo = item["TipoTrabajo"] == DBNull.Value ? String.Empty : (string)item["TipoTrabajo"];
                            ordenTrabajo.SectorOperativo = item["SectorOperativo"] == DBNull.Value ? String.Empty : (string)item["SectorOperativo"];
                            ordenTrabajo.Producto = (double)item["Producto"];
                            ordenTrabajo.Contrato = (double)item["Contrato"];
                            ordenTrabajo.Ciclo = (double)item["Ciclo"];
                            ordenTrabajo.Medidor = item["Medidor"] == DBNull.Value ? String.Empty : (string)item["Medidor"];
                            ordenTrabajo.CoordenadaX = (double)item["CoordenadaX"];
                            ordenTrabajo.CoordenadaY = (double)item["CoordenadaY"];
                            ordenTrabajo.LEC = item["LEC"] == DBNull.Value ? String.Empty : (string)item["LEC"];
                            ordenTrabajo.DistritoSuministro = item["DistritoSuministro"] == DBNull.Value ? String.Empty : (string)item["DistritoSuministro"];
                            ordenTrabajo.ProvinciaSuministro = item["ProvinciaSuministro"] == DBNull.Value ? String.Empty : (string)item["ProvinciaSuministro"];
                            ordenTrabajo.DepartamentoSuministro = item["DepartamentoSuministro"] == DBNull.Value ? String.Empty : (string)item["DepartamentoSuministro"];
                            ordenTrabajo.Barrio = item["Barrio"] == DBNull.Value ? String.Empty : (string)item["Barrio"];
                            ordenTrabajo.UbicacionGeografica = item["UbicacionGeografica"] == DBNull.Value ? String.Empty : (string)item["UbicacionGeografica"];
                            ordenTrabajo.DetalleDireccion = item["DetalleDireccion"] == DBNull.Value ? String.Empty : (string)item["DetalleDireccion"];
                            ordenTrabajo.TipoInconsistencia = item["TipoInconsistencia"] == DBNull.Value ? String.Empty : (string)item["TipoInconsistencia"];
                            ordenTrabajo.OrdenReparto = (double)item["OrdenReparto"];
                            ordenTrabajo.UsuarioReparto = item["UsuarioReparto"] == DBNull.Value ? String.Empty : (string)item["UsuarioReparto"];
                            ordenTrabajo.FechaHoraLectura = item["FechaHoraLectura"] == DBNull.Value ? (DateTime?)null : (DateTime)item["FechaHoraLectura"];
                            ordenTrabajo.observacion = item["Observacion"] == DBNull.Value ? String.Empty : (string)item["Observacion"];
                            ordenTrabajo.imagenes = item["imagenes"] == DBNull.Value ? String.Empty : (string)item["imagenes"];
                            ordenTrabajo.NuevaLectura = item["NuevaLectura"] == DBNull.Value ? String.Empty : (string)item["NuevaLectura"];
                            ordenTrabajo.ruta = item["ruta"] == DBNull.Value ? String.Empty : (string)item["ruta"];
                            ordenTrabajo.IdTipoObservacion = item["idTipoObservacion"] == DBNull.Value ? 0 : (int)item["idTipoObservacion"];
                            ordenTrabajo.descripcion = item["descripcion"] == DBNull.Value ? String.Empty : (string)item["descripcion"];
                            lista.Add(ordenTrabajo);
                        }
                    }
                }
            }
            return lista;
        }

        public async Task<OrdenTrabajo> obtenerOrdenTrabajo(OrdenTrabajo ot) {
            var ordenTrabajo = new OrdenTrabajo();
            using (var sql = new SqlConnection(cn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("find_ordentrabajolectura", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdOrdenTrabajo", ot.IdOrdenTrabajo);
                    await sql.OpenAsync();
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        if (await item.ReadAsync())
                        {
                            ordenTrabajo.IdOrdenTrabajo = (int)item["IdOrdenTrabajo"];
                            ordenTrabajo.TipoTrabajo = item["TipoTrabajo"] == DBNull.Value ? String.Empty : (string)item["TipoTrabajo"];
                            ordenTrabajo.SectorOperativo = item["SectorOperativo"] == DBNull.Value ? String.Empty : (string)item["SectorOperativo"];
                            ordenTrabajo.Producto = (double)item["Producto"];
                            ordenTrabajo.Contrato = (double)item["Contrato"];
                            ordenTrabajo.Ciclo = (double)item["Ciclo"];
                            ordenTrabajo.Medidor = item["Medidor"] == DBNull.Value ? String.Empty : (string)item["Medidor"];
                            ordenTrabajo.CoordenadaX = (double)item["CoordenadaX"];
                            ordenTrabajo.CoordenadaY = (double)item["CoordenadaY"];
                            ordenTrabajo.LEC = item["LEC"] == DBNull.Value ? String.Empty : (string)item["LEC"];
                            ordenTrabajo.DistritoSuministro = item["DistritoSuministro"] == DBNull.Value ? String.Empty : (string)item["DistritoSuministro"];
                            ordenTrabajo.ProvinciaSuministro = item["ProvinciaSuministro"] == DBNull.Value ? String.Empty : (string)item["ProvinciaSuministro"];
                            ordenTrabajo.DepartamentoSuministro = item["DepartamentoSuministro"] == DBNull.Value ? String.Empty : (string)item["DepartamentoSuministro"];
                            ordenTrabajo.Barrio = item["Barrio"] == DBNull.Value ? String.Empty : (string)item["Barrio"];
                            ordenTrabajo.UbicacionGeografica = item["UbicacionGeografica"] == DBNull.Value ? String.Empty : (string)item["UbicacionGeografica"];
                            ordenTrabajo.DetalleDireccion = item["DetalleDireccion"] == DBNull.Value ? String.Empty : (string)item["DetalleDireccion"];
                            ordenTrabajo.TipoInconsistencia = item["TipoInconsistencia"] == DBNull.Value ? String.Empty : (string)item["TipoInconsistencia"];
                            ordenTrabajo.OrdenReparto = (double)item["OrdenReparto"];
                            ordenTrabajo.UsuarioReparto = item["UsuarioReparto"] == DBNull.Value ? String.Empty : (string)item["UsuarioReparto"];
                            ordenTrabajo.FechaHoraLectura = item["FechaHoraLectura"] == DBNull.Value? (DateTime?)null:(DateTime)item["FechaHoraLectura"];
                            ordenTrabajo.observacion = item["Observacion"] == DBNull.Value ? String.Empty : (string)item["Observacion"];
                            ordenTrabajo.imagenes = item["imagenes"] == DBNull.Value ? String.Empty : (string)item["imagenes"];
                            ordenTrabajo.NuevaLectura = item["NuevaLectura"] == DBNull.Value ? String.Empty : (string)item["NuevaLectura"];
                            ordenTrabajo.ruta = item["ruta"] == DBNull.Value ? String.Empty : (string)item["ruta"];
                            ordenTrabajo.IdTipoObservacion = item["idTipoObservacion"] == DBNull.Value ? 0 : (int)item["idTipoObservacion"];
                        }
                    }
                }
            }

            return ordenTrabajo;
        }

        public async Task<OrdenTrabajo> registrarLectura(OrdenTrabajo ot)
        {
            var resultado = 0;
            using (var sql = new SqlConnection(cn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("registrarLectura", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdOrdenTrabajo", ot.IdOrdenTrabajo);
                    cmd.Parameters.AddWithValue("@nuevaLectura", ot.NuevaLectura);
                    cmd.Parameters.AddWithValue("@observacion", ot.observacion);
                    cmd.Parameters.AddWithValue("@imagen", ot.imagenes);
                    cmd.Parameters.AddWithValue("@ruta", ot.ruta);
                    cmd.Parameters.AddWithValue("@tipoObs", ot.IdTipoObservacion);

                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    
                }
            }
            return ot;
        }
    }
}
