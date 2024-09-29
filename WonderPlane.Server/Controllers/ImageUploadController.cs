using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;

namespace WonderPlane.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly Cloudinary _cloudinary;

        public ImageUploadController(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        [HttpPost("imageUpload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            // Validar que se haya seleccionado un archivo
            if (file == null || file.Length == 0)
            {
                return BadRequest("Debe seleccionar un archivo de imagen.");
            }

            // Verificar el tipo MIME del archivo
            var supportedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
            if (!supportedTypes.Contains(file.ContentType))
            {
                return BadRequest("Tipo de archivo no soportado. Solo se permiten imágenes en formato JPEG, PNG y GIF.");
            }

            try
            {
                // Parámetros de subida para Cloudinary
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    AssetFolder = "WonderPlaneUsersImages",
                    Transformation = new Transformation().Crop("limit").Width(500).Height(500)
                };

                // Subir imagen a Cloudinary
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                // Verificar si la subida fue exitosa
                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // Retornar la URL de la imagen subida
                    return Ok(new { imageUrl = uploadResult.SecureUrl.ToString() });
                }

                return StatusCode((int)uploadResult.StatusCode, "No se pudo subir la imagen a Cloudinary.");
            }
            catch (Exception ex)
            {
                // Capturar errores y devolver un mensaje de error
                return StatusCode(500, $"Ocurrió un error al subir la imagen: {ex.Message}");
            }
        }
    }
}
