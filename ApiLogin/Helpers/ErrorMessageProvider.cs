using ApiLogin.Models;
using System.IO;
using System.Text.Json;

namespace ApiLogin.Helpers // Cambia el espacio de nombres según tu estructura
{
    public class ErrorMessageProvider
    {
        private readonly ErrorMessages _errorMessages;

        public ErrorMessageProvider()
        {
            // Suponiendo que el archivo está en el directorio raíz del proyecto
            string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Messages", "SpanishError.json");

            // Leer el contenido del archivo
            string jsonContent = File.ReadAllText(jsonFilePath);

            // Deserializar JSON a objeto ErrorMessages
            _errorMessages = JsonSerializer.Deserialize<ErrorMessages>(jsonContent);
        }

        public string GetErrorMessage(string key)
        {
            // Retornar el mensaje de error basado en la clave
            return key switch
            {
                "LoginError" => _errorMessages.LoginError,
                "DatabaseError" => _errorMessages.DatabaseError,
                "InvalidLogin" => _errorMessages.InvalidLogin,
                "UserInactive" => _errorMessages.UserInactive,
                _ => _errorMessages.GeneralError
            };
        }
    }
}
