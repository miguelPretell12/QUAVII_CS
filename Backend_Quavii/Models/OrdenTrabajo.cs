namespace Backend_Quavii.Models
{
    public class OrdenTrabajo
    {
        public int IdOrdenTrabajo { get; set; }
        public string? TipoTrabajo { get; set; }
        public string? SectorOperativo { get; set; }
        public double Producto { get; set; }
        public double Contrato { get; set; }
        public double Ciclo { get; set; }
        public string? Medidor { get; set; }
        public double CoordenadaX { get; set; }
        public double CoordenadaY { get; set; }
        public string? LEC { get; set; }
        public string? DistritoSuministro { get; set; }
        public string? ProvinciaSuministro { get; set; }
        public string? DepartamentoSuministro { get; set; }
        public string? Barrio { get; set; }
        public string? UbicacionGeografica { get; set; }
        public string? DetalleDireccion { get; set; }
        public string? UltimaLectura { get; set; }
        public string? NuevaLectura { get; set; }
        public string? TipoInconsistencia { get; set; }
        public double OrdenReparto { get; set; }
        public string? UsuarioReparto { get; set; }
        
        public string? observacion { get; set; }
        public DateTime? FechaHoraLectura { get; set; }
        public string? imagen { get; set; }
        public string? imagenes { get; set; }
        public string? ruta { get; set;}
        //public List<OrdenTrabajo> lista { get; set; }

        public int IdUsuario { get; set; }
        public int opcion1 { get; set; }
        public int opcion2 { get; set; }

        public int IdTipoObservacion { get; set; }
        public string? descripcion { get; set; }
    }
}
