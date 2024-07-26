using ApiLogin.Helpers;
using ApiLogin.Models;
using ApiLogin.Models.Database;
using ApiLogin.Models.Request.Login;
using ApiLogin.Models.Response.Login;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ApiLogin.Services.Login
{
    public class LoginApiClient : ILoginApiClient
    {
        private readonly PracticaContext _dbContext;
        private readonly ErrorMessageProvider _errorMessageProvider;
        public LoginApiClient(PracticaContext dbContext)
        {
            _dbContext = dbContext;
            _errorMessageProvider = new ErrorMessageProvider();
        }
        public async Task<LoginResponse> PostLogin(LoginRequest request)
        {
            var response = new LoginResponse();

            try
            {
                // Obtener la cadena de conexión del contexto de base de datos
                var connectionString = _dbContext.Database.GetDbConnection().ConnectionString;

                // Crear y abrir la conexión
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // Crear el comando y especificar el procedimiento almacenado
                    using (var command = new SqlCommand("UserLogin", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Agregar los parámetros
                        command.Parameters.Add(new SqlParameter("@UsernameOrEmail", SqlDbType.NVarChar) { Value = request.UsernameOrEmail });
                        command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar) { Value = request.Password });

                        // Ejecutar el procedimiento almacenado
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                // Leer y procesar los datos devueltos
                                var token = reader.GetString(reader.GetOrdinal("Token"));
                                var ExpiresAt = reader.GetDateTime(reader.GetOrdinal("ExpiresAt"));

                                // Rellenar la respuesta
                                response.IsSuccess = true;
                                response.Token = token;
                                response.ExpiresAt = ExpiresAt;
                            }
                            else
                            {
                                // Usuario no encontrado o credenciales incorrectas
                                response.ErrorMessage = _errorMessageProvider.GetErrorMessage("InvalidLogin");
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejo de excepciones específicas de SQL
                if (ex.Message.Contains("Invalid username or password"))
                {
                    response.ErrorMessage = _errorMessageProvider.GetErrorMessage("InvalidLogin");
                }else if (ex.Message.Contains("User is inactive"))
                {
                    response.ErrorMessage = _errorMessageProvider.GetErrorMessage("UserInactive");
                }
                else
                {
                    response.ErrorMessage = _errorMessageProvider.GetErrorMessage("LoginError");
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones generales
                response.ErrorMessage = _errorMessageProvider.GetErrorMessage("LoginError");
            }

            return response;
        }
        public async Task<RegisterResponse> PostUser(RegisterRequest request)
        {
            var response = new RegisterResponse();

            var newUserIdParam = new SqlParameter
            {
                ParameterName = "@NewUserId",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC [dbo].[CreateUser] @Username, @Email, @Password, @NewUserId OUTPUT",
                    new SqlParameter("@Username", request.Username),
                    new SqlParameter("@Email", request.Email),
                    new SqlParameter("@Password", request.Password),
                    newUserIdParam);

                response.NewUserId = (int)newUserIdParam.Value;
            }
            catch (SqlException sqlEx)
            {
                // Manejo específico para excepciones de SQL
                string errorMessage = sqlEx.Number switch
                {
                    // Puedes agregar más códigos de error SQL si es necesario
                    50000 => sqlEx.Message,  // Error personalizado lanzado desde el procedimiento almacenado
                    _ => _errorMessageProvider.GetErrorMessage("DatabaseError")  // Mensaje genérico de error de base de datos
                };

                response.ErrorMessage = errorMessage;  // Añadir mensaje de error a la respuesta
            }
            catch (Exception ex)
            {
                // Manejo para otras excepciones generales
                response.ErrorMessage = _errorMessageProvider.GetErrorMessage("UnexpectedError");
                // Podrías registrar el error aquí para análisis posterior
            }

            return response;
        }
    }
}
