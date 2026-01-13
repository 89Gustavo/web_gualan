

namespace web_gualan.Helpers
{
    public static class ValidarArchivo
    {
        public static bool ValidarNombre(string fileName,string nombreArchivoAValidar)
        {
            // Solo validar que el nombre contenga "DatosColocaciones", sin importar mayúsculas/minúsculas
            return Path.GetFileName(fileName)
                       .Contains(nombreArchivoAValidar, StringComparison.OrdinalIgnoreCase);
        }
    }
}
