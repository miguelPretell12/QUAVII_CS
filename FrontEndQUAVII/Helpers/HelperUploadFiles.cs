using FrontEndQUAVII.Providers;

namespace FrontEndQUAVII.Helpers
{
    public class HelperUploadFiles
    {
        private PathProvider pathProvider;
        
        public HelperUploadFiles (PathProvider pathProvider)
        {
            this.pathProvider =pathProvider;
        }

        public  async Task<String> UploadFilesAsync(IFormFile formFile, string nombreImagen, Folders folders)
        {
            string path = this.pathProvider.MapPath(nombreImagen, folders);

            using(Stream stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return path;
        }
    }
}
