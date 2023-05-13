namespace Backend_Quavii.Conexion
{
    public class ConexionBD
    {
        private string connectionString = String.Empty;

        public ConexionBD()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            connectionString = builder.GetSection("ConnectionStrings:ConexionQuavii").Value;
        }

        public string ConectarSQL()
        {
            return connectionString;
        }
    }
}
